using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Bildirim model sınıfı
    /// </summary>
    public class BildirimModel
    {
        public int BildirimID { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string BildirimTipi { get; set; } // OnayBekliyor/IslemTamamlandi/Uyari
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public bool OkunduMu { get; set; }
        public DateTime? OkunmaTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime? GecerlilikTarihi { get; set; }

        /// <summary>
        /// Okunmamış bildirim mi?
        /// </summary>
        public bool OkunmamisMi
        {
            get { return !OkunduMu; }
        }

        /// <summary>
        /// Geçerlilik süresi doldu mu?
        /// </summary>
        public bool GecerlilikDolduMu
        {
            get
            {
                if (GecerlilikTarihi == null)
                    return false;
                return DateTime.Now > GecerlilikTarihi.Value;
            }
        }

        /// <summary>
        /// Aktif bildirim mi?
        /// </summary>
        public bool AktifMi
        {
            get { return !GecerlilikDolduMu; }
        }

        /// <summary>
        /// Onay bildirimi mi?
        /// </summary>
        public bool OnayBildirimiMi
        {
            get { return BildirimTipi == "OnayBekliyor"; }
        }

        /// <summary>
        /// Uyarı bildirimi mi?
        /// </summary>
        public bool UyariMi
        {
            get { return BildirimTipi == "Uyari"; }
        }

        /// <summary>
        /// Yeni bildirim mi? (son 24 saat)
        /// </summary>
        public bool YeniMi
        {
            get
            {
                TimeSpan fark = DateTime.Now - OlusturmaTarihi;
                return fark.TotalHours < 24;
            }
        }

        /// <summary>
        /// Bildirim yaşı (saat)
        /// </summary>
        public int BildirimYasi
        {
            get
            {
                TimeSpan fark = DateTime.Now - OlusturmaTarihi;
                return (int)fark.TotalHours;
            }
        }

        /// <summary>
        /// Bildirim yaşı açıklama
        /// </summary>
        public string BildirimYasiAciklama
        {
            get
            {
                if (BildirimYasi < 1)
                    return "Az önce";
                
                if (BildirimYasi < 24)
                    return $"{BildirimYasi} saat önce";
                
                int gun = BildirimYasi / 24;
                return $"{gun} gün önce";
            }
        }
    }
}

