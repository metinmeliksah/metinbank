namespace MetinBank.WebAPI.DTOs
{
    /// <summary>
    /// Müşteri şifre oluşturma request
    /// </summary>
    public class MusteriSifreOlusturRequest
    {
        public string MusteriNo { get; set; }
        public long TCKN { get; set; }
        public string YeniSifre { get; set; }
        public string YeniSifreTekrar { get; set; }
    }

    /// <summary>
    /// Müşteri şifre oluşturma response
    /// </summary>
    public class MusteriSifreOlusturResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Müşteri login request (güncellenmiş)
    /// </summary>
    public class MusteriLoginRequest
    {
        public string MusteriNo { get; set; }
        public long TCKN { get; set; }
        public string Sifre { get; set; }
    }

    /// <summary>
    /// Şifre var mı kontrol response
    /// </summary>
    public class SifreVarMiResponse
    {
        public bool Success { get; set; }
        public bool SifreVar { get; set; }
        public string Message { get; set; }
    }
}
