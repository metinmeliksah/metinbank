namespace MetinBank.Core.Entities.Transaction;

/// <summary>
/// İşlem onayı (Banka veya Firma onayı)
/// </summary>
public class TransactionApproval : BaseEntity
{
    /// <summary>
    /// İşlem ID
    /// </summary>
    public Guid TransactionId { get; set; }
    
    /// <summary>
    /// İşlem
    /// </summary>
    public virtual Transaction Transaction { get; set; } = null!;
    
    /// <summary>
    /// Onay tipi (BANK/FIRM)
    /// </summary>
    public string ApprovalType { get; set; } = string.Empty;
    
    /// <summary>
    /// Onaylayan kullanıcı ID
    /// </summary>
    public Guid ApproverId { get; set; }
    
    /// <summary>
    /// Onaylayan rol
    /// </summary>
    public string ApproverRole { get; set; } = string.Empty;
    
    /// <summary>
    /// Onaylayan adı
    /// </summary>
    public string ApproverName { get; set; } = string.Empty;
    
    /// <summary>
    /// Onaylandı mı?
    /// </summary>
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// Red nedeni
    /// </summary>
    public string? RejectionReason { get; set; }
    
    /// <summary>
    /// Onay zamanı
    /// </summary>
    public DateTime ApprovedAt { get; set; }
    
    /// <summary>
    /// IP adresi
    /// </summary>
    public string? IpAddress { get; set; }
}


