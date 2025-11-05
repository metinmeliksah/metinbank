namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// Dış ticaret işlemleri (Teminat Mektubu, Akreditif)
/// </summary>
public class TradeFinance : BaseEntity
{
    /// <summary>
    /// Müşteri ID (Kurumsal)
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// İşlem tipi (LG: Teminat Mektubu, LC: Akreditif)
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Referans numarası
    /// </summary>
    public string ReferenceNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Tutar
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public string Currency { get; set; } = "TRY";
    
    /// <summary>
    /// Başlangıç tarihi
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Bitiş tarihi
    /// </summary>
    public DateTime ExpiryDate { get; set; }
    
    /// <summary>
    /// Lehdar (Beneficiary)
    /// </summary>
    public string Beneficiary { get; set; } = string.Empty;
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Durum (PENDING/ACTIVE/EXPIRED/CANCELLED)
    /// </summary>
    public string Status { get; set; } = "PENDING";
    
    /// <summary>
    /// Başvuru tarihi
    /// </summary>
    public DateTime ApplicationDate { get; set; }
    
    /// <summary>
    /// Onay tarihi
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    public TradeFinance()
    {
        ApplicationDate = DateTime.UtcNow;
    }
}


