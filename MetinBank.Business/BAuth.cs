using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BAuth
    {
        private readonly DataAccess _dataAccess;

        public BAuth()
        {
            _dataAccess = new DataAccess();
        }

        public string Login(string kullaniciAdi, string sifre, string ipAdresi, string macAdresi, out KullaniciModel kullanici)
        {
            kullanici = null;

            try
            {
                string query = @"SELECT k.*, r.RolAdi, r.YetkiSeviyesi, s.SubeAdi 
                                FROM Kullanici k
                                INNER JOIN Rol r ON k.RolID = r.RolID
                                LEFT JOIN Sube s ON k.SubeID = s.SubeID
                                WHERE k.KullaniciAdi = @kullaniciAdi";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@kullaniciAdi", kullaniciAdi) };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0) return "Kullanıcı adı veya şifre hatalı.";

                DataRow row = dt.Rows[0];

                bool hesapKilitli = Convert.ToBoolean(row["HesapKilitliMi"]);
                if (hesapKilitli) return "Hesabınız kilitlenmiştir. Lütfen yöneticiniz ile iletişime geçin.";

                bool aktifMi = Convert.ToBoolean(row["AktifMi"]);
                if (!aktifMi) return "Hesabınız pasif durumdadır.";

                string sifreHash = row["Sifre"].ToString();
                string sifreTuzu = row["SifreTuzu"].ToString();

                if (!SecurityHelper.VerifyPassword(sifre, sifreHash, sifreTuzu))
                {
                    // Başarısız giriş sayısını artır
                    int basarisizSayisi = Convert.ToInt32(row["BasarisizGirisSayisi"]) + 1;
                    
                    string updateQuery = "UPDATE Kullanici SET BasarisizGirisSayisi = @sayi";
                    
                    if (basarisizSayisi >= 5)
                    {
                        updateQuery += ", HesapKilitliMi = 1";
                    }
                    
                    updateQuery += " WHERE KullaniciID = @id";

                    MySqlParameter[] updateParams = new MySqlParameter[]
                    {
                        new MySqlParameter("@sayi", basarisizSayisi),
                        new MySqlParameter("@id", row["KullaniciID"])
                    };

                    int affected;
                    _dataAccess.ExecuteNonQuery(updateQuery, updateParams, out affected);

                    if (basarisizSayisi >= 5)
                        return "Hesabınız 5 başarısız giriş nedeniyle kilitlenmiştir.";

                    return $"Kullanıcı adı veya şifre hatalı. Kalan deneme: {5 - basarisizSayisi}";
                }

                // Başarılı giriş - sayacı sıfırla ve son giriş tarihini güncelle
                string successQuery = @"UPDATE Kullanici SET BasarisizGirisSayisi = 0, SonGirisTarihi = NOW() 
                                       WHERE KullaniciID = @id";
                MySqlParameter[] successParams = new MySqlParameter[] { new MySqlParameter("@id", row["KullaniciID"]) };
                int affectedRows;
                _dataAccess.ExecuteNonQuery(successQuery, successParams, out affectedRows);

                kullanici = new KullaniciModel
                {
                    KullaniciID = Convert.ToInt32(row["KullaniciID"]),
                    KullaniciAdi = row["KullaniciAdi"].ToString(),
                    RolID = Convert.ToInt32(row["RolID"]),
                    RolAdi = row["RolAdi"].ToString(),
                    SubeID = row["SubeID"] != DBNull.Value ? (int?)Convert.ToInt32(row["SubeID"]) : null,
                    SubeAdi = row["SubeAdi"] != DBNull.Value ? row["SubeAdi"].ToString() : null,
                    Ad = row["Ad"].ToString(),
                    Soyad = row["Soyad"].ToString(),
                    Email = row["Email"].ToString(),
                    Telefon = row["Telefon"].ToString(),
                    AktifMi = true,
                    HesapKilitliMi = false
                };

                return null;
            }
            catch (Exception ex)
            {
                return $"Login hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string Logout(int kullaniciID, string ipAdresi)
        {
            try
            {
                // Logout işlemi için özel bir işlem yapmaya gerek yok
                // Log kaydı service katmanında tutulacak
                return null;
            }
            catch (Exception ex)
            {
                return $"Logout hatası: {ex.Message}";
            }
        }

        public string SifreDegistir(int kullaniciID, string eskiSifre, string yeniSifre)
        {
            try
            {
                // Kullanıcıyı getir
                string query = "SELECT Sifre, SifreTuzu FROM Kullanici WHERE KullaniciID = @id";
                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@id", kullaniciID) };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0) return "Kullanıcı bulunamadı.";

                string sifreHash = dt.Rows[0]["Sifre"].ToString();
                string sifreTuzu = dt.Rows[0]["SifreTuzu"].ToString();

                // Eski şifre kontrolü
                if (!SecurityHelper.VerifyPassword(eskiSifre, sifreHash, sifreTuzu))
                    return "Eski şifre hatalı.";

                // Yeni şifre hash'le
                string yeniTuz = SecurityHelper.GenerateSalt();
                string yeniHash = SecurityHelper.HashPassword(yeniSifre, yeniTuz);

                // Güncelle
                string updateQuery = @"UPDATE Kullanici SET Sifre = @sifre, SifreTuzu = @tuz, 
                                      SonSifreDegistirmeTarihi = NOW() WHERE KullaniciID = @id";

                MySqlParameter[] updateParams = new MySqlParameter[]
                {
                    new MySqlParameter("@sifre", yeniHash),
                    new MySqlParameter("@tuz", yeniTuz),
                    new MySqlParameter("@id", kullaniciID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(updateQuery, updateParams, out affectedRows);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Şifre değiştirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string HesapKilidiAc(int kullaniciID, int adminID)
        {
            try
            {
                string query = "UPDATE Kullanici SET HesapKilitliMi = 0, BasarisizGirisSayisi = 0 WHERE KullaniciID = @id";
                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@id", kullaniciID) };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                if (affectedRows == 0) return "Kullanıcı bulunamadı.";

                return null;
            }
            catch (Exception ex)
            {
                return $"Hesap kilidi açma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string YetkiKontrol(int kullaniciID, int gerekliYetkiSeviyesi)
        {
            try
            {
                string query = @"SELECT r.YetkiSeviyesi FROM Kullanici k
                                INNER JOIN Rol r ON k.RolID = r.RolID
                                WHERE k.KullaniciID = @id";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@id", kullaniciID) };

                object result;
                string hata = _dataAccess.ExecuteScalar(query, parameters, out result);
                if (hata != null) return hata;

                if (result == null) return "Kullanıcı bulunamadı.";

                int yetkiSeviyesi = Convert.ToInt32(result);

                if (yetkiSeviyesi < gerekliYetkiSeviyesi)
                    return "Bu işlem için yetkiniz bulunmamaktadır.";

                return null;
            }
            catch (Exception ex)
            {
                return $"Yetki kontrolü hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}

