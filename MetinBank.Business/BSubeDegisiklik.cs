using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Util;
using MetinBank.Models;

namespace MetinBank.Business
{
    /// <summary>
    /// Şube değişikliği business logic sınıfı
    /// </summary>
    public class BSubeDegisiklik
    {
        private readonly DataAccess _dataAccess;
        private readonly BLog _bLog;

        public BSubeDegisiklik()
        {
            _dataAccess = new DataAccess();
            _bLog = new BLog();
        }

        /// <summary>
        /// Yeni şube değişikliği talebi oluşturur
        /// </summary>
        public string TalepOlustur(int kullaniciID, int mevcutSubeID, int yeniSubeID, string talepNedeni, out int talepID)
        {
            talepID = 0;

            try
            {
                // Aynı şubeye talep kontrolü
                if (mevcutSubeID == yeniSubeID)
                    return "Mevcut şubeniz ile aynı şubeye talep açamazsınız.";

                // Validasyonlar
                if (string.IsNullOrWhiteSpace(talepNedeni))
                    return "Talep nedeni boş olamaz.";

                if (talepNedeni.Length < 20)
                    return "Talep nedeni en az 20 karakter olmalıdır.";

                // Bekleyen talep var mı kontrol et
                string checkQuery = @"SELECT COUNT(*) FROM SubeDegisiklikTalep 
                                    WHERE KullaniciID = @kullaniciID AND OnayDurumu = 'Beklemede'";
                
                MySqlParameter[] checkParams = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciID", kullaniciID)
                };

                object result;
                string hata = _dataAccess.ExecuteScalar(checkQuery, checkParams, out result);
                if (hata != null) return hata;

                if (Convert.ToInt32(result) > 0)
                    return "Zaten beklemede olan bir talebiniz var. Önce onu tamamlayın.";

                // Talep oluştur
                string query = @"INSERT INTO SubeDegisiklikTalep 
                               (KullaniciID, MevcutSubeID, YeniSubeID, TalepNedeni, OnayDurumu)
                               VALUES (@kullaniciID, @mevcutSubeID, @yeniSubeID, @talepNedeni, 'Beklemede')";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@mevcutSubeID", mevcutSubeID),
                    new MySqlParameter("@yeniSubeID", yeniSubeID),
                    new MySqlParameter("@talepNedeni", talepNedeni)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                talepID = (int)_dataAccess.GetLastInsertId();

                // Log kaydet
                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "SubeDegisiklikTalebi",
                    "SubeDegisiklikTalep",
                    talepID,
                    null,
                    null,
                    $"Kullanıcı şube değişikliği talebi oluşturdu. Yeni Şube ID: {yeniSubeID}",
                    CommonFunctions.GetLocalIPAddress(),
                    true,
                    null
                );

                return null;
            }
            catch (Exception ex)
            {
                return $"Talep oluşturma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Şube değişikliği talebini onaylar ve kullanıcının şubesini günceller
        /// </summary>
        public string TalepOnayla(int talepID, int onaylayanID)
        {
            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Talep detaylarını al
                string detayQuery = @"SELECT KullaniciID, YeniSubeID, OnayDurumu 
                                    FROM SubeDegisiklikTalep WHERE TalepID = @talepID";
                
                MySqlParameter[] detayParams = new MySqlParameter[]
                {
                    new MySqlParameter("@talepID", talepID)
                };

                DataTable dtDetay;
                hata = _dataAccess.ExecuteQuery(detayQuery, detayParams, out dtDetay);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (dtDetay.Rows.Count == 0)
                {
                    _dataAccess.RollbackTransaction();
                    return "Talep bulunamadı.";
                }

                DataRow row = dtDetay.Rows[0];
                string onayDurumu = row["OnayDurumu"].ToString();
                
                if (onayDurumu != "Beklemede")
                {
                    _dataAccess.RollbackTransaction();
                    return "Bu talep zaten işleme alınmış.";
                }

                int kullaniciID = Convert.ToInt32(row["KullaniciID"]);
                int yeniSubeID = Convert.ToInt32(row["YeniSubeID"]);

                // Talebi onayla
                string onayQuery = @"UPDATE SubeDegisiklikTalep 
                                   SET OnayDurumu = 'Onaylandı', OnaylayanID = @onaylayanID, OnayTarihi = NOW()
                                   WHERE TalepID = @talepID";

                MySqlParameter[] onayParams = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@talepID", talepID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(onayQuery, onayParams, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Kullanıcının şubesini güncelle
                string guncelleQuery = @"UPDATE Kullanici SET SubeID = @yeniSubeID WHERE KullaniciID = @kullaniciID";

                MySqlParameter[] guncelleParams = new MySqlParameter[]
                {
                    new MySqlParameter("@yeniSubeID", yeniSubeID),
                    new MySqlParameter("@kullaniciID", kullaniciID)
                };

                hata = _dataAccess.ExecuteNonQuery(guncelleQuery, guncelleParams, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Commit transaction
                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                // Log kaydet
                _bLog.IslemLoguKaydet(
                    onaylayanID,
                    "SubeDegisiklikOnay",
                    "SubeDegisiklikTalep",
                    talepID,
                    null,
                    null,
                    $"Şube değişikliği talebi onaylandı. Kullanıcı ID: {kullaniciID}, Yeni Şube ID: {yeniSubeID}",
                    CommonFunctions.GetLocalIPAddress(),
                    true,
                    null
                );

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Talep onaylama hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Şube değişikliği talebini reddeder
        /// </summary>
        public string TalepReddet(int talepID, int onaylayanID, string redNedeni)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(redNedeni))
                    return "Red nedeni boş olamaz.";

                // Talep durumunu kontrol et
                string checkQuery = @"SELECT OnayDurumu FROM SubeDegisiklikTalep WHERE TalepID = @talepID";
                
                MySqlParameter[] checkParams = new MySqlParameter[]
                {
                    new MySqlParameter("@talepID", talepID)
                };

                object result;
                string hata = _dataAccess.ExecuteScalar(checkQuery, checkParams, out result);
                if (hata != null) return hata;

                if (result == null || result == DBNull.Value)
                    return "Talep bulunamadı.";

                string onayDurumu = result.ToString();
                if (onayDurumu != "Beklemede")
                    return "Bu talep zaten işleme alınmış.";

                // Talebi reddet
                string query = @"UPDATE SubeDegisiklikTalep 
                               SET OnayDurumu = 'Reddedildi', OnaylayanID = @onaylayanID, 
                                   OnayTarihi = NOW(), RedNedeni = @redNedeni
                               WHERE TalepID = @talepID";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@redNedeni", redNedeni),
                    new MySqlParameter("@talepID", talepID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                // Log kaydet
                _bLog.IslemLoguKaydet(
                    onaylayanID,
                    "SubeDegisiklikRed",
                    "SubeDegisiklikTalep",
                    talepID,
                    null,
                    null,
                    $"Şube değişikliği talebi reddedildi. Red nedeni: {redNedeni}",
                    CommonFunctions.GetLocalIPAddress(),
                    true,
                    null
                );

                return null;
            }
            catch (Exception ex)
            {
                return $"Talep reddetme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Bekleyen şube değişikliği taleplerini getirir
        /// </summary>
        public string BekleyenTalepleriGetir(out DataTable talepler)
        {
            talepler = null;

            try
            {
                string query = @"SELECT * FROM vw_SubeDegisiklikTalepleri 
                               WHERE OnayDurumu = 'Beklemede' 
                               ORDER BY TalepTarihi ASC";

                string hata = _dataAccess.ExecuteQuery(query, null, out talepler);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Bekleyen talepler getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Kullanıcının şube değişikliği talep durumunu getirir
        /// </summary>
        public string KullaniciTalepDurumuGetir(int kullaniciID, out DataTable talepler)
        {
            talepler = null;

            try
            {
                string query = @"SELECT * FROM vw_SubeDegisiklikTalepleri 
                               WHERE KullaniciID = @kullaniciID 
                               ORDER BY TalepTarihi DESC";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciID", kullaniciID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out talepler);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Kullanıcı talep durumu getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Tüm şube değişikliği taleplerini getirir (raporlama için)
        /// </summary>
        public string TumTalepleriGetir(out DataTable talepler)
        {
            talepler = null;

            try
            {
                string query = @"SELECT * FROM vw_SubeDegisiklikTalepleri 
                               ORDER BY TalepTarihi DESC";

                string hata = _dataAccess.ExecuteQuery(query, null, out talepler);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Tüm talepler getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}
