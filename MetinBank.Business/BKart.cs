using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    /// <summary>
    /// Kart işlemleri business logic sınıfı
    /// </summary>
    public class BKart
    {
        private readonly DataAccess _dataAccess;
        private static Random _random = new Random();

        public BKart()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// Troy veya Mastercard için kart numarası üretir
        /// </summary>
        public static long GenerateCardNumber(string kartMarkasi)
        {
            string prefix;
            
            if (kartMarkasi.ToUpper() == "TROY")
            {
                prefix = "97920127"; // Troy: 9792 0127 ile başlar
            }
            else // Mastercard
            {
                prefix = "54127512"; // Mastercard: 5412 7512 ile başlar
            }

            // Kalan 7 haneli rastgele sayı (son hane Luhn için hesaplanacak)
            string randomPart = "";
            for (int i = 0; i < 7; i++)
            {
                randomPart += _random.Next(0, 10).ToString();
            }

            string cardWithoutCheckDigit = prefix + randomPart;
            
            // Luhn check digit hesapla
            int checkDigit = CalculateLuhnCheckDigit(cardWithoutCheckDigit);
            string fullCardNumber = cardWithoutCheckDigit + checkDigit.ToString();

            return long.Parse(fullCardNumber);
        }

        /// <summary>
        /// Luhn algoritması ile kontrol hanesi hesaplar
        /// </summary>
        private static int CalculateLuhnCheckDigit(string cardNumber)
        {
            int sum = 0;
            bool alternate = true;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return (10 - (sum % 10)) % 10;
        }

        /// <summary>
        /// 3 haneli CVV üretir
        /// </summary>
        public static string GenerateCVV()
        {
            return _random.Next(100, 1000).ToString();
        }

        /// <summary>
        /// Son kullanma tarihi üretir (5 yıl sonrası)
        /// </summary>
        public static DateTime GenerateExpiryDate()
        {
            return DateTime.Now.AddYears(5);
        }

        /// <summary>
        /// Yeni kart oluşturur
        /// </summary>
        public string CreateCard(int hesapID, string kartMarkasi, string kartSahibiAdi, int kullaniciID, out int kartID, out long createdCardNo)
        {
            kartID = 0;
            createdCardNo = 0;
            try
            {
                long kartNo = GenerateCardNumber(kartMarkasi);
                createdCardNo = kartNo;
                string cvv = GenerateCVV();
                DateTime sonKullanmaTarihi = GenerateExpiryDate();
                string kartTipi = kartMarkasi;

                string query = @"INSERT INTO BankaKarti 
                    (HesapID, KartNo, KartTipi, SonKullanmaTarihi, CVV, Durum, BasvuruTarihi, 
                     AktivasyonTarihi, GunlukHarcamaLimiti, AylikHarcamaLimiti, GunlukCekimLimiti, KartSahibiAdi)
                    VALUES 
                    (@hesapID, @kartNo, @kartTipi, @sonKullanma, @cvv, 'Aktif', @basvuruTarihi,
                     @aktivasyonTarihi, 5000, 50000, 3000, @kartSahibiAdi);
                    SELECT LAST_INSERT_ID();";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@hesapID", hesapID),
                    new MySqlParameter("@kartNo", kartNo),
                    new MySqlParameter("@kartTipi", kartTipi),
                    new MySqlParameter("@sonKullanma", sonKullanmaTarihi),
                    new MySqlParameter("@cvv", cvv),
                    new MySqlParameter("@basvuruTarihi", DateTime.Now),
                    new MySqlParameter("@aktivasyonTarihi", DateTime.Now),
                    new MySqlParameter("@kartSahibiAdi", kartSahibiAdi)
                };

                object result;
                string hata = _dataAccess.ExecuteScalar(query, parameters, out result);
                _dataAccess.CloseConnection();

                if (hata == null && result != null)
                {
                    kartID = Convert.ToInt32(result);
                    return null;
                }

                return "Kart oluşturulamadı.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart oluşturma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşterinin tüm kartlarını getirir
        /// </summary>
        public string GetMusteriKartlari(int musteriID, out DataTable kartlar)
        {
            kartlar = null;
            try
            {
                string query = @"SELECT 
                    k.KartID,
                    k.KartNo,
                    k.KartTipi,
                    k.SonKullanmaTarihi,
                    k.Durum,
                    k.KartSahibiAdi,
                    k.GunlukHarcamaLimiti,
                    k.AylikHarcamaLimiti,
                    k.GunlukCekimLimiti,
                    k.HesapID,
                    h.IBAN,
                    h.HesapTipi,
                    h.Bakiye
                FROM BankaKarti k
                INNER JOIN Hesap h ON k.HesapID = h.HesapID
                WHERE h.MusteriID = @musteriID
                ORDER BY k.Durum, k.SonKullanmaTarihi DESC";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriID", musteriID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out kartlar);
                _dataAccess.CloseConnection();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart listesi hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Hesaba ait kartları getirir
        /// </summary>
        public string GetHesapKartlari(int hesapID, out DataTable kartlar)
        {
            kartlar = null;
            try
            {
                string query = @"SELECT 
                    k.KartID,
                    k.KartNo,
                    k.KartTipi,
                    k.SonKullanmaTarihi,
                    k.Durum,
                    k.KartSahibiAdi,
                    k.GunlukHarcamaLimiti,
                    k.AylikHarcamaLimiti,
                    k.GunlukCekimLimiti
                FROM BankaKarti k
                WHERE k.HesapID = @hesapID
                ORDER BY k.Durum, k.SonKullanmaTarihi DESC";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@hesapID", hesapID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out kartlar);
                _dataAccess.CloseConnection();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart listesi hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kartı iptal eder
        /// </summary>
        public string CancelCard(int kartID)
        {
            try
            {
                string query = @"UPDATE BankaKarti 
                                SET Durum = 'Iptal', IptalTarihi = @iptalTarihi 
                                WHERE KartID = @kartID";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kartID", kartID),
                    new MySqlParameter("@iptalTarihi", DateTime.Now)
                };

                int affected;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affected);
                _dataAccess.CloseConnection();

                if (hata != null) return hata;
                if (affected > 0) return null;
                return "Kart bulunamadı.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart iptal hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kartı bloke eder
        /// </summary>
        public string BlockCard(int kartID)
        {
            try
            {
                string query = @"UPDATE BankaKarti 
                                SET Durum = 'Blokeli' 
                                WHERE KartID = @kartID AND Durum = 'Aktif'";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kartID", kartID)
                };

                int affected;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affected);
                _dataAccess.CloseConnection();

                if (hata != null) return hata;
                if (affected > 0) return null;
                return "Kart bulunamadı veya zaten blokeli.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart bloke hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Blokeli kartı aktifleştirir
        /// </summary>
        public string ActivateCard(int kartID)
        {
            try
            {
                string query = @"UPDATE BankaKarti 
                                SET Durum = 'Aktif', AktivasyonTarihi = @aktivasyonTarihi
                                WHERE KartID = @kartID AND Durum = 'Blokeli'";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kartID", kartID),
                    new MySqlParameter("@aktivasyonTarihi", DateTime.Now)
                };

                int affected;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affected);
                _dataAccess.CloseConnection();

                if (hata != null) return hata;
                if (affected > 0) return null;
                return "Kart bulunamadı veya zaten aktif.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart aktivasyon hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Tek bir kartın detaylarını getirir
        /// </summary>
        public string GetKartDetay(int kartID, out BankaKartiModel kart)
        {
            kart = null;
            try
            {
                string query = @"SELECT 
                    k.KartID, k.HesapID, k.KartNo, k.KartTipi, k.SonKullanmaTarihi,
                    k.CVV, k.Durum, k.BasvuruTarihi, k.AktivasyonTarihi, k.IptalTarihi,
                    k.GunlukHarcamaLimiti, k.AylikHarcamaLimiti, k.GunlukCekimLimiti, k.KartSahibiAdi
                FROM BankaKarti k
                WHERE k.KartID = @kartID";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kartID", kartID)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                _dataAccess.CloseConnection();

                if (hata != null) return hata;

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    kart = new BankaKartiModel
                    {
                        KartID = Convert.ToInt32(row["KartID"]),
                        HesapID = Convert.ToInt32(row["HesapID"]),
                        KartNo = Convert.ToInt64(row["KartNo"]),
                        KartTipi = row["KartTipi"].ToString(),
                        SonKullanmaTarihi = Convert.ToDateTime(row["SonKullanmaTarihi"]),
                        CVV = row["CVV"].ToString(),
                        Durum = row["Durum"].ToString(),
                        KartSahibiAdi = row["KartSahibiAdi"].ToString(),
                        GunlukHarcamaLimiti = Convert.ToDecimal(row["GunlukHarcamaLimiti"]),
                        AylikHarcamaLimiti = Convert.ToDecimal(row["AylikHarcamaLimiti"]),
                        GunlukCekimLimiti = Convert.ToDecimal(row["GunlukCekimLimiti"])
                    };
                    return null;
                }

                return "Kart bulunamadı.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart detay hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşterinin hesaplarını getirir (kart başvurusu için)
        /// </summary>
        public string GetMusteriHesaplari(int musteriID, out DataTable hesaplar)
        {
            hesaplar = null;
            try
            {
                string query = @"SELECT h.HesapID, h.IBAN, h.HesapTipi as HesapTuru, h.Bakiye, h.HesapCinsi as DovizCinsi
                                FROM Hesap h
                                WHERE h.MusteriID = @musteriID AND h.Durum = 'Aktif'
                                ORDER BY h.HesapTipi";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriID", musteriID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out hesaplar);
                _dataAccess.CloseConnection();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Hesap listesi hatası: {ex.Message}";
            }
        }
    }
}
