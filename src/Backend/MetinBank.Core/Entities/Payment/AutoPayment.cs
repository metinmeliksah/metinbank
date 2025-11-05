namespace MetinBank.Core.Entities.Payment;

/// <summary>
/// Otomatik ödeme talimatı
/// </summary>
public class AutoPayment : BaseEntity
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
    /// Hesap ID
    /// </summary>
    public Guid AccountId { get; set; }
    
    /// <summary>
    /// Hesap
    /// </summary>
    public virtual Account.Account Account { get; set; } = null!;
    
    /// <summary>
    /// Ödeme tipi (BILL/CREDIT_CARD/LOAN)
    /// </summary>
    public string PaymentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Kurum kodu (Fatura için)
    /// </summary>
    public string? InstitutionCode { get; set; }
    
    /// <summary>
    /// Abone numarası (Fatura için)
    /// </summary>
    public string? SubscriberNumber { get; set; }
    
    /// <summary>
    /// Kredi kartı ID (Kredi kartı ödemesi için)
    /// </summary>
    public Guid? CreditCardId { get; set; }
    
    /// <summary>
    /// Limit (Maximum ödeme tutarı)
    /// </summary>
    public decimal? MaxAmount { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Son çalışma tarihi
    /// </summary>
    public DateTime? LastExecutedAt { get; set; }
}


