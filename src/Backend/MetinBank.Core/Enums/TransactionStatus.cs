namespace MetinBank.Core.Enums;

/// <summary>
/// İşlem durumu
/// </summary>
public enum TransactionStatus
{
    /// <summary>
    /// Beklemede
    /// </summary>
    Pending = 1,
    
    /// <summary>
    /// Firma onayı bekliyor
    /// </summary>
    PendingFirmApproval = 2,
    
    /// <summary>
    /// Banka onayı bekliyor
    /// </summary>
    PendingBankApproval = 3,
    
    /// <summary>
    /// İnceleme altında
    /// </summary>
    UnderReview = 4,
    
    /// <summary>
    /// İşleniyor
    /// </summary>
    Processing = 5,
    
    /// <summary>
    /// Tamamlandı
    /// </summary>
    Completed = 6,
    
    /// <summary>
    /// Başarısız
    /// </summary>
    Failed = 7,
    
    /// <summary>
    /// İptal edildi
    /// </summary>
    Cancelled = 8,
    
    /// <summary>
    /// Reddedildi
    /// </summary>
    Rejected = 9
}


