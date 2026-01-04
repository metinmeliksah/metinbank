using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BMusteriSifre
    {
        private readonly DataAccess _dataAccess;

        public BMusteriSifre()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// Müşteri için yeni şifre oluşturur
        /// </summary>
        public string SifreOlustur(int musteriID, string yeniSifre, string ipAdresi, out int musteriSifreID)
        {
            musteriSifreID = 0;

            try
            {
                // Şifre politikası kontrolü
                string sifreKontrol = SecurityHelper.ValidatePasswordStrength(yeniSifre);
                if (sifreKontrol != null)
                    return sifreKontrol;

                // Salt ve hash oluştur
                string salt = SecurityHelper.GenerateSalt();
                string hashedPassword = SecurityHelper.HashPassword(yeniSifre, salt);

                // Müşterinin şifresi var mı kontrol et
                string checkQuery = "SELECT MusteriSifreID FROM MusteriSifre WHERE MusteriID = @MusteriID";
                MySqlParameter[] checkParams = {
                    new MySqlParameter("@MusteriID", musteriID)
                };

                DataTable existingDt;
                string hata = _dataAccess.ExecuteQuery(checkQuery, checkParams, out existingDt);
                if (hata != null) return hata;

                if (existingDt.Rows.Count > 0)
                {
                    // Şifre zaten var - güncelle
                    string updateQuery = @"
                        UPDATE MusteriSifre 
                        SET Sifre = @Sifre, 
                            SifreTuzu = @SifreTuzu,
                            SonGuncellemeTarihi = NOW(),
                            BasarisizGirisSayisi = 0,
                            HesapKilitliMi = FALSE
                        WHERE MusteriID = @MusteriID";

                    MySqlParameter[] updateParams = {
                        new MySqlParameter("@MusteriID", musteriID),
                        new MySqlParameter("@Sifre", hashedPassword),
                        new MySqlParameter("@SifreTuzu", salt)
                    };

                    int affectedRows;
                    hata = _dataAccess.ExecuteNonQuery(updateQuery, updateParams, out affectedRows);
                    if (hata != null) return hata;

                    musteriSifreID = Convert.ToInt32(existingDt.Rows[0]["MusteriSifreID"]);
                }
                else
                {
                    // Yeni şifre oluştur
                    string insertQuery = @"
                        INSERT INTO MusteriSifre (MusteriID, Sifre, SifreTuzu, AktifMi)
                        VALUES (@MusteriID, @Sifre, @SifreTuzu, TRUE)";

                    MySqlParameter[] insertParams = {
                        new MySqlParameter("@MusteriID", musteriID),
                        new MySqlParameter("@Sifre", hashedPassword),
                        new MySqlParameter("@SifreTuzu", salt)
                    };

                    int affectedRows;
                    hata = _dataAccess.ExecuteNonQuery(insertQuery, insertParams, out affectedRows);
                    if (hata != null) return hata;

                    // Son eklenen ID'yi al
                    string lastIdQuery = "SELECT LAST_INSERT_ID()";
                    DataTable lastIdDt;
                    hata = _dataAccess.ExecuteQuery(lastIdQuery, null, out lastIdDt);
                    if (hata == null && lastIdDt.Rows.Count > 0)
                    {
                        musteriSifreID = Convert.ToInt32(lastIdDt.Rows[0][0]);
                    }

                    // İlk kayıt geçmişe ekle
                    string historyQuery = @"
                        INSERT INTO SifreGecmisi (MusteriID, EskiSifre, EskiSifreTuzu, DegistirmeNedeni, IPAdresi)
                        VALUES (@MusteriID, @EskiSifre, @EskiSifreTuzu, 'IlkKayit', @IPAdresi)";

                    MySqlParameter[] historyParams = {
                        new MySqlParameter("@MusteriID", musteriID),
                        new MySqlParameter("@EskiSifre", hashedPassword),
                        new MySqlParameter("@EskiSifreTuzu", salt),
                        new MySqlParameter("@IPAdresi", ipAdresi ?? "")
                    };

                    _dataAccess.ExecuteNonQuery(historyQuery, historyParams, out affectedRows);
                }

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşteri şifre doğrulama
        /// </summary>
        public string SifreDogrula(int musteriID, string girilenSifre, string ipAdresi, out bool basarili)
        {
            basarili = false;

            try
            {
                string query = @"
                    SELECT Sifre, SifreTuzu, HesapKilitliMi, BasarisizGirisSayisi, AktifMi
                    FROM MusteriSifre
                    WHERE MusteriID = @MusteriID";

                MySqlParameter[] parameters = {
                    new MySqlParameter("@MusteriID", musteriID)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0)
                    return "Müşteri şifresi bulunamadı. Lütfen şifrenizi oluşturun.";

                var row = dt.Rows[0];
                bool hesapKilitli = Convert.ToBoolean(row["HesapKilitliMi"]);
                bool aktif = Convert.ToBoolean(row["AktifMi"]);
                int basarisizSayisi = Convert.ToInt32(row["BasarisizGirisSayisi"]);

                if (!aktif)
                    return "Şifre aktif değil. Lütfen yönetici ile iletişime geçin.";

                if (hesapKilitli)
                    return "Hesabınız kilitlenmiştir. Lütfen şubeye başvurun.";

                string storedHash = row["Sifre"].ToString();
                string salt = row["SifreTuzu"].ToString();

                // Şifre kontrolü
                string girilenhash = SecurityHelper.HashPassword(girilenSifre, salt);

                if (girilenhash == storedHash)
                {
                    basarili = true;

                    // Başarılı giriş - sayaçları sıfırla
                    string updateQuery = @"
                        UPDATE MusteriSifre
                        SET BasarisizGirisSayisi = 0,
                            SonKullanimTarihi = NOW()
                        WHERE MusteriID = @MusteriID";

                    MySqlParameter[] updateParams = {
                        new MySqlParameter("@MusteriID", musteriID)
                    };

                    int affectedRows;
                    _dataAccess.ExecuteNonQuery(updateQuery, updateParams, out affectedRows);

                    return null; // Başarılı
                }
                else
                {
                    // Başarısız giriş
                    basarisizSayisi++;

                    bool kilitlensin = basarisizSayisi >= 5;

                    string updateQuery = @"
                        UPDATE MusteriSifre
                        SET BasarisizGirisSayisi = @BasarisizSayisi,
                            HesapKilitliMi = @Kilitlensin,
                            KilitlenmeTarihi = @KilitlenmeTarihi
                        WHERE MusteriID = @MusteriID";

                    MySqlParameter[] updateParams = {
                        new MySqlParameter("@MusteriID", musteriID),
                        new MySqlParameter("@BasarisizSayisi", basarisizSayisi),
                        new MySqlParameter("@Kilitlensin", kilitlensin),
                        new MySqlParameter("@KilitlenmeTarihi", kilitlensin ? (object)DateTime.Now : DBNull.Value)
                    };

                    int affectedRows;
                    _dataAccess.ExecuteNonQuery(updateQuery, updateParams, out affectedRows);

                    if (kilitlensin)
                        return "Çok fazla başarısız giriş denemesi. Hesabınız kilitlenmiştir.";
                    else
                        return $"Şifre hatalı. Kalan deneme hakkı: {5 - basarisizSayisi}";
                }
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Şifre değiştirme
        /// </summary>
        public string SifreDegistir(int musteriID, string eskiSifre, string yeniSifre, string ipAdresi)
        {
            try
            {
                // Önce eski şifreyi doğrula
                bool basarili;
                string hata = SifreDogrula(musteriID, eskiSifre, ipAdresi, out basarili);
                
                if (hata != null || !basarili)
                    return hata ?? "Eski şifre hatalı.";

                // Yeni şifre politikası kontrolü
                string sifreKontrol = SecurityHelper.ValidatePasswordStrength(yeniSifre);
                if (sifreKontrol != null)
                    return sifreKontrol;

                // Yeni şifre oluştur
                int musteriSifreID;
                hata = SifreOlustur(musteriID, yeniSifre, ipAdresi, out musteriSifreID);
                
                return hata; // null ise başarılı
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşterinin şifresi var mı kontrol et
        /// </summary>
        public string SifreVarMi(int musteriID, out bool sifreVar)
        {
            sifreVar = false;

            try
            {
                string query = "SELECT COUNT(*) FROM MusteriSifre WHERE MusteriID = @MusteriID AND AktifMi = TRUE";
                
                MySqlParameter[] parameters = {
                    new MySqlParameter("@MusteriID", musteriID)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                sifreVar = Convert.ToInt32(dt.Rows[0][0]) > 0;
                return null;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}
