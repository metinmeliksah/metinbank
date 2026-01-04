namespace MetinBank.WebAPI.DTOs
{
    /// <summary>
    /// Müşteri doğrulama request (şifre sıfırlama/oluşturma için)
    /// </summary>
    public class MusteriDogrulamaRequest
    {
        public string TCKNVeyaMusteriNo { get; set; } // TCKN veya Müşteri No
        public string DogumTarihi { get; set; } // YYYY-MM-DD formatında
        public string AnneAdi { get; set; }
        public string CepTelefon { get; set; } // 05XXXXXXXXX formatında
    }

    /// <summary>
    /// Müşteri doğrulama response
    /// </summary>
    public class MusteriDogrulamaResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? MusteriID { get; set; }
        public string MusteriNo { get; set; }
        public long? TCKN { get; set; }
    }

    /// <summary>
    /// Şifre sıfırlama/oluşturma request
    /// </summary>
    public class SifreSifirlaRequest
    {
        public int MusteriID { get; set; }
        public string YeniSifre { get; set; }
        public string YeniSifreTekrar { get; set; }
    }
}
