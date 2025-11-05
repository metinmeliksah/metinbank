namespace MetinBank.Core.Enums;

/// <summary>
/// Müşteri durumu
/// </summary>
public enum CustomerStatus
{
    /// <summary>
    /// Aktif müşteri
    /// </summary>
    Active = 1,
    
    /// <summary>
    /// Pasif müşteri
    /// </summary>
    Passive = 2,
    
    /// <summary>
    /// Askıya alınmış
    /// </summary>
    Suspended = 3,
    
    /// <summary>
    /// eKYC bekliyor
    /// </summary>
    PendingEKYC = 4
}


