/*
 * MetinBank - Hesap Entity
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Hesap entity sınıfı
 * Standartlara uygun Türkçe isimlendirme
 */

using System;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Banka Hesabı Entity
    /// </summary>
    public class Hesap : BaseEntity
    {
        // Private değişkenler - standart: _ ile başlar (Türkçe)
        private string _hesapNo;
        private Guid _musteriId;
        private int _hesapTip; // 1:Vadesiz, 2:Vadeli, 3:Doviz, 4:KMH
        private int _dovizKod; // 1:TRY, 2:USD, 3:EUR, 4:GBP
        private decimal _bakiye; // Standart: _bakiye (property için)
        private decimal _kullanilabilirBakiye;
        private string? _hesapAd;
        private DateTime? _vadeTarih;
        private decimal? _faizOran;
        private DateTime _acilisTarih;
        private DateTime? _kapanisTarih;

        /// <summary>
        /// Hesap numarası (IBAN formatında)
        /// </summary>
        public string HesapNo
        {
            get { return _hesapNo; }
            set { _hesapNo = value; }
        }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public Guid MusteriId
        {
            get { return _musteriId; }
            set { _musteriId = value; }
        }

        /// <summary>
        /// Hesap tipi (1:Vadesiz, 2:Vadeli, 3:Döviz, 4:KMH)
        /// </summary>
        public int HesapTip
        {
            get { return _hesapTip; }
            set { _hesapTip = value; }
        }

        /// <summary>
        /// Döviz kodu (Para birimi)
        /// </summary>
        public int DovizKod
        {
            get { return _dovizKod; }
            set { _dovizKod = value; }
        }

        /// <summary>
        /// Bakiye (Property ismi standarda göre: Bakiye, private değişken: _bakiye)
        /// </summary>
        public decimal Bakiye
        {
            get { return _bakiye; }
            set { _bakiye = value; }
        }

        /// <summary>
        /// Kullanılabilir bakiye
        /// </summary>
        public decimal KullanilabilirBakiye
        {
            get { return _kullanilabilirBakiye; }
            set { _kullanilabilirBakiye = value; }
        }

        /// <summary>
        /// Hesap adı (Müşterinin verdiği özel ad)
        /// </summary>
        public string? HesapAd
        {
            get { return _hesapAd; }
            set { _hesapAd = value; }
        }

        /// <summary>
        /// Vade tarihi (Vadeli hesaplar için)
        /// </summary>
        public DateTime? VadeTarih
        {
            get { return _vadeTarih; }
            set { _vadeTarih = value; }
        }

        /// <summary>
        /// Faiz oranı (%)
        /// </summary>
        public decimal? FaizOran
        {
            get { return _faizOran; }
            set { _faizOran = value; }
        }

        /// <summary>
        /// Hesap açılış tarihi
        /// </summary>
        public DateTime AcilisTarih
        {
            get { return _acilisTarih; }
            set { _acilisTarih = value; }
        }

        /// <summary>
        /// Hesap kapanış tarihi
        /// </summary>
        public DateTime? KapanisTarih
        {
            get { return _kapanisTarih; }
            set { _kapanisTarih = value; }
        }

        // Public değişkenler - camelCase (Türkçe)
        public string hesapNo; // Standart format
        public long musteriNo; // long tip kullanımı
        public int adresKod;
        public int subeKod; // Şube kodu

        /// <summary>
        /// Constructor
        /// </summary>
        public Hesap() : base()
        {
            _hesapNo = string.Empty;
            _bakiye = 0;
            _kullanilabilirBakiye = 0;
            _acilisTarih = DateTime.UtcNow;
            hesapNo = string.Empty;
        }
    }
}

