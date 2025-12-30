using System;
using System.Text.RegularExpressions;

namespace MetinBank.Util
{
    /// <summary>
    /// Veri doğrulama işlemleri için yardımcı sınıf
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// TCKN (TC Kimlik Numarası) doğrulama
        /// </summary>
        /// <param name="tckn">11 haneli TCKN</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateTCKN(string tckn)
        {
            if (string.IsNullOrWhiteSpace(tckn))
                return "TCKN boş olamaz.";

            // Sadece rakam kontrolü
            if (!Regex.IsMatch(tckn, @"^\d{11}$"))
                return "TCKN 11 haneli rakamlardan oluşmalıdır.";

            // İlk hane 0 olamaz
            if (tckn[0] == '0')
                return "TCKN'nin ilk hanesi 0 olamaz.";

            long tcknNumber;
            if (!long.TryParse(tckn, out tcknNumber))
                return "Geçersiz TCKN formatı.";

            // TCKN algoritması
            int[] digits = new int[11];
            for (int i = 0; i < 11; i++)
            {
                digits[i] = int.Parse(tckn[i].ToString());
            }

            // 10. hane kontrolü
            int sum1 = (digits[0] + digits[2] + digits[4] + digits[6] + digits[8]) * 7;
            int sum2 = digits[1] + digits[3] + digits[5] + digits[7];
            int digit10 = (sum1 - sum2) % 10;

            if (digit10 != digits[9])
                return "TCKN 10. hane kontrolü hatalı.";

            // 11. hane kontrolü
            int sum3 = 0;
            for (int i = 0; i < 10; i++)
            {
                sum3 += digits[i];
            }
            int digit11 = sum3 % 10;

            if (digit11 != digits[10])
                return "TCKN 11. hane kontrolü hatalı.";

            return null; // TCKN geçerli
        }

        /// <summary>
        /// Email adresi doğrulama
        /// </summary>
        /// <param name="email">Email adresi</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email adresi boş olamaz.";

            // Email regex pattern
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            
            if (!Regex.IsMatch(email, pattern))
                return "Geçersiz email formatı.";

            return null; // Email geçerli
        }

        /// <summary>
        /// Telefon numarası doğrulama (Türkiye formatı)
        /// </summary>
        /// <param name="telefon">Telefon numarası</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateTelefon(string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon))
                return "Telefon numarası boş olamaz.";

            // Boşluk ve tire karakterlerini kaldır
            telefon = telefon.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            // 0555 123 45 67 veya 05551234567 formatı
            if (Regex.IsMatch(telefon, @"^0[0-9]{10}$"))
                return null;

            // 0212 123 45 67 veya 02121234567 formatı
            if (Regex.IsMatch(telefon, @"^0[0-9]{10}$"))
                return null;

            return "Geçersiz telefon numarası formatı. (Örnek: 05551234567)";
        }

        /// <summary>
        /// Para birimi miktarı doğrulama
        /// </summary>
        /// <param name="tutar">Tutar</param>
        /// <param name="minTutar">Minimum tutar</param>
        /// <param name="maxTutar">Maximum tutar</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateTutar(decimal tutar, decimal minTutar = 0, decimal maxTutar = decimal.MaxValue)
        {
            if (tutar < 0)
                return "Tutar negatif olamaz.";

            if (tutar < minTutar)
                return $"Tutar en az {minTutar:N2} TL olmalıdır.";

            if (tutar > maxTutar)
                return $"Tutar en fazla {maxTutar:N2} TL olabilir.";

            return null; // Tutar geçerli
        }

        /// <summary>
        /// Bakiye yeterlilik kontrolü
        /// </summary>
        /// <param name="bakiye">Mevcut bakiye</param>
        /// <param name="islemTutari">İşlem tutarı</param>
        /// <returns>Hata mesajı veya null (yeterli ise)</returns>
        public static string ValidateBakiye(decimal bakiye, decimal islemTutari)
        {
            if (bakiye < islemTutari)
                return $"Yetersiz bakiye. Mevcut: {bakiye:N2} TL, Gerekli: {islemTutari:N2} TL";

            return null; // Bakiye yeterli
        }

        /// <summary>
        /// Tarih doğrulama (geçmiş tarih)
        /// </summary>
        /// <param name="tarih">Kontrol edilecek tarih</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidatePastDate(DateTime tarih)
        {
            if (tarih > DateTime.Now)
                return "Tarih gelecekte olamaz.";

            if (tarih < new DateTime(1900, 1, 1))
                return "Tarih çok eski olamaz.";

            return null; // Tarih geçerli
        }

        /// <summary>
        /// Yaş kontrolü (18 yaş üzeri)
        /// </summary>
        /// <param name="dogumTarihi">Doğum tarihi</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateYas(DateTime dogumTarihi)
        {
            int yas = DateTime.Now.Year - dogumTarihi.Year;
            if (dogumTarihi > DateTime.Now.AddYears(-yas)) yas--;

            if (yas < 18)
                return "Müşteri 18 yaşından küçük olamaz.";

            if (yas > 120)
                return "Geçersiz doğum tarihi.";

            return null; // Yaş geçerli
        }

        /// <summary>
        /// String uzunluk doğrulama
        /// </summary>
        /// <param name="value">Değer</param>
        /// <param name="fieldName">Alan adı</param>
        /// <param name="minLength">Minimum uzunluk</param>
        /// <param name="maxLength">Maximum uzunluk</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateLength(string value, string fieldName, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{fieldName} boş olamaz.";

            if (value.Length < minLength)
                return $"{fieldName} en az {minLength} karakter olmalıdır.";

            if (value.Length > maxLength)
                return $"{fieldName} en fazla {maxLength} karakter olabilir.";

            return null; // Uzunluk geçerli
        }

        /// <summary>
        /// Kredi kartı numarası doğrulama (Luhn algoritması)
        /// </summary>
        /// <param name="kartNo">16 haneli kart numarası</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateKartNo(string kartNo)
        {
            if (string.IsNullOrWhiteSpace(kartNo))
                return "Kart numarası boş olamaz.";

            // Boşluk ve tire kaldır
            kartNo = kartNo.Replace(" ", "").Replace("-", "");

            if (!Regex.IsMatch(kartNo, @"^\d{16}$"))
                return "Kart numarası 16 haneli olmalıdır.";

            // Luhn algoritması
            int sum = 0;
            bool alternate = false;

            for (int i = kartNo.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(kartNo[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                alternate = !alternate;
            }

            if (sum % 10 != 0)
                return "Geçersiz kart numarası.";

            return null; // Kart numarası geçerli
        }

        /// <summary>
        /// CVV doğrulama
        /// </summary>
        /// <param name="cvv">3 haneli CVV</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateCVV(string cvv)
        {
            if (string.IsNullOrWhiteSpace(cvv))
                return "CVV boş olamaz.";

            if (!Regex.IsMatch(cvv, @"^\d{3}$"))
                return "CVV 3 haneli olmalıdır.";

            return null; // CVV geçerli
        }

        /// <summary>
        /// Hesap numarası doğrulama
        /// </summary>
        /// <param name="hesapNo">Hesap numarası</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateHesapNo(string hesapNo)
        {
            if (string.IsNullOrWhiteSpace(hesapNo))
                return "Hesap numarası boş olamaz.";

            if (!Regex.IsMatch(hesapNo, @"^\d{16}$"))
                return "Hesap numarası 16 haneli olmalıdır.";

            return null; // Hesap numarası geçerli
        }

        /// <summary>
        /// Boş alan kontrolü
        /// </summary>
        /// <param name="value">Değer</param>
        /// <param name="fieldName">Alan adı</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{fieldName} zorunludur.";

            return null; // Alan dolu
        }

        /// <summary>
        /// Sadece harf kontrolü
        /// </summary>
        /// <param name="value">Değer</param>
        /// <param name="fieldName">Alan adı</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateOnlyLetters(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{fieldName} boş olamaz.";

            if (!Regex.IsMatch(value, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$"))
                return $"{fieldName} sadece harf içermelidir.";

            return null; // Sadece harf
        }

        /// <summary>
        /// Limit aşımı kontrolü
        /// </summary>
        /// <param name="islemTutari">İşlem tutarı</param>
        /// <param name="limit">Limit</param>
        /// <param name="limitTipi">Limit tipi (günlük, aylık, vb.)</param>
        /// <returns>Hata mesajı veya null (geçerli ise)</returns>
        public static string ValidateLimit(decimal islemTutari, decimal limit, string limitTipi)
        {
            if (islemTutari > limit)
                return $"{limitTipi} limit aşıldı. Limit: {limit:N2} TL, İşlem: {islemTutari:N2} TL";

            return null; // Limit uygun
        }
    }
}

