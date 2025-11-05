/*
 * MetinBank - Genel Helper Sınıfı
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Genel yardımcı metodlar
 * Standart: Helper sınıfları H prefix'i ile başlar
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace MetinBank.Common.Helper
{
    /// <summary>
    /// Genel Helper Sınıfı
    /// Prefix: H (HGenelHelper)
    /// Static metodlar içerir
    /// </summary>
    public static class HGenelHelper
    {
        /// <summary>
        /// String şifreleme (AES-256)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="plainText">Şifrelenecek metin</param>
        /// <param name="key">Şifreleme anahtarı (32 byte)</param>
        /// <returns>Şifrelenmiş metin (Base64)</returns>
        public static string Sifrele(string plainText, string key)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(plainText))
                {
                    throw new Exception("Şifrelenecek metin boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(key) || key.Length != 32)
                {
                    throw new Exception("Anahtar 32 karakter olmalıdır");
                }

                // AES nesnesi oluştur
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = new byte[16]; // IV (Initialization Vector)

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Şifreleme hatası: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// String şifre çözme (AES-256)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="encryptedText">Şifrelenmiş metin (Base64)</param>
        /// <param name="key">Şifreleme anahtarı (32 byte)</param>
        /// <returns>Çözülmüş metin</returns>
        public static string SifreCoz(string encryptedText, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedText))
                {
                    throw new Exception("Şifrelenmiş metin boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(key) || key.Length != 32)
                {
                    throw new Exception("Anahtar 32 karakter olmalıdır");
                }

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = new byte[16];

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Şifre çözme hatası: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// SHA256 Hash oluşturur (Şifre için)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="text">Hash'lenecek metin</param>
        /// <returns>Hash (hex string)</returns>
        public static string Sha256Hash(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new Exception("Hash'lenecek metin boş olamaz");
                }

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(text);
                    byte[] hashBytes = sha256.ComputeHash(bytes);

                    // StringBuilder kullanarak hex string oluştur (Standart)
                    StringBuilder hash = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++) // Standart: counter i
                    {
                        hash.Append(hashBytes[i].ToString("x2"));
                    }

                    return hash.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hash oluşturma hatası: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// IBAN oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="subeKod">Şube kodu (int - standart)</param>
        /// <param name="hesapNo">Hesap numarası (string)</param>
        /// <returns>IBAN formatında hesap numarası</returns>
        public static string IbanOlustur(int subeKod, string hesapNo)
        {
            try
            {
                // Validasyon
                if (subeKod <= 0)
                {
                    throw new Exception("Şube kodu geçersiz");
                }

                if (string.IsNullOrWhiteSpace(hesapNo))
                {
                    throw new Exception("Hesap no boş olamaz");
                }

                // TR + 2 hane kontrol + 5 hane banka + 1 rezerv + 16 hane hesap
                string bankaKod = "00062"; // MetinBank
                string rezerv = "0";

                // Şube kodunu 5 haneye tamamla
                string subeStr = subeKod.ToString().PadLeft(5, '0');

                // Hesap no'yu 11 haneye tamamla
                string hesapStr = hesapNo.PadLeft(11, '0');

                // IBAN kontrol hanesi hesapla (basitleştirilmiş)
                string kontrolHane = IbanKontrolHanesiHesapla(bankaKod + subeStr + rezerv + hesapStr);
                
                if (string.IsNullOrEmpty(kontrolHane))
                {
                    kontrolHane = "00";
                }

                // StringBuilder kullanarak IBAN oluştur (Standart)
                StringBuilder iban = new StringBuilder();
                iban.Append("TR");
                iban.Append(kontrolHane);
                iban.Append(bankaKod);
                iban.Append(subeStr);
                iban.Append(rezerv);
                iban.Append(hesapStr);

                return iban.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("IBAN oluşturma hatası: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// IBAN kontrol hanesi hesaplar
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="hesapBilgi">Hesap bilgisi (banka+şube+hesap)</param>
        /// <returns>2 haneli kontrol kodu</returns>
        private static string IbanKontrolHanesiHesapla(string hesapBilgi)
        {
            try
            {
                // IBAN mod 97 algoritması (basitleştirilmiş)
                // Gerçek uygulamada tam algoritma kullanılmalı
                long hesapNumara = Convert.ToInt64(hesapBilgi + "271500");
                int kontrol = (int)(98 - (hesapNumara % 97));
                return kontrol.ToString("D2");
            }
            catch
            {
                // Hata durumunda varsayılan değer
                return "00";
            }
        }

        /// <summary>
        /// IBAN doğrulama
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="iban">IBAN numarası</param>
        /// <returns>Geçerli ise true, değilse false</returns>
        public static bool IbanDogrula(string iban)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(iban))
                {
                    return false;
                }

                // Boşlukları temizle
                iban = iban.Replace(" ", "").ToUpper();

                // Türkiye IBAN'ı TR ile başlamalı ve 26 karakter olmalı
                if (!iban.StartsWith("TR") || iban.Length != 26)
                {
                    return false;
                }

                // Mod 97 kontrolü
                // Gerçek uygulamada tam algoritma uygulanmalı
                return true;
            }
            catch (Exception ex)
            {
                // Exception yakalandı - geçersiz IBAN
                Console.WriteLine("IBAN doğrulama hatası: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Telefon numarası formatlar
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="telefon">Telefon numarası</param>
        /// <returns>Formatlanmış telefon (0555 123 45 67)</returns>
        public static string TelefonFormatla(string telefon)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(telefon))
                {
                    return string.Empty;
                }

                // Sadece rakamları al
                StringBuilder rakamlar = new StringBuilder();
                for (int i = 0; i < telefon.Length; i++)
                {
                    if (char.IsDigit(telefon[i]))
                    {
                        rakamlar.Append(telefon[i]);
                    }
                }

                string tel = rakamlar.ToString();

                // 11 haneli olmalı (0555 123 45 67)
                if (tel.Length == 11 && tel.StartsWith("0"))
                {
                    StringBuilder formatli = new StringBuilder();
                    formatli.Append(tel.Substring(0, 4)); // 0555
                    formatli.Append(" ");
                    formatli.Append(tel.Substring(4, 3)); // 123
                    formatli.Append(" ");
                    formatli.Append(tel.Substring(7, 2)); // 45
                    formatli.Append(" ");
                    formatli.Append(tel.Substring(9, 2)); // 67

                    return formatli.ToString();
                }

                return tel;
            }
            catch (Exception ex)
            {
                throw new Exception("Telefon formatlama hatası: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Tarih formatlar (dd.MM.yyyy)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="tarih">Tarih</param>
        /// <returns>Formatlanmış tarih</returns>
        public static string TarihFormatla(DateTime tarih)
        {
            return tarih.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Tarih-Saat formatlar (dd.MM.yyyy HH:mm:ss)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="tarih">Tarih</param>
        /// <returns>Formatlanmış tarih-saat</returns>
        public static string TarihSaatFormatla(DateTime tarih)
        {
            return tarih.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Para formatlar (1.234,56 TL)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="tutar">Tutar</param>
        /// <param name="paraBirim">Para birimi (TL, USD, EUR)</param>
        /// <returns>Formatlanmış tutar</returns>
        public static string ParaFormatla(decimal tutar, string paraBirim = "TL")
        {
            StringBuilder formatli = new StringBuilder();
            formatli.Append(tutar.ToString("N2")); // 1,234.56 formatı
            formatli.Append(" ");
            formatli.Append(paraBirim);

            return formatli.ToString();
        }

        /// <summary>
        /// Random string oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="uzunluk">String uzunluğu</param>
        /// <returns>Random string</returns>
        public static string RandomStringOlustur(int uzunluk)
        {
            const string karakterler = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            StringBuilder sonuc = new StringBuilder();

            for (int i = 0; i < uzunluk; i++)
            {
                sonuc.Append(karakterler[rnd.Next(karakterler.Length)]);
            }

            return sonuc.ToString();
        }
    }
}

