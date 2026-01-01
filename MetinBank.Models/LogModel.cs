using System;

namespace MetinBank.Models
{
    /// <summary>
    /// İşlem log model sınıfı
    /// </summary>
    public class LogModel
    {
        public long LogID { get; set; }
        public int? KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string LogTipi { get; set; } // Islem/Goruntuleme/Sistem/Onay/Guvenlik
        public string IslemTipi { get; set; }
        public string TabloAdi { get; set; }
        public long? KayitID { get; set; }
        public string OncekiDeger { get; set; } // JSON
        public string YeniDeger { get; set; } // JSON
        public string IslemDetay { get; set; }
        public string IPAdresi { get; set; }
        public string MacAdresi { get; set; }
        public string SessionID { get; set; }
        public DateTime Tarih { get; set; }
        public bool BasariliMi { get; set; }
        public string HataMesaji { get; set; }

        /// <summary>
        /// Log başarılı mı?
        /// </summary>
        public bool BasariliIslem
        {
            get { return BasariliMi; }
        }

        /// <summary>
        /// Güvenlik logu mu?
        /// </summary>
        public bool GuvenlikLoguMu
        {
            get { return LogTipi == "Guvenlik"; }
        }

        /// <summary>
        /// Sistem logu mu?
        /// </summary>
        public bool SistemLoguMu
        {
            get { return LogTipi == "Sistem"; }
        }

        /// <summary>
        /// İşlem logu mu?
        /// </summary>
        public bool IslemLoguMu
        {
            get { return LogTipi == "Islem"; }
        }
    }

    /// <summary>
    /// Login log model sınıfı
    /// </summary>
    public class LoginLogModel
    {
        public long LoginLogID { get; set; }
        public int? KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string IslemTipi { get; set; } // Login/Logout/FailedLogin
        public bool BasariliMi { get; set; }
        public string IPAdresi { get; set; }
        public string MacAdresi { get; set; }
        public string Tarayici { get; set; }
        public string Cihaz { get; set; }
        public string IsletimSistemi { get; set; }
        public DateTime Tarih { get; set; }
        public string HataMesaji { get; set; }

        /// <summary>
        /// Login başarılı mı?
        /// </summary>
        public bool LoginBasariliMi
        {
            get { return IslemTipi == "Login" && BasariliMi; }
        }

        /// <summary>
        /// Başarısız giriş mi?
        /// </summary>
        public bool BasarisizGirisMi
        {
            get { return IslemTipi == "FailedLogin"; }
        }

        /// <summary>
        /// Logout mu?
        /// </summary>
        public bool LogoutMu
        {
            get { return IslemTipi == "Logout"; }
        }
    }

    /// <summary>
    /// Güvenlik log model sınıfı
    /// </summary>
    public class GuvenlikLogModel
    {
        public long GuvenlikLogID { get; set; }
        public string OlayTipi { get; set; } // YetkiIhlali/LimitAsimi/SuphelijIslem/CokluGiris
        public int? KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string IPAdresi { get; set; }
        public string OlayDetay { get; set; }
        public string RiskSeviyesi { get; set; } // Dusuk/Orta/Yuksek/Kritik
        public DateTime Tarih { get; set; }
        public bool IslemeAlindiMi { get; set; }
        public DateTime? IslemTarihi { get; set; }

        /// <summary>
        /// Kritik risk mi?
        /// </summary>
        public bool KritikRiskMi
        {
            get { return RiskSeviyesi == "Kritik"; }
        }

        /// <summary>
        /// Yüksek risk mi?
        /// </summary>
        public bool YuksekRiskMi
        {
            get { return RiskSeviyesi == "Yuksek"; }
        }

        /// <summary>
        /// İşleme alındı mı?
        /// </summary>
        public bool IslemeAlindiMiProp
        {
            get { return IslemeAlindiMi; }
        }

        /// <summary>
        /// Beklemede mi?
        /// </summary>
        public bool BeklemedeMi
        {
            get { return !IslemeAlindiMi; }
        }
    }
}

