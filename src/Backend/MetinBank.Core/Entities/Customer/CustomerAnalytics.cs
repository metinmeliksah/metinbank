namespace MetinBank.Core.Entities.Customer;

/// <summary>
/// Müşteri analitik bilgileri (Python servisi tarafından hesaplanır)
/// </summary>
public class CustomerAnalytics : BaseEntity
{
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Risk profili (Low, Medium, High)
    /// </summary>
    public string RiskProfile { get; set; } = "Medium";
    
    /// <summary>
    /// Risk skoru (0-100)
    /// </summary>
    public decimal RiskScore { get; set; }
    
    /// <summary>
    /// Tahmini gelir (Bireysel için)
    /// </summary>
    public decimal? PredictedIncome { get; set; }
    
    /// <summary>
    /// Tahmini ciro (Kurumsal için)
    /// </summary>
    public decimal? PredictedRevenue { get; set; }
    
    /// <summary>
    /// Harcama kategorisi (Analytics)
    /// </summary>
    public string? SpendingCategory { get; set; }
    
    /// <summary>
    /// Aylık ortalama harcama
    /// </summary>
    public decimal AverageMonthlySpending { get; set; }
    
    /// <summary>
    /// Son analiz tarihi
    /// </summary>
    public DateTime LastAnalyzedAt { get; set; }
    
    /// <summary>
    /// Kredi uygunluk skoru
    /// </summary>
    public decimal? CreditEligibilityScore { get; set; }
}


