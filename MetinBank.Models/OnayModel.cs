using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Onay model sınıfı
    /// </summary>
    public class OnayModel
    {
        public int OnayLogID { get; set; }
        public long? IslemID { get; set; }
        public string IslemReferansNo { get; set; }
        public string IslemTipi { get; set; }
        public int TalepEdenID { get; set; }
        public string TalepEdenAdi { get; set; }
        public int? OnaylayanID { get; set; }
        public string OnaylayanAdi { get; set; }
        public string OnayDurumu { get; set; } // Beklemede/Onaylandı/Reddedildi
        public DateTime? OnayTarihi { get; set; }
        public string RedNedeni { get; set; }
        public DateTime TalepTarihi { get; set; }
        public string BeklenenOnaylayanRol { get; set; }

        // İşlem detayları
        public decimal? IslemTutari { get; set; }
        public string ParaBirimi { get; set; }
        public string KaynakIBAN { get; set; }
        public string HedefIBAN { get; set; }
        public string Aciklama { get; set; }

        /// <summary>
        /// Onay bekliyor mu?
        /// </summary>
        public bool BekliyorMu
        {
            get { return OnayDurumu == "Beklemede"; }
        }

        /// <summary>
        /// Onaylandı mı?
        /// </summary>
        public bool OnaylandiMi
        {
            get { return OnayDurumu == "Onaylandı"; }
        }

        /// <summary>
        /// Reddedildi mi?
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
        /// Zaman aşımına uğradı mı? (48 saat)
        /// </summary>
        public bool ZamanAsimiMi
        {
            get { return BeklemeSuresi > 48 && BekliyorMu; }
        }

        /// <summary>
        /// Acil onay mı? (24 saatten fazla bekliyor)
        /// </summary>
        public bool AcilMi
        {
            get { return BeklemeSuresi > 24 && BekliyorMu; }
        }

        /// <summary>
        /// Tutar formatlanmış
        /// </summary>
        public string TutarFormatli
        {
            get
            {
                if (IslemTutari.HasValue)
                    return $"{IslemTutari:N2} {ParaBirimi}";
                return "-";
            }
        }

        /// <summary>
        /// Durum açıklama
        /// </summary>
        public string DurumAciklama
        {
            get
            {
                if (ReddedildiMi)
                    return $"Reddedildi - {RedNedeni}";
                
                if (OnaylandiMi)
                    return "Onaylandı";
                
                if (ZamanAsimiMi)
                    return "Zaman Aşımı";
                
                if (AcilMi)
                    return "Acil Onay Bekliyor";
                
                return "Onay Bekliyor";
            }
        }

        /// <summary>
        /// Onay seviyesi açıklama
        /// </summary>
        public string OnaySevisyesiAciklama
        {
            get
            {
                if (IslemTutari.HasValue && IslemTutari.Value > 10000)
                    return "Genel Merkez Onayı Gerekli";
                
                if (IslemTutari.HasValue && IslemTutari.Value > 5000)
                    return "Müdür Onayı Gerekli";
                
                return BeklenenOnaylayanRol;
            }
        }
    }
}

