namespace MetinBank.Core.Enums;

/// <summary>
/// Hesap tipleri
/// </summary>
public enum AccountType
{
    /// <summary>
    /// Vadesiz TL Hesabı
    /// </summary>
    DemandDeposit = 1,
    
    /// <summary>
    /// Vadeli TL Hesabı
    /// </summary>
    TimeDeposit = 2,
    
    /// <summary>
    /// Döviz Hesabı (USD, EUR, GBP vb.)
    /// </summary>
    ForeignCurrency = 3,
    
    /// <summary>
    /// Kredili Mevduat Hesabı (KMH)
    /// </summary>
    Overdraft = 4,
    
    /// <summary>
    /// Yatırım Hesabı
    /// </summary>
    Investment = 5
}


