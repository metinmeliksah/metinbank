using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Payment;

/// <summary>
/// Fatura ödemesi
/// </summary>
public class BillPayment : BaseEntity
{
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Hesap ID (Ödeme yapılan hesap)
    /// </summary>
    public Guid AccountId { get; set; }
    
    /// <summary>
    /// Hesap
    /// </summary>
    public virtual Account.Account Account { get; set; } = null!;
    
    /// <summary>
    /// Kurum kodu (Elektrik, Su, Doğalgaz, vb.)
    /// </summary>
    public string InstitutionCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Kurum adı
    /// </summary>
    public string InstitutionName { get; set; } = string.Empty;
    
    /// <summary>
    /// Abone numarası
    /// </summary>
    public string SubscriberNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Fatura tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Son ödeme tarihi
    /// </summary>
    public DateTime DueDate { get; set; }
    
    /// <summary>
    /// Ödeme zamanı
    /// </summary>
    public DateTime? PaymentTime { get; set; }
    
    /// <summary>
    /// Durum
    /// </summary>
    public TransactionStatus Status { get; set; }
    
    /// <summary>
    /// İşlem referans numarası
    /// </summary>
    public string? TransactionReference { get; set; }
    
    /// <summary>
    /// Otomatik ödeme mi?
    /// </summary>
    public bool IsAutoPay { get; set; }

    public BillPayment()
    {
        Status = TransactionStatus.Pending;
    }
}


