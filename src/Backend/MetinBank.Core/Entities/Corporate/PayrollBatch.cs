using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// Toplu ödeme batch (Maaş/Tedarikçi)
/// </summary>
public class PayrollBatch : BaseEntity
{
    /// <summary>
    /// Batch numarası
    /// </summary>
    public string BatchNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Müşteri ID (Kurumsal)
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Yükleyen kullanıcı ID
    /// </summary>
    public Guid UploadedByUserId { get; set; }
    
    /// <summary>
    /// Batch tipi (PAYROLL/SUPPLIER)
    /// </summary>
    public string BatchType { get; set; } = string.Empty;
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Toplam tutar
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// Toplam kayıt sayısı
    /// </summary>
    public int TotalRecords { get; set; }
    
    /// <summary>
    /// İşlem durumu
    /// </summary>
    public TransactionStatus Status { get; set; }
    
    /// <summary>
    /// Yükleme zamanı
    /// </summary>
    public DateTime UploadedAt { get; set; }
    
    /// <summary>
    /// İşlem tarihi
    /// </summary>
    public DateTime? ProcessedAt { get; set; }
    
    /// <summary>
    /// Ödeme kalemleri
    /// </summary>
    public virtual ICollection<PayrollItem> Items { get; set; } = new List<PayrollItem>();

    public PayrollBatch()
    {
        UploadedAt = DateTime.UtcNow;
        Status = TransactionStatus.PendingFirmApproval;
    }
}


