namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// Kurumsal onay kuralı (Firma içi onay mekanizması)
/// </summary>
public class CorporateApprovalRule : BaseEntity
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
    /// İşlem tipi (PAYROLL/SUPPLIER_PAYMENT/vb.)
    /// </summary>
    public string TransactionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Minimum tutar
    /// </summary>
    public decimal MinAmount { get; set; }
    
    /// <summary>
    /// Maximum tutar
    /// </summary>
    public decimal MaxAmount { get; set; }
    
    /// <summary>
    /// Gerekli onaylayıcı roller (JSON array)
    /// Örn: ["CorporateApprover-A", "CorporateApprover-B"]
    /// </summary>
    public string RequiredApproverRoles { get; set; } = string.Empty;
    
    /// <summary>
    /// Minimum onaylayıcı sayısı
    /// </summary>
    public int MinApprovers { get; set; } = 1;
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
}


