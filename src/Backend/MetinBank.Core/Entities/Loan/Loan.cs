using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Loan;

/// <summary>
/// Kredi
/// </summary>
public class Loan : BaseEntity
{
    /// <summary>
    /// Kredi numarası
    /// </summary>
    public string LoanNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Kredi tipi
    /// </summary>
    public LoanType LoanType { get; set; }
    
    /// <summary>
    /// Kredi durumu
    /// </summary>
    public LoanStatus Status { get; set; }
    
    /// <summary>
    /// Kredi tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public CurrencyCode Currency { get; set; }
    
    /// <summary>
    /// Faiz oranı (Yıllık %)
    /// </summary>
    public decimal InterestRate { get; set; }
    
    /// <summary>
    /// Vade (Ay)
    /// </summary>
    public int TermMonths { get; set; }
    
    /// <summary>
    /// Aylık taksit tutarı
    /// </summary>
    public decimal MonthlyInstallment { get; set; }
    
    /// <summary>
    /// Kalan borç
    /// </summary>
    public decimal RemainingDebt { get; set; }
    
    /// <summary>
    /// Başvuru tarihi
    /// </summary>
    public DateTime ApplicationDate { get; set; }
    
    /// <summary>
    /// Onay tarihi
    /// </summary>
    public DateTime? ApprovalDate { get; set; }
    
    /// <summary>
    /// Kullandırım tarihi
    /// </summary>
    public DateTime? DisbursementDate { get; set; }
    
    /// <summary>
    /// İlk ödeme tarihi
    /// </summary>
    public DateTime? FirstPaymentDate { get; set; }
    
    /// <summary>
    /// Son ödeme tarihi
    /// </summary>
    public DateTime? LastPaymentDate { get; set; }
    
    /// <summary>
    /// Kredi skoru (Python servisi tarafından)
    /// </summary>
    public decimal? CreditScore { get; set; }
    
    /// <summary>
    /// Risk değerlendirme sonucu
    /// </summary>
    public string? RiskAssessment { get; set; }
    
    /// <summary>
    /// Taksitler
    /// </summary>
    public virtual ICollection<LoanInstallment> Installments { get; set; } = new List<LoanInstallment>();

    public Loan()
    {
        ApplicationDate = DateTime.UtcNow;
        Status = LoanStatus.Applied;
    }
}


