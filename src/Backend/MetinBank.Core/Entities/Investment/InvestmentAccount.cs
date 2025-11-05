namespace MetinBank.Core.Entities.Investment;

/// <summary>
/// Yatırım hesabı
/// </summary>
public class InvestmentAccount : BaseEntity
{
    /// <summary>
    /// Hesap numarası
    /// </summary>
    public string AccountNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Bağlı banka hesabı ID (Para transferleri için)
    /// </summary>
    public Guid LinkedAccountId { get; set; }
    
    /// <summary>
    /// Bağlı hesap
    /// </summary>
    public virtual Account.Account LinkedAccount { get; set; } = null!;
    
    /// <summary>
    /// Toplam portföy değeri (TL karşılığı)
    /// </summary>
    public decimal TotalValue { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Varlıklar
    /// </summary>
    public virtual ICollection<InvestmentAsset> Assets { get; set; } = new List<InvestmentAsset>();
    
    /// <summary>
    /// İşlemler
    /// </summary>
    public virtual ICollection<InvestmentTransaction> Transactions { get; set; } = new List<InvestmentTransaction>();
}


