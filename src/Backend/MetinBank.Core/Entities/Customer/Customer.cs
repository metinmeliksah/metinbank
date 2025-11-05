using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Customer;

/// <summary>
/// Müşteri (Bireysel ve Kurumsal için base)
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Müşteri numarası (Otomatik oluşturulan benzersiz numara)
    /// </summary>
    public string CustomerNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Müşteri tipi (Bireysel/Kurumsal)
    /// </summary>
    public CustomerType CustomerType { get; set; }
    
    /// <summary>
    /// Müşteri durumu
    /// </summary>
    public CustomerStatus Status { get; set; }
    
    /// <summary>
    /// TCKN (Bireysel için) - Şifrelenmiş
    /// </summary>
    public string? TcKimlikNo { get; set; }
    
    /// <summary>
    /// VKN (Kurumsal için) - Şifrelenmiş
    /// </summary>
    public string? VergiKimlikNo { get; set; }
    
    /// <summary>
    /// Ad (Bireysel için)
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Soyad (Bireysel için)
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Unvan (Kurumsal için)
    /// </summary>
    public string? CompanyName { get; set; }
    
    /// <summary>
    /// E-posta - Şifrelenmiş
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Cep telefonu - Şifrelenmiş
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Adres
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Şehir
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = "TR";
    
    /// <summary>
    /// Doğum tarihi (Bireysel için)
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Şifre hash (PBKDF2/bcrypt)
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Son giriş zamanı
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
    
    /// <summary>
    /// eKYC ile mi kaydoldu?
    /// </summary>
    public bool RegisteredViaEKYC { get; set; }
    
    /// <summary>
    /// eKYC tamamlanma tarihi
    /// </summary>
    public DateTime? EKYCCompletedAt { get; set; }
    
    /// <summary>
    /// Hesaplar
    /// </summary>
    public virtual ICollection<Account.Account> Accounts { get; set; } = new List<Account.Account>();
    
    /// <summary>
    /// Kartlar
    /// </summary>
    public virtual ICollection<Card.Card> Cards { get; set; } = new List<Card.Card>();
    
    /// <summary>
    /// Krediler
    /// </summary>
    public virtual ICollection<Loan.Loan> Loans { get; set; } = new List<Loan.Loan>();
    
    /// <summary>
    /// Analitik bilgiler
    /// </summary>
    public virtual CustomerAnalytics? Analytics { get; set; }
    
    /// <summary>
    /// Kurumsal kullanıcılar (sadece kurumsal müşteriler için)
    /// </summary>
    public virtual ICollection<Corporate.CorporateUser>? CorporateUsers { get; set; }
}


