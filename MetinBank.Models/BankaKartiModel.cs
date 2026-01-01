using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Banka kartı model sınıfı
    /// </summary>
    public class BankaKartiModel
    {
        public int KartID { get; set; }
        public int HesapID { get; set; }
        public string IBAN { get; set; }
        public string MusteriAdi { get; set; }
        public long KartNo { get; set; }
        public string KartTipi { get; set; } // Debit/Credit
        public DateTime SonKullanmaTarihi { get; set; }
        public string CVV { get; set; } // Encrypted
        public string Durum { get; set; } // Aktif/Pasif/Iptal/Blokeli/KayipCalinti
        public DateTime BasvuruTarihi { get; set; }
        public DateTime? AktivasyonTarihi { get; set; }
        public DateTime? IptalTarihi { get; set; }
        public decimal GunlukHarcamaLimiti { get; set; }
        public decimal AylikHarcamaLimiti { get; set; }
        public decimal GunlukCekimLimiti { get; set; }
        public string KartSahibiAdi { get; set; }

        /// <summary>
        /// Kart aktif mi?
        /// </summary>
        public bool AktifMi
        {
            get { return Durum == "Aktif"; }
        }

        /// <summary>
        /// Kart blokeli mi?
        /// </summary>
        public bool BlokeliMi
        {
            get { return Durum == "Blokeli"; }
        }

        /// <summary>
        /// Kart süre doldu mu?
        /// </summary>
        public bool SuresiDolduMu
        {
            get { return DateTime.Now > SonKullanmaTarihi; }
        }

        /// <summary>
        /// Kart kullanılabilir mi?
        /// </summary>
        public bool KullanilabilirMi
        {
            get { return AktifMi && !SuresiDolduMu; }
        }

        /// <summary>
        /// Kart numarası maskelenmiş
        /// </summary>
        public string KartNoMaskelenmiş
        {
            get
            {
                string kartNoStr = KartNo.ToString("D16");
                return $"**** **** **** {kartNoStr.Substring(12, 4)}";
            }
        }

        /// <summary>
        /// Kart numarası formatlanmış
        /// </summary>
        public string KartNoFormatli
        {
            get
            {
                string kartNoStr = KartNo.ToString("D16");
                return $"{kartNoStr.Substring(0, 4)} {kartNoStr.Substring(4, 4)} " +
                       $"{kartNoStr.Substring(8, 4)} {kartNoStr.Substring(12, 4)}";
            }
        }

        /// <summary>
        /// Son kullanma tarihi formatlanmış (MM/YY)
        /// </summary>
        public string SonKullanmaFormatli
        {
            get { return SonKullanmaTarihi.ToString("MM/yy"); }
        }

        /// <summary>
        /// Kalan süre (ay)
        /// </summary>
        public int KalanAy
        {
            get
            {
                TimeSpan fark = SonKullanmaTarihi - DateTime.Now;
                return fark.Days / 30;
            }
        }

        /// <summary>
        /// Yenileme gerekiyor mu? (6 ay kaldıysa)
        /// </summary>
        public bool YenilemeGerekiyorMu
        {
            get { return KalanAy <= 6 && KalanAy > 0; }
        }

        /// <summary>
        /// Kart durumu açıklama
        /// </summary>
        public string DurumAciklama
        {
            get
            {
                if (SuresiDolduMu)
                    return "Süresi Dolmuş";
                
                if (Durum == "KayipCalinti")
                    return "Kayıp/Çalıntı";
                
                return Durum;
            }
        }
    }
}

