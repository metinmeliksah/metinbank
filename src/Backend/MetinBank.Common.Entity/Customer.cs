/*
 * MetinBank - Musteri Entity
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri entity sınıfı (Bireysel ve Kurumsal)
 * Standartlara uygun: private değişkenler _ ile, property'ler Türkçe PascalCase
 */

using System;
using System.Collections.Generic;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Müşteri Entity (Bireysel ve Kurumsal)
    /// </summary>
    public class Musteri : BaseEntity
    {
        // Private değişkenler - standart: _ ile başlar
        private string _musteriNo;
        private int _musteriTip; // 1:Bireysel, 2:Kurumsal
        private int _durum; // 1:Aktif, 2:Pasif, 3:AskiyaAlinmis, 4:EKYCBekliyor
        private string? _tcKimlikNo;
        private string? _vergiKimlikNo;
        private string? _ad;
        private string? _soyad;
        private string? _sirketAd;
        private string _eposta;
        private string _telefon;
        private string? _adres;
        private string? _sehir;
        private string _ulke;
        private DateTime? _dogumTarih;
        private string _sifreHash;
        private DateTime? _sonGirisTarih;
        private bool _eKYCIleKayit;
        private DateTime? _eKYCTamamlanmaTarih;

        /// <summary>
        /// Müşteri numarası (Otomatik oluşturulan benzersiz numara)
        /// </summary>
        public string MusteriNo
        {
            get { return _musteriNo; }
            set { _musteriNo = value; }
        }

        /// <summary>
        /// Müşteri tipi (1:Bireysel, 2:Kurumsal)
        /// </summary>
        public int MusteriTip
        {
            get { return _musteriTip; }
            set { _musteriTip = value; }
        }

        /// <summary>
        /// Müşteri durumu (1:Aktif, 2:Pasif, 3:Askıya Alınmış, 4:eKYC Bekliyor)
        /// </summary>
        public int Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// TC Kimlik No (Bireysel için) - Şifrelenmiş
        /// </summary>
        public string? TcKimlikNo
        {
            get { return _tcKimlikNo; }
            set { _tcKimlikNo = value; }
        }

        /// <summary>
        /// Vergi Kimlik No (Kurumsal için) - Şifrelenmiş
        /// </summary>
        public string? VergiKimlikNo
        {
            get { return _vergiKimlikNo; }
            set { _vergiKimlikNo = value; }
        }

        /// <summary>
        /// Ad (Bireysel için)
        /// </summary>
        public string? Ad
        {
            get { return _ad; }
            set { _ad = value; }
        }

        /// <summary>
        /// Soyad (Bireysel için)
        /// </summary>
        public string? Soyad
        {
            get { return _soyad; }
            set { _soyad = value; }
        }

        /// <summary>
        /// Şirket Adı (Kurumsal için)
        /// </summary>
        public string? SirketAd
        {
            get { return _sirketAd; }
            set { _sirketAd = value; }
        }

        /// <summary>
        /// E-posta - Şifrelenmiş
        /// </summary>
        public string Eposta
        {
            get { return _eposta; }
            set { _eposta = value; }
        }

        /// <summary>
        /// Cep telefonu - Şifrelenmiş
        /// </summary>
        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value; }
        }

        /// <summary>
        /// Adres
        /// </summary>
        public string? Adres
        {
            get { return _adres; }
            set { _adres = value; }
        }

        /// <summary>
        /// Şehir
        /// </summary>
        public string? Sehir
        {
            get { return _sehir; }
            set { _sehir = value; }
        }

        /// <summary>
        /// Ülke
        /// </summary>
        public string Ulke
        {
            get { return _ulke; }
            set { _ulke = value; }
        }

        /// <summary>
        /// Doğum tarihi (Bireysel için)
        /// </summary>
        public DateTime? DogumTarih
        {
            get { return _dogumTarih; }
            set { _dogumTarih = value; }
        }

        /// <summary>
        /// Şifre hash (PBKDF2/bcrypt)
        /// </summary>
        public string SifreHash
        {
            get { return _sifreHash; }
            set { _sifreHash = value; }
        }

        /// <summary>
        /// Son giriş zamanı
        /// </summary>
        public DateTime? SonGirisTarih
        {
            get { return _sonGirisTarih; }
            set { _sonGirisTarih = value; }
        }

        /// <summary>
        /// eKYC ile mi kaydoldu?
        /// </summary>
        public bool EKYCIleKayit
        {
            get { return _eKYCIleKayit; }
            set { _eKYCIleKayit = value; }
        }

        /// <summary>
        /// eKYC tamamlanma tarihi
        /// </summary>
        public DateTime? EKYCTamamlanmaTarih
        {
            get { return _eKYCTamamlanmaTarih; }
            set { _eKYCTamamlanmaTarih = value; }
        }

        // Public değişkenler - standart: camelCase (Türkçe)
        public long sicilNo; // Sicil numarası
        public string kisaAd; // Kısa ad
        public int subeKod; // Şube kodu

        /// <summary>
        /// Constructor
        /// </summary>
        public Musteri() : base()
        {
            _musteriNo = string.Empty;
            _eposta = string.Empty;
            _telefon = string.Empty;
            _sifreHash = string.Empty;
            _ulke = "TR";
            _durum = 1; // Aktif
            kisaAd = string.Empty;
        }
    }
}

