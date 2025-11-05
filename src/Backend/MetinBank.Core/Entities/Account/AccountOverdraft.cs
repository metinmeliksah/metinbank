namespace MetinBank.Core.Entities.Account;

/// <summary>
/// Kredili Mevduat Hesabı (KMH) bilgileri
/// </summary>
public class AccountOverdraft : BaseEntity
{
    /// <summary>
    /// Hesap ID
    /// </summary>
    public Guid AccountId { get; set; }
    
    /// <summary>
    /// Hesap
    /// </summary>
    public virtual Account Account { get; set; } = null!;
    
    /// <summary>
    /// KMH limiti
    /// </summary>
    public decimal OverdraftLimit { get; set; }
    
    /// <summary>
    /// Kullanılan tutar
    /// </summary>
    public decimal UsedAmount { get; set; }
    
    /// <summary>
    /// Faiz oranı (Yıllık %)
    /// </summary>
    public decimal InterestRate { get; set; }
    
    /// <summary>
    /// Başlangıç tarihi
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Bitiş tarihi
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
}


