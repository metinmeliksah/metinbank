using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MetinBank.Util
{
    /// <summary>
    /// Güvenlik işlemleri için yardımcı sınıf (SHA256 Hash, AES Şifreleme)
    /// </summary>
    public static class SecurityHelper
    {
        private static readonly byte[] _aesKey = Encoding.UTF8.GetBytes("MetinBank2024Key!"); // 16 byte
        private static readonly byte[] _aesIV = Encoding.UTF8.GetBytes("MetinBankInitVec"); // 16 byte

        /// <summary>
        /// Random salt üretir (32 karakter)
        /// </summary>
        /// <returns>Random salt string</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Şifreyi SHA256 ile hash'ler (salt ile)
        /// </summary>
        /// <param name="password">Şifre</param>
        /// <param name="salt">Salt değeri</param>
        /// <returns>Hash'lenmiş şifre (64 karakter)</returns>
        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            try
            {
                string saltedPassword = password + salt;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                    byte[] hash = sha256.ComputeHash(bytes);
                    
                    StringBuilder result = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        result.Append(b.ToString("x2"));
                    }
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Şifre hash'leme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Şifre doğrulama
        /// </summary>
        /// <param name="password">Girilen şifre</param>
        /// <param name="hashedPassword">Veritabanındaki hash'lenmiş şifre</param>
        /// <param name="salt">Salt değeri</param>
        /// <returns>Şifre doğru ise true</returns>
        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            string hashOfInput = HashPassword(password, salt);
            return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Metni AES-256 ile şifreler
        /// </summary>
        /// <param name="plainText">Şifrelenecek metin</param>
        /// <returns>Şifrelenmiş metin (Base64)</returns>
        public static string EncryptAES(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = _aesKey;
                    aes.IV = _aesIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AES şifreleme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// AES-256 ile şifrelenmiş metni çözer
        /// </summary>
        /// <param name="cipherText">Şifreli metin (Base64)</param>
        /// <returns>Çözülmüş metin</returns>
        public static string DecryptAES(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            try
            {
                byte[] buffer = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = _aesKey;
                    aes.IV = _aesIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AES şifre çözme hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Şifre güvenlik kontrolü (en az 8 karakter, büyük/küçük harf, rakam, özel karakter)
        /// </summary>
        /// <param name="password">Kontrol edilecek şifre</param>
        /// <returns>Hata mesajı veya null (şifre güvenli ise)</returns>
        public static string ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Şifre boş olamaz.";

            if (password.Length < 8)
                return "Şifre en az 8 karakter olmalıdır.";

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (char.IsSymbol(c) || char.IsPunctuation(c)) hasSpecial = true;
            }

            if (!hasUpper)
                return "Şifre en az bir büyük harf içermelidir.";
            if (!hasLower)
                return "Şifre en az bir küçük harf içermelidir.";
            if (!hasDigit)
                return "Şifre en az bir rakam içermelidir.";
            if (!hasSpecial)
                return "Şifre en az bir özel karakter içermelidir.";

            return null; // Şifre güvenli
        }

        /// <summary>
        /// Random şifre üretir
        /// </summary>
        /// <param name="length">Şifre uzunluğu</param>
        /// <returns>Random şifre</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            // En az birer tane ekle
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(digitChars[random.Next(digitChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Kalanı random ekle
            string allChars = upperChars + lowerChars + digitChars + specialChars;
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Karıştır
            return ShuffleString(password.ToString());
        }

        /// <summary>
        /// String'i karıştırır
        /// </summary>
        private static string ShuffleString(string input)
        {
            char[] array = input.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                char temp = array[k];
                array[k] = array[n];
                array[n] = temp;
            }
            return new string(array);
        }

        /// <summary>
        /// JWT Token için random secret key üretir
        /// </summary>
        /// <returns>Random secret key (Base64)</returns>
        public static string GenerateJwtSecretKey()
        {
            byte[] key = new byte[32]; // 256 bit
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            return Convert.ToBase64String(key);
        }
    }
}

