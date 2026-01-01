using System;

namespace MetinBank.Models
{
    /// <summary>
    /// Döviz kur model sınıfı
    /// </summary>
    public class DovizKurModel
    {
        public int KurID { get; set; }
        public string ParaBirimi { get; set; }
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
        public DateTime KayitTarihi { get; set; }

        /// <summary>
        /// Alış-satış farkı
        /// </summary>
        public decimal Fark
        {
            get { return SatisFiyati - AlisFiyati; }
        }

        /// <summary>
        /// Spread yüzdesi
        /// </summary>
        public decimal SpreadYuzde
        {
            get
            {
                if (AlisFiyati == 0)
                    return 0;
                return (Fark / AlisFiyati) * 100;
            }
        }

        /// <summary>
        /// Güncel kur mu? (son 1 saat)
        /// </summary>
        public bool GuncelMi
        {
            get
            {
                TimeSpan fark = DateTime.Now - GuncellemeTarihi;
                return fark.TotalHours < 1;
            }
        }

        /// <summary>
        /// Kur yaşı (dakika)
        /// </summary>
        public int KurYasi
        {
            get
            {
                TimeSpan fark = DateTime.Now - GuncellemeTarihi;
                return (int)fark.TotalMinutes;
            }
        }

        /// <summary>
        /// Para birimi simgesi
        /// </summary>
        public string ParaBirimiSimge
        {
            get
            {
                switch (ParaBirimi)
                {
                    case "USD":
                        return "$";
                    case "EUR":
                        return "€";
                    case "GBP":
                        return "£";
                    default:
                        return ParaBirimi;
                }
            }
        }

        /// <summary>
        /// Alış fiyatı formatlanmış
        /// </summary>
        public string AlisFiyatiFormatli
        {
            get { return $"{AlisFiyati:N4} TL"; }
        }

        /// <summary>
        /// Satış fiyatı formatlanmış
        /// </summary>
        public string SatisFiyatiFormatli
        {
            get { return $"{SatisFiyati:N4} TL"; }
        }
    }
}

