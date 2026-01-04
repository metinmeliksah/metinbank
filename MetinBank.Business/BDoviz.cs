using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    /// <summary>
    /// Döviz işlemleri business logic sınıfı
    /// </summary>
    public class BDoviz
    {
        private readonly DataAccess _dataAccess;

        public BDoviz()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// Güncel döviz kurlarını getirir
        /// </summary>
        public string GetDovizKurlari(out DataTable kurlar)
        {
            kurlar = null;
            try
            {
                string query = @"SELECT ParaBirimi, AlisFiyati, SatisFiyati, GuncellemeTarihi
                                FROM DovizKur
                                WHERE (ParaBirimi, GuncellemeTarihi) IN (
                                    SELECT ParaBirimi, MAX(GuncellemeTarihi) 
                                    FROM DovizKur 
                                    GROUP BY ParaBirimi
                                )
                                ORDER BY ParaBirimi";

                string hata = _dataAccess.ExecuteQuery(query, null, out kurlar);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Döviz kuru getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Belirli bir döviz kurunu getirir
        /// </summary>
        public string GetDovizKuru(string paraBirimi, out decimal alisFiyati, out decimal satisFiyati)
        {
            alisFiyati = 0;
            satisFiyati = 0;
            try
            {
                string query = @"SELECT AlisFiyati, SatisFiyati FROM DovizKur
                                WHERE ParaBirimi = @paraBirimi
                                ORDER BY GuncellemeTarihi DESC LIMIT 1";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@paraBirimi", paraBirimi)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count > 0)
                {
                    alisFiyati = Convert.ToDecimal(dt.Rows[0]["AlisFiyati"]);
                    satisFiyati = Convert.ToDecimal(dt.Rows[0]["SatisFiyati"]);
                    return null;
                }
                return "Döviz kuru bulunamadı.";
            }
            catch (Exception ex)
            {
                return $"Döviz kuru getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Döviz alım işlemi (TRY -> Döviz)
        /// Müşteri TRY hesabından döviz satın alır
        /// </summary>
        public string DovizAl(int tryHesapID, int dovizHesapID, decimal dovizTutar, string dovizCinsi, 
                              int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;
            try
            {
                // Kuru al
                decimal alis, satis;
                string hata = GetDovizKuru(dovizCinsi, out alis, out satis);
                if (hata != null) return hata;

                // Satış fiyatından alım yapılır (banka satıyor)
                decimal tryTutar = dovizTutar * satis;

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // TRY hesabından bakiye kontrolü
                string queryBakiye = "SELECT KullanilabilirBakiye FROM Hesap WHERE HesapID = @hesapID FOR UPDATE";
                MySqlParameter[] paramsBakiye = new MySqlParameter[] { new MySqlParameter("@hesapID", tryHesapID) };
                object bakiyeObj;
                hata = _dataAccess.ExecuteScalar(queryBakiye, paramsBakiye, out bakiyeObj);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                decimal kulBakiye = bakiyeObj != null && bakiyeObj != DBNull.Value ? Convert.ToDecimal(bakiyeObj) : 0;
                if (kulBakiye < tryTutar)
                {
                    _dataAccess.RollbackTransaction();
                    return $"Yetersiz TRY bakiyesi. Gerekli: {tryTutar:N2} TL, Mevcut: {kulBakiye:N2} TL";
                }

                // TRY hesabından düş
                string queryDus = "UPDATE Hesap SET Bakiye = Bakiye - @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] paramsDus = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", tryTutar),
                    new MySqlParameter("@hesapID", tryHesapID)
                };
                int affected;
                hata = _dataAccess.ExecuteNonQuery(queryDus, paramsDus, out affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Döviz hesabına ekle
                string queryEkle = "UPDATE Hesap SET Bakiye = Bakiye + @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] paramsEkle = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", dovizTutar),
                    new MySqlParameter("@hesapID", dovizHesapID)
                };
                hata = _dataAccess.ExecuteNonQuery(queryEkle, paramsEkle, out affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem kaydı oluştur
                string refNo = "TRX" + DateTime.Now.Ticks.ToString().Substring(0, 12);
                string queryIslem = @"INSERT INTO Islem 
                    (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                     Tutar, ParaBirimi, DovizKuru, KullaniciID, SubeID, Aciklama, OnayDurumu, BasariliMi)
                    VALUES (@refNo, @kaynakID, @hedefID, 'Döviz', 'DovizAlim',
                            @tutar, @paraBirimi, @kur, @kullaniciID, @subeID, @aciklama, 'Tamamlandı', 1);
                    SELECT LAST_INSERT_ID();";

                MySqlParameter[] paramsIslem = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", tryHesapID),
                    new MySqlParameter("@hedefID", dovizHesapID),
                    new MySqlParameter("@tutar", dovizTutar),
                    new MySqlParameter("@paraBirimi", dovizCinsi),
                    new MySqlParameter("@kur", satis),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@aciklama", $"Döviz Alım: {dovizTutar:N2} {dovizCinsi} @ {satis:N4}")
                };

                object idObj;
                hata = _dataAccess.ExecuteScalar(queryIslem, paramsIslem, out idObj);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = Convert.ToInt64(idObj);

                hata = _dataAccess.CommitTransaction();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Döviz alım hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Döviz satım işlemi (Döviz -> TRY)
        /// Müşteri döviz hesabından TRY'ye çevirir
        /// </summary>
        public string DovizSat(int dovizHesapID, int tryHesapID, decimal dovizTutar, string dovizCinsi,
                               int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;
            try
            {
                // Kuru al
                decimal alis, satis;
                string hata = GetDovizKuru(dovizCinsi, out alis, out satis);
                if (hata != null) return hata;

                // Alış fiyatından satış yapılır (banka alıyor)
                decimal tryTutar = dovizTutar * alis;

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Döviz hesabından bakiye kontrolü
                string queryBakiye = "SELECT KullanilabilirBakiye FROM Hesap WHERE HesapID = @hesapID FOR UPDATE";
                MySqlParameter[] paramsBakiye = new MySqlParameter[] { new MySqlParameter("@hesapID", dovizHesapID) };
                object bakiyeObj;
                hata = _dataAccess.ExecuteScalar(queryBakiye, paramsBakiye, out bakiyeObj);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                decimal kulBakiye = bakiyeObj != null && bakiyeObj != DBNull.Value ? Convert.ToDecimal(bakiyeObj) : 0;
                if (kulBakiye < dovizTutar)
                {
                    _dataAccess.RollbackTransaction();
                    return $"Yetersiz {dovizCinsi} bakiyesi. Gerekli: {dovizTutar:N2}, Mevcut: {kulBakiye:N2}";
                }

                // Döviz hesabından düş
                string queryDus = "UPDATE Hesap SET Bakiye = Bakiye - @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] paramsDus = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", dovizTutar),
                    new MySqlParameter("@hesapID", dovizHesapID)
                };
                int affected;
                hata = _dataAccess.ExecuteNonQuery(queryDus, paramsDus, out affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // TRY hesabına ekle
                string queryEkle = "UPDATE Hesap SET Bakiye = Bakiye + @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] paramsEkle = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", tryTutar),
                    new MySqlParameter("@hesapID", tryHesapID)
                };
                hata = _dataAccess.ExecuteNonQuery(queryEkle, paramsEkle, out affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem kaydı oluştur
                string refNo = "TRX" + DateTime.Now.Ticks.ToString().Substring(0, 12);
                string queryIslem = @"INSERT INTO Islem 
                    (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                     Tutar, ParaBirimi, DovizKuru, KullaniciID, SubeID, Aciklama, OnayDurumu, BasariliMi)
                    VALUES (@refNo, @kaynakID, @hedefID, 'Döviz', 'DovizSatim',
                            @tutar, @paraBirimi, @kur, @kullaniciID, @subeID, @aciklama, 'Tamamlandı', 1);
                    SELECT LAST_INSERT_ID();";

                MySqlParameter[] paramsIslem = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", dovizHesapID),
                    new MySqlParameter("@hedefID", tryHesapID),
                    new MySqlParameter("@tutar", dovizTutar),
                    new MySqlParameter("@paraBirimi", dovizCinsi),
                    new MySqlParameter("@kur", alis),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@aciklama", $"Döviz Satım: {dovizTutar:N2} {dovizCinsi} @ {alis:N4}")
                };

                object idObj;
                hata = _dataAccess.ExecuteScalar(queryIslem, paramsIslem, out idObj);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = Convert.ToInt64(idObj);

                hata = _dataAccess.CommitTransaction();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Döviz satım hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}
