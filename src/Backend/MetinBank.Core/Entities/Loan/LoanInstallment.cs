namespace MetinBank.Core.Entities.Loan;

/// <summary>
/// Kredi taksiti
/// </summary>
public class LoanInstallment : BaseEntity
{
    /// <summary>
    /// Kredi ID
    /// </summary>
    public Guid LoanId { get; set; }
    
    /// <summary>
    /// Kredi
    /// </summary>
    public virtual Loan Loan { get; set; } = null!;
    
    /// <summary>
    /// Taksit numarası
    /// </summary>
    public int InstallmentNumber { get; set; }
    
    /// <summary>
    /// Vade tarihi
    /// </summary>
    public DateTime DueDate { get; set; }
    
    /// <summary>
    /// Anaparatutarı
    /// </summary>
    public decimal Principal { get; set; }
    
    /// <summary>
    /// Faiz tutarı
    /// </summary>
    public decimal Interest { get; set; }
    
    /// <summary>
    /// Toplam tutar
    /// </summary>
    public decimal TotalAmount { get; set; }
    
    /// <summary>
    /// Ödendi mi?
    /// </summary>
    public bool IsPaid { get; set; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime? PaidDate { get; set; }
    
    /// <summary>
    /// Ödenen tutar
    /// </summary>
    public decimal? PaidAmount { get; set; }
    
    /// <summary>
    /// Gecikme günü
    /// </summary>
    public int? DelayDays { get; set; }
    
    /// <summary>
    /// Gecikme faizi
    /// </summary>
    public decimal? LateFee { get; set; }
}


