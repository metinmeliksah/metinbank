namespace MetinBank.Core.Enums;

/// <summary>
/// Kredi durumu
/// </summary>
public enum LoanStatus
{
    /// <summary>
    /// Başvuru yapıldı
    /// </summary>
    Applied = 1,
    
    /// <summary>
    /// Değerlendirme aşamasında
    /// </summary>
    UnderEvaluation = 2,
    
    /// <summary>
    /// Onay bekliyor
    /// </summary>
    PendingApproval = 3,
    
    /// <summary>
    /// Onaylandı
    /// </summary>
    Approved = 4,
    
    /// <summary>
    /// Reddedildi
    /// </summary>
    Rejected = 5,
    
    /// <summary>
    /// Kullandırıldı
    /// </summary>
    Disbursed = 6,
    
    /// <summary>
    /// Aktif (Ödeme sürecinde)
    /// </summary>
    Active = 7,
    
    /// <summary>
    /// Gecikmiş
    /// </summary>
    Overdue = 8,
    
    /// <summary>
    /// Tamamlandı
    /// </summary>
    Closed = 9,
    
    /// <summary>
    /// Takipteki
    /// </summary>
    InCollect = 10
}


