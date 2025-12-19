using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// Hesap bilgilerini tutan entity sınıfı
    /// </summary>
    public class Account
    {
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? AccountType { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
