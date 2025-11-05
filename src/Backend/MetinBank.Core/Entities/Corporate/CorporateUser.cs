using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// Kurumsal kullanıcı (Firma Yöneticisi, Hazırlayıcı, Onaylayıcı)
/// </summary>
public class CorporateUser : BaseEntity
{
    /// <summary>
    /// Müşteri ID (Kurumsal müşteri)
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Ad
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Soyad
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// E-posta
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Telefon
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Rol
    /// </summary>
    public UserRole Role { get; set; }
    
    /// <summary>
    /// Şifre hash
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Onay limiti (TL) - Onaylayıcı için
    /// </summary>
    public decimal? ApprovalLimit { get; set; }
    
    /// <summary>
    /// Yetkiler (JSON formatında özel yetkiler)
    /// </summary>
    public string? Permissions { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Son giriş zamanı
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
}


