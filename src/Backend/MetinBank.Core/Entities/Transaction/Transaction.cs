using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Transaction;

/// <summary>
/// İşlem (Transfer, Ödeme, vb.)
/// </summary>
public class Transaction : BaseEntity
{
    /// <summary>
    /// İşlem referans numarası (Benzersiz)
    /// </summary>
    public string ReferenceNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// İşlem tipi
    /// </summary>
    public TransactionType TransactionType { get; set; }
    
    /// <summary>
    /// İşlem durumu
    /// </summary>
    public TransactionStatus Status { get; set; }
    
    /// <summary>
    /// Gönderen hesap ID
    /// </summary>
    public Guid? FromAccountId { get; set; }
    
    /// <summary>
    /// Gönderen hesap
    /// </summary>
    public virtual Account.Account? FromAccount { get; set; }
    
    /// <summary>
    /// Alıcı hesap ID
    /// </summary>
    public Guid? ToAccountId { get; set; }
    
    /// <summary>
    /// Alıcı hesap
    /// </summary>
    public virtual Account.Account? ToAccount { get; set; }
    
    /// <summary>
    /// Alıcı IBAN (EFT için)
    /// </summary>
    public string? ToIban { get; set; }
    
    /// <summary>
    /// Alıcı adı
    /// </summary>
    public string? ToName { get; set; }
    
    /// <summary>
    /// Kart ID (Kart işlemleri için)
    /// </summary>
    public Guid? CardId { get; set; }
    
    /// <summary>
    /// Kart
    /// </summary>
    public virtual Card.Card? Card { get; set; }
    
    /// <summary>
    /// İşlem tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public CurrencyCode Currency { get; set; }
    
    /// <summary>
    /// İşlem ücreti
    /// </summary>
    public decimal Fee { get; set; }
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// İşlem zamanı
    /// </summary>
    public DateTime TransactionTime { get; set; }
    
    /// <summary>
    /// Tamamlanma zamanı
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Kanal (Web/Mobile/ATM/Branch)
    /// </summary>
    public string Channel { get; set; } = string.Empty;
    
    /// <summary>
    /// IP adresi
    /// </summary>
    public string? IpAddress { get; set; }
    
    /// <summary>
    /// Risk skoru (Python servisi tarafından hesaplanır)
    /// </summary>
    public decimal? RiskScore { get; set; }
    
    /// <summary>
    /// Risk sonucu (approve/decline/review)
    /// </summary>
    public string? RiskResult { get; set; }
    
    /// <summary>
    /// Onaylar
    /// </summary>
    public virtual ICollection<TransactionApproval> Approvals { get; set; } = new List<TransactionApproval>();

    public Transaction()
    {
        TransactionTime = DateTime.UtcNow;
        Status = TransactionStatus.Pending;
    }
}


