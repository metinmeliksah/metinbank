namespace MetinBank.WebAPI.DTOs
{
    /// <summary>
    /// IBAN sorgulama request
    /// </summary>
    public class IbanSorguRequest
    {
        public string IBAN { get; set; }
    }

    /// <summary>
    /// IBAN sorgulama response
    /// </summary>
    public class IbanSorguResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MusteriAdi { get; set; }
        public string BankaAdi { get; set; }
    }

    /// <summary>
    /// Hesap açma request
    /// </summary>
    public class HesapAcRequest
    {
        public int MusteriID { get; set; }
        public string HesapTipi { get; set; } // TL, USD, EUR
        public int SubeID { get; set; }
    }

    /// <summary>
    /// Hesap açma response
    /// </summary>
    public class HesapAcResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string HesapNo { get; set; }
        public string IBAN { get; set; }
        public int? HesapID { get; set; }
    }
}
