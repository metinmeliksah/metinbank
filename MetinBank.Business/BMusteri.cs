using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    /// <summary>
    /// Müşteri işlemleri business logic sınıfı
    /// </summary>
    public class BMusteri
    {
        private readonly DataAccess _dataAccess;

        public BMusteri()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// Yeni müşteri kaydı oluşturur
        /// </summary>
        public string MusteriEkle(MusteriModel musteri, out int musteriID)
        {
            musteriID = 0;

            try
            {
                // Validasyonlar
                string hata = ValidationHelper.ValidateTCKN(musteri.TCKN.ToString());
                if (hata != null) return hata;

                hata = ValidationHelper.ValidateRequired(musteri.Ad, "Ad");
                if (hata != null) return hata;

                hata = ValidationHelper.ValidateRequired(musteri.Soyad, "Soyad");
                if (hata != null) return hata;

                hata = ValidationHelper.ValidateTelefon(musteri.CepTelefon);
                if (hata != null) return hata;

                if (!string.IsNullOrEmpty(musteri.Email))
                {
                    hata = ValidationHelper.ValidateEmail(musteri.Email);
                    if (hata != null) return hata;
                }

                if (musteri.DogumTarihi.HasValue)
                {
                    hata = ValidationHelper.ValidateYas(musteri.DogumTarihi.Value);
                    if (hata != null) return hata;
                }

                // TCKN kontrolü
                string query = "SELECT COUNT(*) FROM Musteri WHERE TCKN = @tckn";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@tckn", musteri.TCKN)
                };

                object result;
                hata = _dataAccess.ExecuteScalar(query, parameters, out result);
                if (hata != null) return hata;

                if (Convert.ToInt32(result) > 0)
                    return "Bu TCKN ile kayıtlı müşteri zaten mevcut.";

                // Müşteri numarası oluştur
                query = "SELECT COALESCE(MAX(CAST(SUBSTRING(MusteriNo, 2) AS UNSIGNED)), 0) FROM Musteri";
                hata = _dataAccess.ExecuteScalar(query, null, out result);
                if (hata != null) return hata;

                long sonMusteriNo = Convert.ToInt64(result);
                musteri.MusteriNo = CommonFunctions.GenerateMusteriNo(sonMusteriNo);

                // Müşteri ekle
                query = @"INSERT INTO Musteri (MusteriNo, TCKN, VergiNo, Ad, Soyad, DogumTarihi, DogumYeri, 
                         AnneAdi, BabaAdi, Cinsiyet, MedeniDurum, Telefon, CepTelefon, Email, Adres, Il, Ilce, 
                         PostaKodu, GelirDurumu, MeslekBilgisi, MusteriTipi, MusteriSegmenti, KayitSubeID, AktifMi)
                         VALUES (@musteriNo, @tckn, @vergiNo, @ad, @soyad, @dogumTarihi, @dogumYeri, 
                         @anneAdi, @babaAdi, @cinsiyet, @medeniDurum, @telefon, @cepTelefon, @email, @adres, @il, @ilce, 
                         @postaKodu, @gelirDurumu, @meslekBilgisi, @musteriTipi, @musteriSegmenti, @kayitSubeID, @aktifMi)";

                parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriNo", musteri.MusteriNo),
                    new MySqlParameter("@tckn", musteri.TCKN),
                    new MySqlParameter("@vergiNo", (object)musteri.VergiNo ?? DBNull.Value),
                    new MySqlParameter("@ad", musteri.Ad),
                    new MySqlParameter("@soyad", musteri.Soyad),
                    new MySqlParameter("@dogumTarihi", (object)musteri.DogumTarihi ?? DBNull.Value),
                    new MySqlParameter("@dogumYeri", musteri.DogumYeri ?? ""),
                    new MySqlParameter("@anneAdi", musteri.AnneAdi ?? ""),
                    new MySqlParameter("@babaAdi", musteri.BabaAdi ?? ""),
                    new MySqlParameter("@cinsiyet", musteri.Cinsiyet ?? ""),
                    new MySqlParameter("@medeniDurum", musteri.MedeniDurum ?? ""),
                    new MySqlParameter("@telefon", musteri.Telefon ?? ""),
                    new MySqlParameter("@cepTelefon", musteri.CepTelefon),
                    new MySqlParameter("@email", musteri.Email ?? ""),
                    new MySqlParameter("@adres", musteri.Adres ?? ""),
                    new MySqlParameter("@il", musteri.Il ?? ""),
                    new MySqlParameter("@ilce", musteri.Ilce ?? ""),
                    new MySqlParameter("@postaKodu", musteri.PostaKodu ?? ""),
                    new MySqlParameter("@gelirDurumu", musteri.GelirDurumu),
                    new MySqlParameter("@meslekBilgisi", musteri.MeslekBilgisi ?? ""),
                    new MySqlParameter("@musteriTipi", musteri.MusteriTipi ?? "Bireysel"),
                    new MySqlParameter("@musteriSegmenti", musteri.MusteriSegmenti ?? "Standart"),
                    new MySqlParameter("@kayitSubeID", musteri.KayitSubeID),
                    new MySqlParameter("@aktifMi", true)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                musteriID = (int)_dataAccess.GetLastInsertId();

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri ekleme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşteri bilgilerini günceller
        /// </summary>
        public string MusteriGuncelle(MusteriModel musteri)
        {
            try
            {
                // Validasyonlar
                string hata = ValidationHelper.ValidateTelefon(musteri.CepTelefon);
                if (hata != null) return hata;

                if (!string.IsNullOrEmpty(musteri.Email))
                {
                    hata = ValidationHelper.ValidateEmail(musteri.Email);
                    if (hata != null) return hata;
                }

                string query = @"UPDATE Musteri SET 
                                Telefon = @telefon,
                                CepTelefon = @cepTelefon,
                                Email = @email,
                                Adres = @adres,
                                Il = @il,
                                Ilce = @ilce,
                                PostaKodu = @postaKodu,
                                GelirDurumu = @gelirDurumu,
                                MeslekBilgisi = @meslekBilgisi,
                                MusteriSegmenti = @musteriSegmenti
                                WHERE MusteriID = @musteriID";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@telefon", musteri.Telefon ?? ""),
                    new MySqlParameter("@cepTelefon", musteri.CepTelefon),
                    new MySqlParameter("@email", musteri.Email ?? ""),
                    new MySqlParameter("@adres", musteri.Adres ?? ""),
                    new MySqlParameter("@il", musteri.Il ?? ""),
                    new MySqlParameter("@ilce", musteri.Ilce ?? ""),
                    new MySqlParameter("@postaKodu", musteri.PostaKodu ?? ""),
                    new MySqlParameter("@gelirDurumu", musteri.GelirDurumu),
                    new MySqlParameter("@meslekBilgisi", musteri.MeslekBilgisi ?? ""),
                    new MySqlParameter("@musteriSegmenti", musteri.MusteriSegmenti ?? "Standart"),
                    new MySqlParameter("@musteriID", musteri.MusteriID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                if (affectedRows == 0)
                    return "Müşteri bulunamadı.";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri güncelleme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşteri detaylarını getirir
        /// </summary>
        public string MusteriGetir(int musteriID, out MusteriModel musteri)
        {
            musteri = null;

            try
            {
                string query = @"SELECT m.*, s.SubeAdi as KayitSubeAdi 
                                FROM Musteri m 
                                LEFT JOIN Sube s ON m.KayitSubeID = s.SubeID 
                                WHERE m.MusteriID = @musteriID";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriID", musteriID)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0)
                    return "Müşteri bulunamadı.";

                DataRow row = dt.Rows[0];
                musteri = DataRowToModel(row);

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// TCKN ile müşteri arar
        /// </summary>
        public string MusteriGetirTCKN(long tckn, out MusteriModel musteri)
        {
            musteri = null;

            try
            {
                string query = @"SELECT m.*, s.SubeAdi as KayitSubeAdi 
                                FROM Musteri m 
                                LEFT JOIN Sube s ON m.KayitSubeID = s.SubeID 
                                WHERE m.TCKN = @tckn";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@tckn", tckn)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                if (hata != null) return hata;

                if (dt.Rows.Count == 0)
                    return "Müşteri bulunamadı.";

                DataRow row = dt.Rows[0];
                musteri = DataRowToModel(row);

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşteri listesini getirir
        /// </summary>
        public string MusterileriGetir(out DataTable musteriListesi)
        {
            musteriListesi = null;

            try
            {
                string query = @"SELECT m.MusteriID, m.MusteriNo, m.TCKN, 
                                CONCAT(m.Ad, ' ', m.Soyad) as AdSoyad,
                                m.CepTelefon, m.Email, m.MusteriTipi, m.MusteriSegmenti, 
                                m.AktifMi, s.SubeAdi, m.KayitTarihi
                                FROM Musteri m
                                LEFT JOIN Sube s ON m.KayitSubeID = s.SubeID
                                ORDER BY m.MusteriID DESC
                                LIMIT 1000";

                string hata = _dataAccess.ExecuteQuery(query, null, out musteriListesi);
                if (hata != null) return hata;

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri listesi getirme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşteri arama
        /// </summary>
        public string MusteriAra(string aramaKelimesi, out DataTable sonuclar)
        {
            sonuclar = null;

            try
            {
                string query = @"SELECT m.MusteriID, m.MusteriNo, m.TCKN, 
                                CONCAT(m.Ad, ' ', m.Soyad) as AdSoyad,
                                m.CepTelefon, m.Email, m.MusteriTipi, m.MusteriSegmenti, 
                                m.AktifMi, s.SubeAdi
                                FROM Musteri m
                                LEFT JOIN Sube s ON m.KayitSubeID = s.SubeID
                                WHERE m.Ad LIKE @arama OR m.Soyad LIKE @arama 
                                OR m.MusteriNo LIKE @arama OR CAST(m.TCKN AS CHAR) LIKE @arama
                                ORDER BY m.MusteriID DESC
                                LIMIT 100";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@arama", "%" + aramaKelimesi + "%")
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out sonuclar);
                if (hata != null) return hata;

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri arama hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// DataRow'u Model'e çevirir
        /// </summary>
        private MusteriModel DataRowToModel(DataRow row)
        {
            return new MusteriModel
            {
                MusteriID = CommonFunctions.DbNullToInt(row["MusteriID"]),
                MusteriNo = CommonFunctions.DbNullToString(row["MusteriNo"]),
                TCKN = Convert.ToInt64(row["TCKN"]),
                VergiNo = row["VergiNo"] == DBNull.Value ? null : (long?)Convert.ToInt64(row["VergiNo"]),
                Ad = CommonFunctions.DbNullToString(row["Ad"]),
                Soyad = CommonFunctions.DbNullToString(row["Soyad"]),
                DogumTarihi = CommonFunctions.DbNullToDateTime(row["DogumTarihi"]),
                DogumYeri = CommonFunctions.DbNullToString(row["DogumYeri"]),
                AnneAdi = CommonFunctions.DbNullToString(row["AnneAdi"]),
                BabaAdi = CommonFunctions.DbNullToString(row["BabaAdi"]),
                Cinsiyet = CommonFunctions.DbNullToString(row["Cinsiyet"]),
                MedeniDurum = CommonFunctions.DbNullToString(row["MedeniDurum"]),
                Telefon = CommonFunctions.DbNullToString(row["Telefon"]),
                CepTelefon = CommonFunctions.DbNullToString(row["CepTelefon"]),
                Email = CommonFunctions.DbNullToString(row["Email"]),
                Adres = CommonFunctions.DbNullToString(row["Adres"]),
                Il = CommonFunctions.DbNullToString(row["Il"]),
                Ilce = CommonFunctions.DbNullToString(row["Ilce"]),
                PostaKodu = CommonFunctions.DbNullToString(row["PostaKodu"]),
                GelirDurumu = CommonFunctions.DbNullToDecimal(row["GelirDurumu"]),
                MeslekBilgisi = CommonFunctions.DbNullToString(row["MeslekBilgisi"]),
                MusteriTipi = CommonFunctions.DbNullToString(row["MusteriTipi"]),
                MusteriSegmenti = CommonFunctions.DbNullToString(row["MusteriSegmenti"]),
                KayitTarihi = Convert.ToDateTime(row["KayitTarihi"]),
                KayitSubeID = CommonFunctions.DbNullToInt(row["KayitSubeID"]),
                KayitSubeAdi = CommonFunctions.DbNullToString(row["KayitSubeAdi"]),
                AktifMi = CommonFunctions.DbNullToBool(row["AktifMi"], true),
                SonGuncellemeTarihi = Convert.ToDateTime(row["SonGuncellemeTarihi"])
            };
        }
    }
}

