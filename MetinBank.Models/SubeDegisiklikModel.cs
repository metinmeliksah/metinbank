using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Şube değişikliği talep model sınıfı
    /// </summary>
    public class SubeDegisiklikModel
    {
        public int TalepID { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string TalepEdenAdSoyad { get; set; }
        public int MevcutSubeID { get; set; }
        public string MevcutSubeAdi { get; set; }
        public int YeniSubeID { get; set; }
        public string YeniSubeAdi { get; set; }
        public string TalepNedeni { get; set; }
        public string OnayDurumu { get; set; } // Beklemede/Onaylandı/Reddedildi
        public int? OnaylayanID { get; set; }
        public string OnaylayanAdSoyad { get; set; }
        public DateTime TalepTarihi { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public string RedNedeni { get; set; }

        /// <summary>
        /// Talep beklemede mi?
        /// </summary>
        public bool BeklemedeMi
        {
            get { return OnayDurumu == "Beklemede"; }
        }

        /// <summary>
        /// Talep onaylandı mı?
        /// </summary>
        public bool OnaylandiMi
        {
            get { return OnayDurumu == "Onaylandı"; }
        }

        /// <summary>
        /// Talep reddedildi mi?
        /// </summary>
        public bool ReddedildiMi
        {
            get { return OnayDurumu == "Reddedildi"; }
        }

        /// <summary>
        /// Bekleme süresi (saat)
        /// </summary>
        public int BeklemeSuresi
        {
            get
            {
                if (OnayTarihi.HasValue)
                {
                    TimeSpan fark = OnayTarihi.Value - TalepTarihi;
                    return (int)fark.TotalHours;
                }
                else
                {
                    TimeSpan fark = DateTime.Now - TalepTarihi;
                    return (int)fark.TotalHours;
                }
            }
        }

        /// <summary>
        /// Durum açıklaması
        /// </summary>
        public string DurumAciklama
        {
            get
            {
                if (ReddedildiMi)
                    return $"Reddedildi - {RedNedeni}";
                
                if (OnaylandiMi)
                    return "Onaylandı";
                
                if (BeklemeSuresi > 48)
                    return "Uzun Süredir Bekliyor";
                
                return "Onay Bekliyor";
            }
        }

        /// <summary>
        /// Talep özeti
        /// </summary>
        public string TalepOzeti
        {
            get
            {
                return $"{MevcutSubeAdi} → {YeniSubeAdi}";
            }
        }
    }
}
