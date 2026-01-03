using System;
using System.Data;
using MetinBank.Util;
using MySql.Data.MySqlClient;

namespace MetinBank.Service
{
    /// <summary>
    /// Kart işlemleri servis sınıfı
    /// </summary>
    public class SKart
    {
        private readonly DataAccess _dataAccess;
        private static Random _random = new Random();

        public SKart()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// Troy veya Mastercard için kart numarası üretir
        /// </summary>
        /// <param name="kartMarkasi">Troy veya Mastercard</param>
        /// <returns>16 haneli kart numarası</returns>
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
            bool alternate = true;  // Sağdan sola, ilk rakamdan sonraki her 2. rakamı 2 ile çarp

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
                string kartTipi = kartMarkasi; // Troy veya Mastercard

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
                    return null; // Başarılı
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

                if (hata != null)
                {
                    return hata;
                }

                if (affected > 0)
                {
                    return null; // Başarılı
                }

                return "Kart bulunamadı.";
            }
            catch (Exception ex)
            {
                _dataAccess.CloseConnection();
                return $"Kart iptal hatası: {ex.Message}";
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
                string query = @"SELECT h.HesapID, h.IBAN, h.HesapTuru, h.Bakiye, h.DovizCinsi
                                FROM Hesap h
                                WHERE h.MusteriID = @musteriID AND h.Durum = 'Aktif'
                                ORDER BY h.HesapTuru";

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
