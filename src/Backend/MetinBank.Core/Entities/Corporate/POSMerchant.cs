namespace MetinBank.Core.Entities.Corporate;

/// <summary>
/// POS Üye işyeri
/// </summary>
public class POSMerchant : BaseEntity
{
    /// <summary>
    /// Müşteri ID (Kurumsal)
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Üye işyeri numarası
    /// </summary>
    public string MerchantNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// POS cihaz ID
    /// </summary>
    public string PosDeviceId { get; set; } = string.Empty;
    
    /// <summary>
    /// İşyeri adı
    /// </summary>
    public string MerchantName { get; set; } = string.Empty;
    
    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; } = string.Empty;
    
    /// <summary>
    /// Telefon
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// MCC kodu (Merchant Category Code)
    /// </summary>
    public string MccCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Komisyon oranı (%)
    /// </summary>
    public decimal CommissionRate { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// POS işlemleri
    /// </summary>
    public virtual ICollection<POSTransaction> Transactions { get; set; } = new List<POSTransaction>();
}


