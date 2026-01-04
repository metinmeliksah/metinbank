namespace MetinBank.WebAPI.DTOs
{
    public class LoginRequest
    {
        public string MusteriNo { get; set; } = string.Empty;
        public long TCKN { get; set; }
        public string Sifre { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public KullaniciInfo? Kullanici { get; set; }
    }

    public class KullaniciInfo
    {
        public int MusteriID { get; set; }
        public string MusteriNo { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
    }

    public class SifreDegistirRequest
    {
        public int MusteriID { get; set; }
        public string EskiSifre { get; set; } = string.Empty;
        public string YeniSifre { get; set; } = string.Empty;
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
