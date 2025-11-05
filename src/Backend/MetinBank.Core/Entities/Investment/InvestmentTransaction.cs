namespace MetinBank.Core.Entities.Investment;

/// <summary>
/// Yatırım işlemi (Alım/Satım)
/// </summary>
public class InvestmentTransaction : BaseEntity
{
    /// <summary>
    /// Yatırım hesabı ID
    /// </summary>
    public Guid InvestmentAccountId { get; set; }
    
    /// <summary>
    /// Yatırım hesabı
    /// </summary>
    public virtual InvestmentAccount InvestmentAccount { get; set; } = null!;
    
    /// <summary>
    /// İşlem tipi (BUY/SELL)
    /// </summary>
    public string TransactionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Varlık kodu
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Varlık adı
    /// </summary>
    public string AssetName { get; set; } = string.Empty;
    
    /// <summary>
    /// Miktar
    /// </summary>
    public decimal Quantity { get; set; }
    
    /// <summary>
    /// Birim fiyat
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Toplam tutar
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// İşlem ücreti
    /// </summary>
    public decimal Fee { get; set; }
    
    /// <summary>
    /// Durum (PENDING/COMPLETED/FAILED)
    /// </summary>
    public string Status { get; set; } = "PENDING";
    
    /// <summary>
    /// İşlem zamanı
    /// </summary>
    public DateTime TransactionTime { get; set; }
    
    /// <summary>
    /// Tamamlanma zamanı
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    public InvestmentTransaction()
    {
        TransactionTime = DateTime.UtcNow;
    }
}


