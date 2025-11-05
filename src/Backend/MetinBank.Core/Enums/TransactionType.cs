namespace MetinBank.Core.Enums;

/// <summary>
/// İşlem tipleri
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Para yatırma
    /// </summary>
    Deposit = 1,
    
    /// <summary>
    /// Para çekme
    /// </summary>
    Withdrawal = 2,
    
    /// <summary>
    /// Havale (Aynı banka içi)
    /// </summary>
    Transfer = 3,
    
    /// <summary>
    /// EFT (Farklı bankaya)
    /// </summary>
    EFT = 4,
    
    /// <summary>
    /// Fatura ödemesi
    /// </summary>
    BillPayment = 5,
    
    /// <summary>
    /// Kredi kartı ödemesi
    /// </summary>
    CreditCardPayment = 6,
    
    /// <summary>
    /// Banka kartı ile alışveriş
    /// </summary>
    DebitCardPurchase = 7,
    
    /// <summary>
    /// Kredi kartı ile alışveriş
    /// </summary>
    CreditCardPurchase = 8,
    
    /// <summary>
    /// Kredi kullandırımı
    /// </summary>
    LoanDisbursement = 9,
    
    /// <summary>
    /// Kredi taksit ödemesi
    /// </summary>
    LoanPayment = 10,
    
    /// <summary>
    /// Yatırım alım
    /// </summary>
    InvestmentBuy = 11,
    
    /// <summary>
    /// Yatırım satım
    /// </summary>
    InvestmentSell = 12,
    
    /// <summary>
    /// ATM işlemi
    /// </summary>
    ATM = 13,
    
    /// <summary>
    /// Toplu ödeme (Maaş/Tedarikçi)
    /// </summary>
    BulkPayment = 14
}


