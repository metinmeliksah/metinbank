using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// İşlem bilgilerini tutan entity sınıfı
    /// </summary>
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? TransactionNumber { get; set; }
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        public string? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int BranchId { get; set; }
    }
}
