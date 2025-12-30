using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Kullanıcı model sınıfı
    /// </summary>
    public class KullaniciModel
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string SifreTuzu { get; set; }
        public int RolID { get; set; }
        public string RolAdi { get; set; }
        public int? SubeID { get; set; }
        public string SubeAdi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public bool AktifMi { get; set; }
        public DateTime? SonGirisTarihi { get; set; }
        public int BasarisizGirisSayisi { get; set; }
        public bool HesapKilitliMi { get; set; }
        public DateTime? SonSifreDegistirmeTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        /// <summary>
        /// Kullanıcının tam adı
        /// </summary>
        public string TamAd
        {
            get { return $"{Ad} {Soyad}"; }
        }

        /// <summary>
        /// Kullanıcı aktif ve kilitli değil mi?
        /// </summary>
        public bool KullanilabilirMi
        {
            get { return AktifMi && !HesapKilitliMi; }
        }

        /// <summary>
        /// Şifre değiştirme gerekiyor mu? (90 gün)
        /// </summary>
        public bool SifreDegistirmeGerekiyorMu
        {
            get
            {
                if (SonSifreDegistirmeTarihi == null)
                    return true;
                
                return (DateTime.Now - SonSifreDegistirmeTarihi.Value).TotalDays > 90;
            }
        }
    }
}

