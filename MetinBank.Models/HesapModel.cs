using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Hesap model sınıfı
    /// </summary>
    public class HesapModel
    {
        public int HesapID { get; set; }
        public long HesapNo { get; set; }
        public string IBAN { get; set; }
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public long MusteriTCKN { get; set; }
        public string HesapTipi { get; set; } // TL/USD/EUR
        public string HesapCinsi { get; set; } // Vadesiz/Vadeli/Cocuk/Maas/Yatirim
        public decimal Bakiye { get; set; }
        public decimal BlokeBakiye { get; set; }
        public decimal KullanilabilirBakiye { get; set; }
        public decimal FaizOrani { get; set; }
        public DateTime? VadeTarihi { get; set; }
        public DateTime AcilisTarihi { get; set; }
        public DateTime? KapanisTarihi { get; set; }
        public string Durum { get; set; } // Aktif/Pasif/Kapalı/Blokeli
        public int SubeID { get; set; }
        public string SubeAdi { get; set; }
        public decimal GunlukTransferLimiti { get; set; }
        public decimal AylikTransferLimiti { get; set; }
        public int OlusturanKullaniciID { get; set; }
        public int? OnaylayanKullaniciID { get; set; }
        public DateTime? OnayTarihi { get; set; }

        /// <summary>
        /// Hesap aktif mi?
        /// </summary>
        public bool AktifMi
        {
            get { return Durum == "Aktif"; }
        }

        /// <summary>
        /// Hesap vadeli mi?
        /// </summary>
        public bool VadeliMi
        {
            get { return HesapCinsi == "Vadeli"; }
        }

        /// <summary>
        /// Vade doldu mu?
        /// </summary>
        public bool VadeDolduMu
        {
            get
            {
                if (VadeTarihi == null)
                    return false;
                return DateTime.Now >= VadeTarihi.Value;
            }
        }

        /// <summary>
        /// Hesap blokeli mi?
        /// </summary>
        public bool BlokeliMi
        {
            get { return Durum == "Blokeli"; }
        }

        /// <summary>
        /// İşlem yapılabilir mi?
        /// </summary>
        public bool IslemYapilabilirMi
        {
            get { return Durum == "Aktif" && !BlokeliMi; }
        }

        /// <summary>
        /// IBAN formatlanmış
        /// </summary>
        public string IBANFormatli
        {
            get
            {
                if (string.IsNullOrEmpty(IBAN) || IBAN.Length != 26)
                    return IBAN;
                
                return $"{IBAN.Substring(0, 4)} {IBAN.Substring(4, 4)} {IBAN.Substring(8, 4)} " +
                       $"{IBAN.Substring(12, 4)} {IBAN.Substring(16, 4)} {IBAN.Substring(20, 4)} " +
                       $"{IBAN.Substring(24, 2)}";
            }
        }

        /// <summary>
        /// Hesap açıklama
        /// </summary>
        public string HesapAciklama
        {
            get { return $"{HesapTipi} {HesapCinsi} Hesap"; }
        }

        /// <summary>
        /// Kalan vade günü
        /// </summary>
        public int KalanVadeGunu
        {
            get
            {
                if (VadeTarihi == null)
                    return 0;
                
                TimeSpan fark = VadeTarihi.Value - DateTime.Now;
                return fark.Days > 0 ? fark.Days : 0;
            }
        }
    }
}

