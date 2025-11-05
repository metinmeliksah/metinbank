/*
 * MetinBank - Kart Entity
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Kart entity sınıfı (Banka Kartı ve Kredi Kartı)
 * Standartlara uygun Türkçe isimlendirme
 */

using System;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Kart Entity (Banka Kartı ve Kredi Kartı)
    /// </summary>
    public class Kart : BaseEntity
    {
        // Private değişkenler - standart: _ ile başlar (Türkçe)
        private string _kartNo;
        private Guid _hesapId;
        private Guid _musteriId;
        private int _kartTip; // 1:BankaKart, 2:KrediKart, 3:SanalKart
        private int _durum; // 1:Aktif, 2:Blokeli, 3:İptal, 4:Kayıp, 5:Çalıntı
        private string _kartSahibiAd;
        private DateTime _sonKullanmaTarih;
        private string _cvv; // Şifrelenmiş
        private decimal? _gunlukLimit;
        private decimal? _aylikLimit;
        private bool _internetAlisveris;
        private bool _yurtdisiKullanim;
        private bool _temassizOdeme;
        
        // Kredi kartı için ek alanlar
        private decimal? _krediLimit;
        private decimal? _kullanimTutar;
        private decimal? _minimumOdeme;
        private DateTime? _hesapKesimTarih;
        private DateTime? _sonOdemeTarih;

        /// <summary>
        /// Kart numarası (16 haneli, şifrelenmiş)
        /// </summary>
        public string KartNo
        {
            get { return _kartNo; }
            set { _kartNo = value; }
        }

        /// <summary>
        /// Hesap ID
        /// </summary>
        public Guid HesapId
        {
            get { return _hesapId; }
            set { _hesapId = value; }
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
        /// Kart tipi (1:Banka Kartı, 2:Kredi Kartı, 3:Sanal Kart)
        /// </summary>
        public int KartTip
        {
            get { return _kartTip; }
            set { _kartTip = value; }
        }

        /// <summary>
        /// Kart durumu (1:Aktif, 2:Blokeli, 3:İptal, 4:Kayıp, 5:Çalıntı)
        /// </summary>
        public int Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// Kart sahibi adı (Kart üzerindeki isim)
        /// </summary>
        public string KartSahibiAd
        {
            get { return _kartSahibiAd; }
            set { _kartSahibiAd = value; }
        }

        /// <summary>
        /// Son kullanma tarihi (MM/YY)
        /// </summary>
        public DateTime SonKullanmaTarih
        {
            get { return _sonKullanmaTarih; }
            set { _sonKullanmaTarih = value; }
        }

        /// <summary>
        /// CVV (Card Verification Value) - Şifrelenmiş
        /// </summary>
        public string CVV
        {
            get { return _cvv; }
            set { _cvv = value; }
        }

        /// <summary>
        /// Günlük işlem limiti
        /// </summary>
        public decimal? GunlukLimit
        {
            get { return _gunlukLimit; }
            set { _gunlukLimit = value; }
        }

        /// <summary>
        /// Aylık işlem limiti
        /// </summary>
        public decimal? AylikLimit
        {
            get { return _aylikLimit; }
            set { _aylikLimit = value; }
        }

        /// <summary>
        /// İnternet alışverişi aktif mi?
        /// </summary>
        public bool InternetAlisveris
        {
            get { return _internetAlisveris; }
            set { _internetAlisveris = value; }
        }

        /// <summary>
        /// Yurtdışı kullanım aktif mi?
        /// </summary>
        public bool YurtdisiKullanim
        {
            get { return _yurtdisiKullanim; }
            set { _yurtdisiKullanim = value; }
        }

        /// <summary>
        /// Temassız ödeme aktif mi?
        /// </summary>
        public bool TemassizOdeme
        {
            get { return _temassizOdeme; }
            set { _temassizOdeme = value; }
        }

        /// <summary>
        /// Kredi limiti (Kredi kartı için)
        /// </summary>
        public decimal? KrediLimit
        {
            get { return _krediLimit; }
            set { _krediLimit = value; }
        }

        /// <summary>
        /// Kullanılan tutar (Kredi kartı için)
        /// </summary>
        public decimal? KullanimTutar
        {
            get { return _kullanimTutar; }
            set { _kullanimTutar = value; }
        }

        /// <summary>
        /// Minimum ödeme tutarı (Kredi kartı için)
        /// </summary>
        public decimal? MinimumOdeme
        {
            get { return _minimumOdeme; }
            set { _minimumOdeme = value; }
        }

        /// <summary>
        /// Hesap kesim tarihi (Kredi kartı için)
        /// </summary>
        public DateTime? HesapKesimTarih
        {
            get { return _hesapKesimTarih; }
            set { _hesapKesimTarih = value; }
        }

        /// <summary>
        /// Son ödeme tarihi (Kredi kartı için)
        /// </summary>
        public DateTime? SonOdemeTarih
        {
            get { return _sonOdemeTarih; }
            set { _sonOdemeTarih = value; }
        }

        // Public değişkenler - camelCase (Türkçe)
        public string kartNo; // 16 haneli kart no
        public long musteriNo; // Müşteri no (long tip - standart)
        public string hesapNo; // Hesap no
        public int subeKod; // Şube kodu

        /// <summary>
        /// Constructor
        /// </summary>
        public Kart() : base()
        {
            _kartNo = string.Empty;
            _kartSahibiAd = string.Empty;
            _cvv = string.Empty;
            _durum = 1; // Aktif
            _internetAlisveris = true;
            _yurtdisiKullanim = false;
            _temassizOdeme = true;
            kartNo = string.Empty;
            hesapNo = string.Empty;
        }
    }
}


