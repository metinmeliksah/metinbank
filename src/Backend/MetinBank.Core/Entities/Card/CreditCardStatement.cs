namespace MetinBank.Core.Entities.Card;

/// <summary>
/// Kredi kartı ekstresi
/// </summary>
public class CreditCardStatement : BaseEntity
{
    /// <summary>
    /// Kredi kartı bilgisi ID
    /// </summary>
    public Guid CreditCardInfoId { get; set; }
    
    /// <summary>
    /// Kredi kartı bilgisi
    /// </summary>
    public virtual CreditCardInfo CreditCardInfo { get; set; } = null!;
    
    /// <summary>
    /// Ekstre dönemi başlangıcı
    /// </summary>
    public DateTime PeriodStart { get; set; }
    
    /// <summary>
    /// Ekstre dönemi bitişi
    /// </summary>
    public DateTime PeriodEnd { get; set; }
    
    /// <summary>
    /// Toplam borç
    /// </summary>
    public decimal TotalDebt { get; set; }
    
    /// <summary>
    /// Minimum ödeme
    /// </summary>
    public decimal MinimumPayment { get; set; }
    
    /// <summary>
    /// Son ödeme tarihi
    /// </summary>
    public DateTime DueDate { get; set; }
    
    /// <summary>
    /// Önceki bakiye
    /// </summary>
    public decimal PreviousBalance { get; set; }
    
    /// <summary>
    /// Yapılan ödemeler
    /// </summary>
    public decimal Payments { get; set; }
    
    /// <summary>
    /// Yeni işlemler
    /// </summary>
    public decimal NewTransactions { get; set; }
    
    /// <summary>
    /// Faiz tutarı
    /// </summary>
    public decimal InterestAmount { get; set; }
    
    /// <summary>
    /// Kazanılan puan
    /// </summary>
    public decimal PointsEarned { get; set; }
    
    /// <summary>
    /// PDF dosya yolu
    /// </summary>
    public string? PdfFilePath { get; set; }
    
    /// <summary>
    /// Ödendi mi?
    /// </summary>
    public bool IsPaid { get; set; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime? PaidAt { get; set; }
}


