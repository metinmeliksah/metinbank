/*
 * MetinBank - Transfer Entity
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Transfer entity sınıfı (Havale, EFT, Virman)
 * Standartlara uygun Türkçe isimlendirme
 */

using System;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Transfer Entity (Havale, EFT, Virman, FAST, SWIFT)
    /// </summary>
    public class Transfer : BaseEntity
    {
        // Private değişkenler - standart: _ ile başlar (Türkçe)
        private string _transferNo;
        private Guid _gonderenMusteriId;
        private Guid _gonderenHesapId;
        private string _gonderenHesapNo;
        private string _aliciAd;
        private string _aliciHesapNo;
        private string _aliciBankaKod;
        private int _transferTip; // 1:Virman, 2:Havale, 3:EFT, 4:FAST, 5:SWIFT
        private int _durum; // 1:Beklemede, 2:Başarılı, 3:Başarısız, 4:İptal, 5:İade
        private decimal _tutar;
        private string _dovizKod; // TRY, USD, EUR, GBP
        private decimal _komisyon;
        private string? _aciklama;
        private DateTime _islemTarih;
        private DateTime? _gerceklesenTarih;
        private string? _hataMesaj;
        private string? _referansNo; // Banka referans numarası

        /// <summary>
        /// Transfer numarası (Otomatik oluşturulan)
        /// </summary>
        public string TransferNo
        {
            get { return _transferNo; }
            set { _transferNo = value; }
        }

        /// <summary>
        /// Gönderen müşteri ID
        /// </summary>
        public Guid GonderenMusteriId
        {
            get { return _gonderenMusteriId; }
            set { _gonderenMusteriId = value; }
        }

        /// <summary>
        /// Gönderen hesap ID
        /// </summary>
        public Guid GonderenHesapId
        {
            get { return _gonderenHesapId; }
            set { _gonderenHesapId = value; }
        }

        /// <summary>
        /// Gönderen hesap numarası (IBAN)
        /// </summary>
        public string GonderenHesapNo
        {
            get { return _gonderenHesapNo; }
            set { _gonderenHesapNo = value; }
        }

        /// <summary>
        /// Alıcı adı
        /// </summary>
        public string AliciAd
        {
            get { return _aliciAd; }
            set { _aliciAd = value; }
        }

        /// <summary>
        /// Alıcı hesap numarası (IBAN)
        /// </summary>
        public string AliciHesapNo
        {
            get { return _aliciHesapNo; }
            set { _aliciHesapNo = value; }
        }

        /// <summary>
        /// Alıcı banka kodu (EFT için)
        /// </summary>
        public string AliciBankaKod
        {
            get { return _aliciBankaKod; }
            set { _aliciBankaKod = value; }
        }

        /// <summary>
        /// Transfer tipi (1:Virman, 2:Havale, 3:EFT, 4:FAST, 5:SWIFT)
        /// </summary>
        public int TransferTip
        {
            get { return _transferTip; }
            set { _transferTip = value; }
        }

        /// <summary>
        /// Transfer durumu
        /// </summary>
        public int Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// Transfer tutarı
        /// </summary>
        public decimal Tutar
        {
            get { return _tutar; }
            set { _tutar = value; }
        }

        /// <summary>
        /// Döviz kodu (TRY, USD, EUR, GBP)
        /// </summary>
        public string DovizKod
        {
            get { return _dovizKod; }
            set { _dovizKod = value; }
        }

        /// <summary>
        /// Komisyon tutarı
        /// </summary>
        public decimal Komisyon
        {
            get { return _komisyon; }
            set { _komisyon = value; }
        }

        /// <summary>
        /// Transfer açıklaması
        /// </summary>
        public string? Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime IslemTarih
        {
            get { return _islemTarih; }
            set { _islemTarih = value; }
        }

        /// <summary>
        /// Gerçekleşen tarih (Transfer tamamlandığında)
        /// </summary>
        public DateTime? GerceklesenTarih
        {
            get { return _gerceklesenTarih; }
            set { _gerceklesenTarih = value; }
        }

        /// <summary>
        /// Hata mesajı (Başarısız işlemler için)
        /// </summary>
        public string? HataMesaj
        {
            get { return _hataMesaj; }
            set { _hataMesaj = value; }
        }

        /// <summary>
        /// Referans numarası (Banka referansı)
        /// </summary>
        public string? ReferansNo
        {
            get { return _referansNo; }
            set { _referansNo = value; }
        }

        // Public değişkenler - camelCase (Türkçe)
        public string transferNo; // Transfer numarası
        public long gonderenMusteriNo; // Gönderen müşteri no (long tip - standart)
        public string gonderenHesapNo; // Gönderen hesap no
        public string aliciHesapNo; // Alıcı hesap no
        public int subeKod; // Şube kodu

        /// <summary>
        /// Constructor
        /// </summary>
        public Transfer() : base()
        {
            _transferNo = string.Empty;
            _gonderenHesapNo = string.Empty;
            _aliciAd = string.Empty;
            _aliciHesapNo = string.Empty;
            _aliciBankaKod = string.Empty;
            _dovizKod = "TRY";
            _komisyon = 0;
            _tutar = 0;
            _durum = 1; // Beklemede
            _islemTarih = DateTime.Now;
            transferNo = string.Empty;
            gonderenHesapNo = string.Empty;
            aliciHesapNo = string.Empty;
        }
    }
}


