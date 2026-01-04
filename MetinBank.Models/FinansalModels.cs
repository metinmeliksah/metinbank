using System;

namespace MetinBank.Models
{
    public class KrediOranModel
    {
        public int OranID { get; set; }
        public string KrediTipi { get; set; }
        public decimal BaslangicTutar { get; set; }
        public decimal BitisTutar { get; set; }
        public int MinVade { get; set; }
        public int MaxVade { get; set; }
        public decimal FaizOrani { get; set; }
        public bool AktifMi { get; set; }
    }

    public class MevduatOranModel
    {
        public int OranID { get; set; }
        public string ParaBirimi { get; set; }
        public decimal BaslangicTutar { get; set; }
        public decimal BitisTutar { get; set; }
        public int MinGun { get; set; }
        public int MaxGun { get; set; }
        public decimal FaizOrani { get; set; }
        public decimal StopajOrani { get; set; }
        public string Aciklama { get; set; }
        public bool AktifMi { get; set; }
    }

    public class KrediBasvuruModel
    {
        public int BasvuruID { get; set; }
        public int? MusteriID { get; set; }
        public long TCKN { get; set; }
        public string AdSoyad { get; set; }
        public string CepTelefon { get; set; }
        public decimal AylikGelir { get; set; }
        public decimal TalepEdilenTutar { get; set; }
        public int TalepEdilenVade { get; set; }
        public decimal? OnaylananTutar { get; set; }
        public int? OnaylananVade { get; set; }
        public decimal FaizOrani { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public string Kanal { get; set; }
        public string Durum { get; set; }
        public string RedNedeni { get; set; }
        public int? OnaylayanKullaniciID { get; set; }
        public DateTime? OnayTarihi { get; set; }
    }
}
