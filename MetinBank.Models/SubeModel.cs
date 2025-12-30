using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Şube model sınıfı
    /// </summary>
    public class SubeModel
    {
        public int SubeID { get; set; }
        public string SubeKodu { get; set; }
        public string SubeAdi { get; set; }
        public string BolgeKodu { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int? MudurID { get; set; }
        public string MudurAdi { get; set; }
        public decimal KasaBakiyesi { get; set; }
        public DateTime AcilisTarihi { get; set; }
        public bool AktifMi { get; set; }
        public int CalisanSayisi { get; set; }
        public TimeSpan CalismaBaslangic { get; set; }
        public TimeSpan CalismaBitis { get; set; }

        /// <summary>
        /// Şube tam adres
        /// </summary>
        public string TamAdres
        {
            get { return $"{Adres} {Ilce}/{Sehir}"; }
        }

        /// <summary>
        /// Çalışma saatleri
        /// </summary>
        public string CalismaSaatleri
        {
            get
            {
                return $"{CalismaBaslangic:hh\\:mm} - {CalismaBitis:hh\\:mm}";
            }
        }

        /// <summary>
        /// Şu an açık mı?
        /// </summary>
        public bool AcikMi
        {
            get
            {
                if (!AktifMi)
                    return false;
                
                TimeSpan now = DateTime.Now.TimeOfDay;
                return now >= CalismaBaslangic && now <= CalismaBitis;
            }
        }

        /// <summary>
        /// Şube yaşı (yıl)
        /// </summary>
        public int SubeYasi
        {
            get
            {
                return DateTime.Now.Year - AcilisTarihi.Year;
            }
        }

        /// <summary>
        /// Müdür atanmış mı?
        /// </summary>
        public bool MudurAtanmisMi
        {
            get { return MudurID.HasValue && MudurID.Value > 0; }
        }
    }
}

