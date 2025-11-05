namespace MetinBank.Core.Entities.Customer;

/// <summary>
/// Cihaz kayıt mekanizması (Device fingerprinting)
/// </summary>
public class AuthDevice : BaseEntity
{
    /// <summary>
    /// Kullanıcı ID (Customer veya CorporateUser)
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Cihaz fingerprint (hash)
    /// </summary>
    public string DeviceFingerprint { get; set; } = string.Empty;
    
    /// <summary>
    /// Cihaz adı
    /// </summary>
    public string DeviceName { get; set; } = string.Empty;
    
    /// <summary>
    /// Cihaz tipi (Mobile/Web/Desktop)
    /// </summary>
    public string DeviceType { get; set; } = string.Empty;
    
    /// <summary>
    /// İşletim sistemi
    /// </summary>
    public string? OS { get; set; }
    
    /// <summary>
    /// Tarayıcı bilgisi
    /// </summary>
    public string? Browser { get; set; }
    
    /// <summary>
    /// IP adresi
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Doğrulandı mı?
    /// </summary>
    public bool IsVerified { get; set; }
    
    /// <summary>
    /// Son kullanım zamanı
    /// </summary>
    public DateTime? LastUsedAt { get; set; }
    
    /// <summary>
    /// FCM Token (Push notification için)
    /// </summary>
    public string? FcmToken { get; set; }
}


