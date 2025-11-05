using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Account;

/// <summary>
/// Banka hesabı
/// </summary>
public class Account : BaseEntity
{
    /// <summary>
    /// Hesap numarası (IBAN formatında)
    /// </summary>
    public string AccountNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Hesap tipi
    /// </summary>
    public AccountType AccountType { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public CurrencyCode Currency { get; set; }
    
    /// <summary>
    /// Bakiye
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Kullanılabilir bakiye (Blokeli işlemler düşüldükten sonra)
    /// </summary>
    public decimal AvailableBalance { get; set; }
    
    /// <summary>
    /// Hesap adı (Müşterinin verdiği özel ad)
    /// </summary>
    public string? AccountName { get; set; }
    
    /// <summary>
    /// Vade tarihi (Vadeli hesaplar için)
    /// </summary>
    public DateTime? MaturityDate { get; set; }
    
    /// <summary>
    /// Faiz oranı (Vadeli hesaplar için, %)
    /// </summary>
    public decimal? InterestRate { get; set; }
    
    /// <summary>
    /// Hesap açılış tarihi
    /// </summary>
    public DateTime OpenedAt { get; set; }
    
    /// <summary>
    /// Hesap kapanış tarihi
    /// </summary>
    public DateTime? ClosedAt { get; set; }
    
    /// <summary>
    /// Aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Kredili mevduat hesabı bilgisi
    /// </summary>
    public virtual AccountOverdraft? OverdraftInfo { get; set; }
    
    /// <summary>
    /// İşlemler
    /// </summary>
    public virtual ICollection<Transaction.Transaction> Transactions { get; set; } = new List<Transaction.Transaction>();
    
    /// <summary>
    /// Kartlar
    /// </summary>
    public virtual ICollection<Card.Card> Cards { get; set; } = new List<Card.Card>();

    public Account()
    {
        OpenedAt = DateTime.UtcNow;
        AvailableBalance = Balance;
    }
}


