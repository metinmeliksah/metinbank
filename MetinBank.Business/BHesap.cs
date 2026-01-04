using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BHesap
    {
        private readonly DataAccess _dataAccess;

        public BHesap()
        {
            _dataAccess = new DataAccess();
        }

        private static Random _random = new Random();

        public string HesapAc(HesapModel hesap, out int hesapID)
        {
            hesapID = 0;

            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Şube kodunu al
                string querySubeKod = "SELECT SubeKodu FROM Sube WHERE SubeID = @subeID";
                MySqlParameter[] paramsSubeKod = new MySqlParameter[] { new MySqlParameter("@subeID", hesap.SubeID) };
                DataTable dtSube;
                hata = _dataAccess.ExecuteQuery(querySubeKod, paramsSubeKod, out dtSube);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                if (dtSube.Rows.Count == 0) { _dataAccess.RollbackTransaction(); return "Şube bulunamadı."; }

                string subeKodu = dtSube.Rows[0]["SubeKodu"].ToString();

                // Benzersiz hesap numarası üret (16 haneli)
                // Format: Sabit 4 haneli prefix (1000-1999) + 12 haneli rastgele
                // NOT: BIGINT başındaki 0'ları tutamaz, bu yüzden ilk rakam 1 olmalı
                long hesapNo = 0;
                string hesapNoStr = "";
                int maxAttempts = 10;
                
                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    // 16 haneli: Sabit "1000" prefix + 12 hane rastgele
                    // Bu sayede her zaman 16 haneli kalır (1000XXXXXXXXXXXX)
                    string prefix = "1000"; // Sabit prefix, değişmez
                    string randomPart = GenerateRandomDigits(12);
                    hesapNoStr = prefix + randomPart;
                    hesapNo = long.Parse(hesapNoStr);

                    // Benzersizlik kontrolü
                    string queryCheck = "SELECT COUNT(*) FROM Hesap WHERE HesapNo = @hesapNo";
                    MySqlParameter[] paramsCheck = new MySqlParameter[] { new MySqlParameter("@hesapNo", hesapNo) };
                    object countObj;
                    hata = _dataAccess.ExecuteScalar(queryCheck, paramsCheck, out countObj);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    int count = Convert.ToInt32(countObj ?? 0);
                    if (count == 0)
                    {
                        break; // Benzersiz numara bulundu
                    }

                    if (attempt == maxAttempts - 1)
                    {
                        _dataAccess.RollbackTransaction();
                        return "Benzersiz hesap numarası üretilemedi. Lütfen tekrar deneyiniz.";
                    }
                }

                // IBAN oluştur - Son 11 haneyi kullan
                string hesapNoForIban = hesapNoStr.Substring(hesapNoStr.Length - 11);
                string iban = IbanHelper.GenerateIban(subeKodu, hesapNoForIban);
                iban = IbanHelper.RemoveIbanSpaces(iban);

                // Hesap ekle - Bakiye ve BlokeBakiye 0 olarak
                string queryHesap = @"INSERT INTO Hesap (HesapNo, IBAN, MusteriID, HesapTipi, HesapCinsi, Bakiye, BlokeBakiye, 
                                     FaizOrani, VadeTarihi, Durum, SubeID, GunlukTransferLimiti, AylikTransferLimiti, 
                                     OlusturanKullaniciID, OnaylayanKullaniciID, OnayTarihi)
                                     VALUES (@hesapNo, @iban, @musteriID, @hesapTipi, @hesapCinsi, 0.00, 0.00,
                                     @faizOrani, @vadeTarihi, @durum, @subeID, @gunlukLimit, @aylikLimit,
                                     @olusturanID, @onaylayanID, @onayTarihi)";

                MySqlParameter[] paramsHesap = new MySqlParameter[]
                {
                    new MySqlParameter("@hesapNo", hesapNo),
                    new MySqlParameter("@iban", iban),
                    new MySqlParameter("@musteriID", hesap.MusteriID),
                    new MySqlParameter("@hesapTipi", hesap.HesapTipi),
                    new MySqlParameter("@hesapCinsi", hesap.HesapCinsi),
                    new MySqlParameter("@faizOrani", hesap.FaizOrani),
                    new MySqlParameter("@vadeTarihi", (object)hesap.VadeTarihi ?? DBNull.Value),
                    new MySqlParameter("@durum", "Aktif"),
                    new MySqlParameter("@subeID", hesap.SubeID),
                    new MySqlParameter("@gunlukLimit", hesap.GunlukTransferLimiti > 0 ? hesap.GunlukTransferLimiti : 20000),
                    new MySqlParameter("@aylikLimit", hesap.AylikTransferLimiti > 0 ? hesap.AylikTransferLimiti : 500000),
                    new MySqlParameter("@olusturanID", hesap.OlusturanKullaniciID),
                    new MySqlParameter("@onaylayanID", (object)hesap.OnaylayanKullaniciID ?? DBNull.Value),
                    new MySqlParameter("@onayTarihi", (object)hesap.OnayTarihi ?? DBNull.Value)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(queryHesap, paramsHesap, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hesapID = (int)_dataAccess.GetLastInsertId();

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Hesap açma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Belirtilen uzunlukta rastgele rakam dizisi üretir
        /// </summary>
        private static string GenerateRandomDigits(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(_random.Next(0, 10));
            }
            return sb.ToString();
        }

        public string HesapGetir(int hesapID, out HesapModel hesap)
        {
            hesap = null;

            try
            {
                string query = @"SELECT h.*, m.Ad, m.Soyad, m.TCKN, s.SubeAdi 
                                FROM Hesap h
                                INNER JOIN Musteri m ON h.MusteriID = m.MusteriID
                                INNER JOIN Sube s ON h.SubeID = s.SubeID
                                WHERE h.HesapID = @hesapID";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@hesapID", hesapID) };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0) return "Hesap bulunamadı.";

                DataRow row = dt.Rows[0];
                hesap = new HesapModel
                {
                    HesapID = Convert.ToInt32(row["HesapID"]),
                    HesapNo = Convert.ToInt64(row["HesapNo"]),
                    IBAN = row["IBAN"].ToString(),
                    MusteriID = Convert.ToInt32(row["MusteriID"]),
                    MusteriAdi = row["Ad"].ToString() + " " + row["Soyad"].ToString(),
                    MusteriTCKN = Convert.ToInt64(row["TCKN"]),
                    HesapTipi = row["HesapTipi"].ToString(),
                    HesapCinsi = row["HesapCinsi"].ToString(),
                    Bakiye = Convert.ToDecimal(row["Bakiye"]),
                    BlokeBakiye = Convert.ToDecimal(row["BlokeBakiye"]),
                    KullanilabilirBakiye = Convert.ToDecimal(row["KullanilabilirBakiye"]),
                    FaizOrani = Convert.ToDecimal(row["FaizOrani"]),
                    VadeTarihi = row["VadeTarihi"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["VadeTarihi"]) : null,
                    AcilisTarihi = Convert.ToDateTime(row["AcilisTarihi"]),
                    KapanisTarihi = row["KapanisTarihi"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["KapanisTarihi"]) : null,
                    Durum = row["Durum"].ToString(),
                    SubeID = Convert.ToInt32(row["SubeID"]),
                    SubeAdi = row["SubeAdi"].ToString(),
                    GunlukTransferLimiti = Convert.ToDecimal(row["GunlukTransferLimiti"]),
                    AylikTransferLimiti = Convert.ToDecimal(row["AylikTransferLimiti"]),
                    OlusturanKullaniciID = Convert.ToInt32(row["OlusturanKullaniciID"])
                };

                return null;
            }
            catch (Exception ex)
            {
                return $"Hesap getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string HesapGetirIBAN(string iban, out HesapModel hesap)
        {
            hesap = null;

            try
            {
                iban = IbanHelper.RemoveIbanSpaces(iban);
                string query = @"SELECT h.*, m.Ad, m.Soyad, m.TCKN, s.SubeAdi 
                                FROM Hesap h
                                INNER JOIN Musteri m ON h.MusteriID = m.MusteriID
                                INNER JOIN Sube s ON h.SubeID = s.SubeID
                                WHERE h.IBAN = @iban";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@iban", iban) };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0) return "Hesap bulunamadı.";

                DataRow row = dt.Rows[0];
                hesap = new HesapModel
                {
                    HesapID = Convert.ToInt32(row["HesapID"]),
                    HesapNo = Convert.ToInt64(row["HesapNo"]),
                    IBAN = row["IBAN"].ToString(),
                    MusteriID = Convert.ToInt32(row["MusteriID"]),
                    MusteriAdi = row["Ad"].ToString() + " " + row["Soyad"].ToString(),
                    HesapTipi = row["HesapTipi"].ToString(),
                    Bakiye = Convert.ToDecimal(row["Bakiye"]),
                    BlokeBakiye = Convert.ToDecimal(row["BlokeBakiye"]),
                    KullanilabilirBakiye = Convert.ToDecimal(row["KullanilabilirBakiye"]),
                    Durum = row["Durum"].ToString(),
                    SubeID = Convert.ToInt32(row["SubeID"])
                };

                return null;
            }
            catch (Exception ex)
            {
                return $"Hesap getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string MusterininHesaplari(int musteriID, out DataTable hesaplar)
        {
            hesaplar = null;

            try
            {
                string query = @"SELECT HesapID, HesapNo, IBAN, HesapTipi, HesapCinsi, Bakiye, 
                                KullanilabilirBakiye, Durum, AcilisTarihi
                                FROM Hesap WHERE MusteriID = @musteriID ORDER BY AcilisTarihi DESC";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@musteriID", musteriID) };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out hesaplar);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hesap listesi getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string BakiyeGuncelle(int hesapID, decimal tutar)
        {
            try
            {
                string query = "UPDATE Hesap SET Bakiye = Bakiye + @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@hesapID", hesapID)
                };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                if (affectedRows == 0) return "Hesap bulunamadı.";

                return null;
            }
            catch (Exception ex)
            {
                return $"Bakiye güncelleme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string HesapKapat(int hesapID, int kullaniciID)
        {
            try
            {
                // Bakiye kontrolü
                HesapModel hesap;
                string hata = HesapGetir(hesapID, out hesap);
                if (hata != null) return hata;

                if (hesap.Bakiye != 0) return "Hesap bakiyesi 0 TL olmalıdır.";

                string query = "UPDATE Hesap SET Durum = 'Kapalı', KapanisTarihi = NOW() WHERE HesapID = @hesapID";
                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@hesapID", hesapID) };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hesap kapatma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}

