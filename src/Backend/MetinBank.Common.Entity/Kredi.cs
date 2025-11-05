/*
 * MetinBank - Kredi Entity
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Kredi entity sınıfı
 * Standartlara uygun Türkçe isimlendirme
 */

using System;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Kredi Entity
    /// </summary>
    public class Kredi : BaseEntity
    {
        // Private değişkenler - standart: _ ile başlar (Türkçe)
        private string _krediNo;
        private Guid _musteriId;
        private Guid _hesapId;
        private int _krediTip; // 1:İhtiyaç, 2:Konut, 3:Taşıt, 4:Ticari, 5:İşletme, 6:Çiftçi
        private int _durum; // 1:Başvuru, 2:Onaylandı, 3:Reddedildi, 4:Aktif, 5:Kapandı, 6:Gecikmiş, 7:Takipte
        private decimal _krediTutar;
        private decimal _kalanBorc;
        private decimal _faizOran; // Yıllık faiz oranı (%)
        private int _vade; // Ay cinsinden vade
        private decimal _taksitTutar;
        private int _odenenTaksit;
        private int _kalanTaksit;
        private DateTime _kullandirmaTarih;
        private DateTime _ilkTaksitTarih;
        private DateTime? _sonOdemeTarih;
        private decimal? _gecikmeZammi;
        private string? _teminat; // Teminat bilgisi
        private string? _aciklama;

        /// <summary>
        /// Kredi numarası (Otomatik oluşturulan)
        /// </summary>
        public string KrediNo
        {
            get { return _krediNo; }
            set { _krediNo = value; }
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
        /// Hesap ID (Kredinin yatırılacağı hesap)
        /// </summary>
        public Guid HesapId
        {
            get { return _hesapId; }
            set { _hesapId = value; }
        }

        /// <summary>
        /// Kredi tipi (1:İhtiyaç, 2:Konut, 3:Taşıt, 4:Ticari)
        /// </summary>
        public int KrediTip
        {
            get { return _krediTip; }
            set { _krediTip = value; }
        }

        /// <summary>
        /// Kredi durumu
        /// </summary>
        public int Durum
        {
            get { return _durum; }
            set { _durum = value; }
        }

        /// <summary>
        /// Kredi tutarı (Anapara)
        /// </summary>
        public decimal KrediTutar
        {
            get { return _krediTutar; }
            set { _krediTutar = value; }
        }

        /// <summary>
        /// Kalan borç tutarı
        /// </summary>
        public decimal KalanBorc
        {
            get { return _kalanBorc; }
            set { _kalanBorc = value; }
        }

        /// <summary>
        /// Faiz oranı (Yıllık %)
        /// </summary>
        public decimal FaizOran
        {
            get { return _faizOran; }
            set { _faizOran = value; }
        }

        /// <summary>
        /// Vade (Ay cinsinden)
        /// </summary>
        public int Vade
        {
            get { return _vade; }
            set { _vade = value; }
        }

        /// <summary>
        /// Aylık taksit tutarı
        /// </summary>
        public decimal TaksitTutar
        {
            get { return _taksitTutar; }
            set { _taksitTutar = value; }
        }

        /// <summary>
        /// Ödenen taksit sayısı
        /// </summary>
        public int OdenenTaksit
        {
            get { return _odenenTaksit; }
            set { _odenenTaksit = value; }
        }

        /// <summary>
        /// Kalan taksit sayısı
        /// </summary>
        public int KalanTaksit
        {
            get { return _kalanTaksit; }
            set { _kalanTaksit = value; }
        }

        /// <summary>
        /// Kullandırma tarihi
        /// </summary>
        public DateTime KullandirmaTarih
        {
            get { return _kullandirmaTarih; }
            set { _kullandirmaTarih = value; }
        }

        /// <summary>
        /// İlk taksit tarihi
        /// </summary>
        public DateTime IlkTaksitTarih
        {
            get { return _ilkTaksitTarih; }
            set { _ilkTaksitTarih = value; }
        }

        /// <summary>
        /// Son ödeme tarihi (Kredi kapandıysa)
        /// </summary>
        public DateTime? SonOdemeTarih
        {
            get { return _sonOdemeTarih; }
            set { _sonOdemeTarih = value; }
        }

        /// <summary>
        /// Gecikme zammı oranı (%)
        /// </summary>
        public decimal? GecikmeZammi
        {
            get { return _gecikmeZammi; }
            set { _gecikmeZammi = value; }
        }

        /// <summary>
        /// Teminat bilgisi
        /// </summary>
        public string? Teminat
        {
            get { return _teminat; }
            set { _teminat = value; }
        }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Aciklama
        {
            get { return _aciklama; }
            set { _aciklama = value; }
        }

        // Public değişkenler - camelCase (Türkçe)
        public string krediNo; // Kredi numarası
        public long musteriNo; // Müşteri no (long tip - standart)
        public string hesapNo; // Hesap no
        public int subeKod; // Şube kodu

        /// <summary>
        /// Constructor
        /// </summary>
        public Kredi() : base()
        {
            _krediNo = string.Empty;
            _krediTutar = 0;
            _kalanBorc = 0;
            _taksitTutar = 0;
            _odenenTaksit = 0;
            _durum = 1; // Başvuru yapıldı
            krediNo = string.Empty;
            hesapNo = string.Empty;
        }
    }
}


