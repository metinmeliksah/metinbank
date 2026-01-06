namespace MetinBank.WebAPI.DTOs
{
    public class HavaleRequest
    {
        public int KaynakHesapID { get; set; }
        public string HedefIBAN { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public decimal IslemUcreti { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public string AliciAdi { get; set; } = string.Empty;
        public string ParaBirimi { get; set; } = "TL";
    }

    public class EFTRequest
    {
        public int KaynakHesapID { get; set; }
        public string HedefIBAN { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public decimal IslemUcreti { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public string AliciAdi { get; set; } = string.Empty;
        public string ParaBirimi { get; set; } = "TL";
    }

    public class VirmanRequest
    {
        public int KaynakHesapID { get; set; }
        public int HedefHesapID { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public string ParaBirimi { get; set; } = "TL";
    }

    public class IslemResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public long? IslemID { get; set; }
        public string? IslemReferansNo { get; set; }
        public object? Data { get; set; }
    }
}
