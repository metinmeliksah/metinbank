namespace MetinBank.Core.Enums;

/// <summary>
/// Kart durumu
/// </summary>
public enum CardStatus
{
    /// <summary>
    /// Aktif
    /// </summary>
    Active = 1,
    
    /// <summary>
    /// Pasif
    /// </summary>
    Inactive = 2,
    
    /// <summary>
    /// Bloke
    /// </summary>
    Blocked = 3,
    
    /// <summary>
    /// Kayıp/Çalıntı
    /// </summary>
    Lost = 4,
    
    /// <summary>
    /// Süresi dolmuş
    /// </summary>
    Expired = 5,
    
    /// <summary>
    /// İptal edilmiş
    /// </summary>
    Cancelled = 6
}


