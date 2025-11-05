using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// Toplu ödeme kalemi
/// </summary>
public class PayrollItem : BaseEntity
{
    /// <summary>
    /// Batch ID
    /// </summary>
    public Guid BatchId { get; set; }
    
    /// <summary>
    /// Batch
    /// </summary>
    public virtual PayrollBatch Batch { get; set; } = null!;
    
    /// <summary>
    /// Alıcı IBAN
    /// </summary>
    public string ToIban { get; set; } = string.Empty;
    
    /// <summary>
    /// Alıcı adı
    /// </summary>
    public string ToName { get; set; } = string.Empty;
    
    /// <summary>
    /// Tutar
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Durum
    /// </summary>
    public TransactionStatus Status { get; set; }
    
    /// <summary>
    /// İşlem referans numarası (Tamamlandıktan sonra)
    /// </summary>
    public string? TransactionReference { get; set; }
    
    /// <summary>
    /// Hata mesajı (Varsa)
    /// </summary>
    public string? ErrorMessage { get; set; }

    public PayrollItem()
    {
        Status = TransactionStatus.Pending;
    }
}


