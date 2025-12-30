using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace MetinBank.Util
{
    /// <summary>
    /// IBAN üretim ve doğrulama işlemleri
    /// TR26 karakterlik IBAN: TR + 2 kontrol rakamı + 5 banka + 1 rezerv + 5 şube + 16 hesap
    /// </summary>
    public static class IbanHelper
    {
        private const string METIN_BANK_CODE = "00001";
        private const string COUNTRY_CODE = "TR";
        private const string RESERVE_DIGIT = "0";

        /// <summary>
        /// Yeni IBAN oluşturur
        /// </summary>
        /// <param name="subeKodu">Şube kodu (5 haneli)</param>
        /// <param name="hesapNo">Hesap numarası (16 haneli)</param>
        /// <returns>IBAN (26 karakter)</returns>
        public static string GenerateIban(string subeKodu, string hesapNo)
        {
            try
            {
                // Şube kodunu 5 haneye tamamla
                subeKodu = subeKodu.PadLeft(5, '0');
                
                // Hesap numarasını 16 haneye tamamla
                hesapNo = hesapNo.PadLeft(16, '0');

                // Kontrol rakamını hesapla
                string kontrolRakam = CalculateCheckDigits(subeKodu, hesapNo);

                // IBAN'ı birleştir
                string iban = $"{COUNTRY_CODE}{kontrolRakam}{METIN_BANK_CODE}{RESERVE_DIGIT}{subeKodu}{hesapNo}";

                // Formatla (4'erli gruplar halinde)
                return FormatIban(iban);
            }
            catch (Exception ex)
            {
                throw new Exception($"IBAN oluşturma hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// IBAN kontrol rakamlarını hesaplar (Mod 97 algoritması)
        /// </summary>
        /// <param name="subeKodu">Şube kodu</param>
        /// <param name="hesapNo">Hesap numarası</param>
        /// <returns>2 haneli kontrol rakamı</returns>
        private static string CalculateCheckDigits(string subeKodu, string hesapNo)
        {
            // IBAN'ı yeniden düzenle: Banka+Rezerv+Şube+Hesap+Ülke Kodu(sayısal)+00
            string rearranged = $"{METIN_BANK_CODE}{RESERVE_DIGIT}{subeKodu}{hesapNo}{ConvertLettersToNumbers(COUNTRY_CODE)}00";

            // BigInteger ile Mod 97 hesapla
            BigInteger number = BigInteger.Parse(rearranged);
            BigInteger remainder = number % 97;
            
            int checkDigit = 98 - (int)remainder;

            return checkDigit.ToString("D2");
        }

        /// <summary>
        /// Harfleri sayıya çevirir (A=10, B=11, ..., Z=35)
        /// </summary>
        /// <param name="letters">Harfler</param>
        /// <returns>Sayısal değer</returns>
        private static string ConvertLettersToNumbers(string letters)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in letters.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    // A=10, B=11, ..., Z=35
                    int value = c - 'A' + 10;
                    result.Append(value);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// IBAN'ı formatlar (4'erli gruplar)
        /// </summary>
        /// <param name="iban">Format edilmemiş IBAN</param>
        /// <returns>Format edilmiş IBAN (TR33 0001 0012 3456 7890 1234 56)</returns>
        public static string FormatIban(string iban)
        {
            // Boşlukları kaldır
            iban = iban.Replace(" ", "");

            if (iban.Length != 26)
                return iban;

            // 4'erli gruplar halinde formatla
            return $"{iban.Substring(0, 4)} {iban.Substring(4, 4)} {iban.Substring(8, 4)} " +
                   $"{iban.Substring(12, 4)} {iban.Substring(16, 4)} {iban.Substring(20, 4)} " +
                   $"{iban.Substring(24, 2)}";
        }

        /// <summary>
        /// IBAN'dan boşlukları kaldırır
        /// </summary>
        /// <param name="iban">Format edilmiş IBAN</param>
        /// <returns>Boşluksuz IBAN</returns>
        public static string RemoveIbanSpaces(string iban)
        {
            return iban?.Replace(" ", "").Trim() ?? string.Empty;
        }

        /// <summary>
        /// IBAN doğrulaması yapar
        /// </summary>
        /// <param name="iban">Doğrulanacak IBAN</param>
        /// <returns>Hata mesajı veya null (IBAN geçerli ise)</returns>
        public static string ValidateIban(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return "IBAN boş olamaz.";

            // Boşlukları kaldır
            iban = RemoveIbanSpaces(iban);

            // Uzunluk kontrolü
            if (iban.Length != 26)
                return "IBAN 26 karakter olmalıdır.";

            // Ülke kodu kontrolü
            if (!iban.StartsWith("TR"))
                return "Sadece Türkiye IBAN'ları kabul edilir.";

            // Format kontrolü (sadece harf ve rakam)
            if (!Regex.IsMatch(iban, @"^[A-Z0-9]+$"))
                return "IBAN sadece harf ve rakam içermelidir.";

            // Kontrol rakamı doğrulama
            if (!VerifyCheckDigits(iban))
                return "IBAN kontrol rakamı hatalı.";

            return null; // IBAN geçerli
        }

        /// <summary>
        /// IBAN kontrol rakamlarını doğrular
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <returns>Kontrol rakamı doğru ise true</returns>
        private static bool VerifyCheckDigits(string iban)
        {
            try
            {
                // IBAN'ı yeniden düzenle: Banka+Rezerv+Şube+Hesap+Ülke Kodu+Kontrol Rakamı
                string rearranged = iban.Substring(4) + ConvertLettersToNumbers(iban.Substring(0, 4));

                // Mod 97 hesapla
                BigInteger number = BigInteger.Parse(rearranged);
                BigInteger remainder = number % 97;

                // Geçerli IBAN için remainder 1 olmalı
                return remainder == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// IBAN'dan şube kodunu çıkarır
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <returns>Şube kodu</returns>
        public static string ExtractSubeKodu(string iban)
        {
            iban = RemoveIbanSpaces(iban);
            if (iban.Length != 26)
                return null;

            // TR(2) + Kontrol(2) + Banka(5) + Rezerv(1) + Şube(5)
            return iban.Substring(10, 5);
        }

        /// <summary>
        /// IBAN'dan hesap numarasını çıkarır
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <returns>Hesap numarası</returns>
        public static string ExtractHesapNo(string iban)
        {
            iban = RemoveIbanSpaces(iban);
            if (iban.Length != 26)
                return null;

            // Son 16 karakter
            return iban.Substring(10, 16);
        }

        /// <summary>
        /// IBAN'ın Metin Bank'a ait olup olmadığını kontrol eder
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <returns>Metin Bank IBAN'ı ise true</returns>
        public static bool IsMetinBankIban(string iban)
        {
            iban = RemoveIbanSpaces(iban);
            if (iban.Length != 26)
                return false;

            // Banka kodu kontrolü
            string bankaKodu = iban.Substring(4, 5);
            return bankaKodu == METIN_BANK_CODE;
        }

        /// <summary>
        /// IBAN'ı maskeler (güvenlik için)
        /// Örnek: TR33 0001 0012 3456 **** **** **
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <returns>Maskelenmiş IBAN</returns>
        public static string MaskIban(string iban)
        {
            string formattedIban = FormatIban(RemoveIbanSpaces(iban));
            if (formattedIban.Length < 26)
                return formattedIban;

            // Son 8 karakteri maskele
            return formattedIban.Substring(0, 19) + "**** **** **";
        }

        /// <summary>
        /// Sonraki hesap numarasını üretir
        /// </summary>
        /// <param name="sonHesapNo">Son kullanılan hesap numarası</param>
        /// <returns>Yeni hesap numarası</returns>
        public static string GenerateNextHesapNo(long sonHesapNo)
        {
            long yeniHesapNo = sonHesapNo + 1;
            return yeniHesapNo.ToString("D16"); // 16 haneye tamamla
        }

        /// <summary>
        /// Test IBAN'ları oluşturur (development için)
        /// </summary>
        /// <param name="subeKodu">Şube kodu</param>
        /// <param name="count">Oluşturulacak IBAN sayısı</param>
        /// <returns>IBAN listesi</returns>
        public static string[] GenerateTestIbans(string subeKodu, int count)
        {
            string[] ibans = new string[count];
            for (int i = 0; i < count; i++)
            {
                string hesapNo = (i + 1).ToString("D16");
                ibans[i] = GenerateIban(subeKodu, hesapNo);
            }
            return ibans;
        }
    }
}

