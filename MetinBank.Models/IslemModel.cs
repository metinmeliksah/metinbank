using System;

namespace MetinBank.Models
{
    /// <summary>
    /// İşlem model sınıfı
    /// </summary>
    public class IslemModel
    {
        public long IslemID { get; set; }
        public string IslemReferansNo { get; set; }
        public int? KaynakHesapID { get; set; }
        public string KaynakIBAN { get; set; }
        public int? HedefHesapID { get; set; }
        public string HedefIBAN { get; set; }
        public string IslemTipi { get; set; } // Yatırma/Çekme/Havale/EFT/Virman/Döviz
        public string IslemAltTipi { get; set; } // Nakit/Transfer/ATM/POS
        public decimal Tutar { get; set; }
        public string ParaBirimi { get; set; }
        public decimal? DovizKuru { get; set; }
        public decimal IslemUcreti { get; set; }
        public DateTime IslemTarihi { get; set; }
        public DateTime ValorTarihi { get; set; }
        public string Aciklama { get; set; }
        public string AliciAdi { get; set; }
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public int SubeID { get; set; }
        public string SubeAdi { get; set; }
        public string OnayDurumu { get; set; } // Beklemede/Onaylandı/Reddedildi/Tamamlandı
        public int? OnaylayanID { get; set; }
        public string OnaylayanAdi { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public string RedNedeni { get; set; }
        public string KanalTipi { get; set; } // Sube/Web/Mobil/ATM
        public string IPAdresi { get; set; }
        public bool BasariliMi { get; set; }
        public string HataMesaji { get; set; }

        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool BasariliIslem
        {
            get { return BasariliMi && OnayDurumu == "Tamamlandı"; }
        }

        /// <summary>
        /// Onay bekliyor mu?
        /// </summary>
        public bool OnayBekliyorMu
        {
            get { return OnayDurumu == "Beklemede"; }
        }

        /// <summary>
        /// Reddedildi mi?
        /// </summary>
        public bool ReddedildiMi
        {
            get { return OnayDurumu == "Reddedildi"; }
        }

        /// <summary>
        /// Toplam tutar (işlem ücreti dahil)
        /// </summary>
        public decimal ToplamTutar
        {
            get { return Tutar + IslemUcreti; }
        }

        /// <summary>
        /// İşlem durumu açıklama
        /// </summary>
        public string DurumAciklama
        {
            get
            {
                if (ReddedildiMi)
                    return $"Reddedildi - {RedNedeni}";
                
                if (OnayBekliyorMu)
                    return "Onay Bekliyor";
                
                if (BasariliIslem)
                    return "Tamamlandı";
                
                return "İşlemde Hata";
            }
        }

        /// <summary>
        /// İşlem tipi açıklama
        /// </summary>
        public string IslemTipiAciklama
        {
            get
            {
                string aciklama = IslemTipi;
                if (!string.IsNullOrEmpty(IslemAltTipi))
                    aciklama += $" ({IslemAltTipi})";
                return aciklama;
            }
        }

        /// <summary>
        /// Tutar formatlanmış
        /// </summary>
        public string TutarFormatli
        {
            get { return $"{Tutar:N2} {ParaBirimi}"; }
        }

        /// <summary>
        /// Havale veya EFT mi?
        /// </summary>
        public bool HavaleVeyaEFTMi
        {
            get { return IslemTipi == "Havale" || IslemTipi == "EFT"; }
        }

        /// <summary>
        /// Onay gerekiyor mu?
        /// </summary>
        public bool OnayGerekiyorMu
        {
            get
            {
                // Havale: 5000 TL üzeri
                if (IslemTipi == "Havale" && Tutar > 5000)
                    return true;
                
                // EFT: 5000 TL üzeri
                if (IslemTipi == "EFT" && Tutar > 5000)
                    return true;
                
                return false;
            }
        }
    }
}

