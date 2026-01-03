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
        private const string METIN_BANK_CODE = "00127";
        private const string COUNTRY_CODE = "TR";
        private const string RESERVE_DIGIT = "0";

        /// <summary>
        /// Yeni IBAN oluşturur
        /// </summary>
        /// <param name="subeKodu">Şube kodu (5 haneli)</param>
        /// <param name="hesapNo">Hesap numarası (11 haneli olacak şekilde)</param>
        /// <returns>IBAN (26 karakter)</returns>
        public static string GenerateIban(string subeKodu, string hesapNo)
        {
            try
            {
                // Şube kodunu 5 haneye tamamla
                subeKodu = subeKodu.PadLeft(5, '0');
                
                // Hesap numarasını 11 haneye tamamla (Toplam 16 hane için: 5 şube + 11 hesap)
                hesapNo = hesapNo.PadLeft(11, '0');

                // Kontrol rakamını hesapla
                string kontrolRakam = CalculateCheckDigits(subeKodu, hesapNo);

                // IBAN'ı birleştir
                // TR + Kontrol(2) + Banka(5) + Rezerv(1) + Şube(5) + Hesap(11) = 26 Karakter
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
        private static string CalculateCheckDigits(string subeKodu, string hesapNo)
        {
            // IBAN'ı yeniden düzenle: Banka+Rezerv+Şube+Hesap+Ülke Kodu(sayısal)+00
            // Banka(5) + Rezerv(1) + Şube(5) + Hesap(11) + TR(numeric) + 00
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
        public static string FormatIban(string iban)
        {
            // Boşlukları kaldır
            iban = iban.Replace(" ", "");

            if (iban.Length != 26)
                return iban;

            // 4'erli gruplar halinde formatla (TR33 0001 0012 3456 7890 1234 56)
            return $"{iban.Substring(0, 4)} {iban.Substring(4, 4)} {iban.Substring(8, 4)} " +
                   $"{iban.Substring(12, 4)} {iban.Substring(16, 4)} {iban.Substring(20, 4)} " +
                   $"{iban.Substring(24, 2)}";
        }

        /// <summary>
        /// IBAN'dan boşlukları kaldırır
        /// </summary>
        public static string RemoveIbanSpaces(string iban)
        {
            return iban?.Replace(" ", "").Trim() ?? string.Empty;
        }

        /// <summary>
        /// IBAN doğrulaması yapar
        /// </summary>
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
        public static string ExtractSubeKodu(string iban)
        {
            iban = RemoveIbanSpaces(iban);
            if (iban.Length != 26)
                return null;

            // TR(2) + Kontrol(2) + Banka(5) + Rezerv(1) + Şube(5)
            // Banka(5) = index 4-8. Rezerv(1) = index 9. Şube(5) = index 10-14.
            return iban.Substring(10, 5);
        }

        /// <summary>
        /// IBAN'dan hesap numarasını çıkarır
        /// </summary>
        public static string ExtractHesapNo(string iban)
        {
            iban = RemoveIbanSpaces(iban);
            if (iban.Length != 26)
                return null;

            // Hesap no = Son 11 karakter (TR standardında banka içi hesap no)
            // Ya da Şube + Hesap olarak 16 hane istenir? 
            // Genelde "Hesap No" sadece müşteri hesabını ifade ederse son 11,
            // ama teknik olarak unique ID ise son 11 yeterli (bizim sistemde).
            return iban.Substring(15, 11);
        }

        /// <summary>
        /// IBAN'ın Metin Bank'a ait olup olmadığını kontrol eder
        /// </summary>
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
        /// </summary>
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
        public static string GenerateNextHesapNo(long sonHesapNo)
        {
            long yeniHesapNo = sonHesapNo + 1;
            return yeniHesapNo.ToString("D11"); // 11 haneye tamamla
        }

        /// <summary>
        /// Test IBAN'ları oluşturur (development için)
        /// </summary>
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
