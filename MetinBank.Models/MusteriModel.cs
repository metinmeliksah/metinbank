using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Müşteri model sınıfı
    /// </summary>
    public class MusteriModel
    {
        public int MusteriID { get; set; }
        public string MusteriNo { get; set; }
        public long TCKN { get; set; }
        public long? VergiNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string AnneAdi { get; set; }
        public string BabaAdi { get; set; }
        public string Cinsiyet { get; set; }
        public string MedeniDurum { get; set; }
        public string Telefon { get; set; }
        public string CepTelefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string PostaKodu { get; set; }
        public decimal GelirDurumu { get; set; }
        public string MeslekBilgisi { get; set; }
        public string MusteriTipi { get; set; } // Bireysel/Kurumsal
        public string MusteriSegmenti { get; set; } // Standart/Premium/VIP
        public DateTime KayitTarihi { get; set; }
        public int KayitSubeID { get; set; }
        public string KayitSubeAdi { get; set; }
        public bool AktifMi { get; set; }
        public DateTime SonGuncellemeTarihi { get; set; }

        /// <summary>
        /// Müşterinin tam adı
        /// </summary>
        public string TamAd
        {
            get { return $"{Ad} {Soyad}"; }
        }

        /// <summary>
        /// Müşterinin yaşı
        /// </summary>
        public int Yas
        {
            get
            {
                if (DogumTarihi == null)
                    return 0;
                
                int yas = DateTime.Now.Year - DogumTarihi.Value.Year;
                if (DogumTarihi.Value > DateTime.Now.AddYears(-yas))
                    yas--;
                return yas;
            }
        }

        /// <summary>
        /// Müşteri bireysel mi?
        /// </summary>
        public bool BireyselMi
        {
            get { return MusteriTipi == "Bireysel"; }
        }

        /// <summary>
        /// Müşteri kurumsal mı?
        /// </summary>
        public bool KurumsalMi
        {
            get { return MusteriTipi == "Kurumsal"; }
        }

        /// <summary>
        /// VIP müşteri mi?
        /// </summary>
        public bool VIPMi
        {
            get { return MusteriSegmenti == "VIP"; }
        }

        /// <summary>
        /// Tam adres
        /// </summary>
        public string TamAdres
        {
            get { return $"{Adres} {Ilce}/{Il}"; }
        }
    }
}

