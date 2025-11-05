namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// POS işlemi
/// </summary>
public class POSTransaction : BaseEntity
{
    /// <summary>
    /// Üye işyeri ID
    /// </summary>
    public Guid MerchantId { get; set; }
    
    /// <summary>
    /// Üye işyeri
    /// </summary>
    public virtual POSMerchant Merchant { get; set; } = null!;
    
    /// <summary>
    /// İşlem referans numarası
    /// </summary>
    public string ReferenceNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Kart son 4 hane (Müşteri kartı)
    /// </summary>
    public string CardLastFour { get; set; } = string.Empty;
    
    /// <summary>
    /// İşlem tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Komisyon tutarı
    /// </summary>
    public decimal CommissionAmount { get; set; }
    
    /// <summary>
    /// Net tutar (İşyerine geçecek)
    /// </summary>
    public decimal NetAmount { get; set; }
    
    /// <summary>
    /// İşlem zamanı
    /// </summary>
    public DateTime TransactionTime { get; set; }
    
    /// <summary>
    /// Durum (SUCCESS/FAILED/REFUNDED)
    /// </summary>
    public string Status { get; set; } = "SUCCESS";
    
    /// <summary>
    /// Hesaba geçti mi?
    /// </summary>
    public bool IsSettled { get; set; }
    
    /// <summary>
    /// Hesaba geçiş tarihi
    /// </summary>
    public DateTime? SettledAt { get; set; }

    public POSTransaction()
    {
        TransactionTime = DateTime.UtcNow;
    }
}


