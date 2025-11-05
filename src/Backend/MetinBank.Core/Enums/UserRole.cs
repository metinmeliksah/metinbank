namespace MetinBank.Core.Enums;

/// <summary>
/// Kullanıcı rolleri (RBAC için)
/// </summary>
public enum UserRole
{
    // Bireysel Müşteri
    /// <summary>
    /// Bireysel Müşteri
    /// </summary>
    RetailCustomer = 1,
    
    // Kurumsal Müşteri Rolleri
    /// <summary>
    /// Firma Yöneticisi (Admin)
    /// </summary>
    CorporateManager = 10,
    
    /// <summary>
    /// Firma Hazırlayıcısı
    /// </summary>
    CorporatePreparer = 11,
    
    /// <summary>
    /// Firma Onaylayıcısı
    /// </summary>
    CorporateApprover = 12,
    
    /// <summary>
    /// Firma Kullanıcısı (Genel)
    /// </summary>
    CorporateUser = 13,
    
    // Banka Personeli Rolleri
    /// <summary>
    /// Şube Çalışanı
    /// </summary>
    BranchEmployee = 20,
    
    /// <summary>
    /// Şube Müdürü
    /// </summary>
    BranchManager = 21,
    
    /// <summary>
    /// Genel Müdürlük Personeli
    /// </summary>
    Headquarters = 22,
    
    /// <summary>
    /// Güvenlik Departmanı
    /// </summary>
    SecurityDepartment = 23,
    
    /// <summary>
    /// Sistem Yöneticisi
    /// </summary>
    SystemAdmin = 30
}


