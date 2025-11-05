namespace MetinBank.Core.Entities.Card;

/// <summary>
/// Kredi kartı özel bilgileri
/// </summary>
public class CreditCardInfo : BaseEntity
{
    /// <summary>
    /// Kart ID
    /// </summary>
    public Guid CardId { get; set; }
    
    /// <summary>
    /// Kart
    /// </summary>
    public virtual Card Card { get; set; } = null!;
    
    /// <summary>
    /// Kredi limiti
    /// </summary>
    public decimal CreditLimit { get; set; }
    
    /// <summary>
    /// Kullanılabilir limit
    /// </summary>
    public decimal AvailableLimit { get; set; }
    
    /// <summary>
    /// Mevcut borç
    /// </summary>
    public decimal CurrentDebt { get; set; }
    
    /// <summary>
    /// Minimum ödeme tutarı
    /// </summary>
    public decimal MinimumPayment { get; set; }
    
    /// <summary>
    /// Hesap kesim günü
    /// </summary>
    public int StatementDay { get; set; }
    
    /// <summary>
    /// Son ödeme günü
    /// </summary>
    public int PaymentDueDay { get; set; }
    
    /// <summary>
    /// Faiz oranı (Aylık %)
    /// </summary>
    public decimal InterestRate { get; set; }
    
    /// <summary>
    /// Gecikme faizi oranı (%)
    /// </summary>
    public decimal LatePaymentFeeRate { get; set; }
    
    /// <summary>
    /// Puan bakiyesi
    /// </summary>
    public decimal PointBalance { get; set; }
    
    /// <summary>
    /// Son ekstre tarihi
    /// </summary>
    public DateTime? LastStatementDate { get; set; }
    
    /// <summary>
    /// Sonraki ekstre tarihi
    /// </summary>
    public DateTime NextStatementDate { get; set; }
    
    /// <summary>
    /// Ekstreler
    /// </summary>
    public virtual ICollection<CreditCardStatement> Statements { get; set; } = new List<CreditCardStatement>();

    public CreditCardInfo()
    {
        AvailableLimit = CreditLimit;
    }
}


