namespace MetinBank.Core.Entities.Investment;

/// <summary>
/// Yatırım varlığı (Fon, Hisse, Altın vb.)
/// </summary>
public class InvestmentAsset : BaseEntity
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
    /// Varlık tipi (FUND/STOCK/GOLD/SILVER)
    /// </summary>
    public string AssetType { get; set; } = string.Empty;
    
    /// <summary>
    /// Varlık kodu (Örn: "GARAN", "XAU", "FUND001")
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
    /// Ortalama alış fiyatı
    /// </summary>
    public decimal AverageBuyPrice { get; set; }
    
    /// <summary>
    /// Güncel fiyat
    /// </summary>
    public decimal CurrentPrice { get; set; }
    
    /// <summary>
    /// Toplam değer (TL)
    /// </summary>
    public decimal TotalValue { get; set; }
    
    /// <summary>
    /// Kar/Zarar
    /// </summary>
    public decimal ProfitLoss { get; set; }
    
    /// <summary>
    /// Kar/Zarar yüzdesi
    /// </summary>
    public decimal ProfitLossPercentage { get; set; }
    
    /// <summary>
    /// Son güncelleme zamanı
    /// </summary>
    public DateTime LastUpdatedAt { get; set; }
}


