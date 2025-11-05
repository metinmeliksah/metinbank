namespace MetinBank.Core.Enums;

/// <summary>
/// Kredi tipleri
/// </summary>
public enum LoanType
{
    // Bireysel Krediler
    /// <summary>
    /// İhtiyaç Kredisi
    /// </summary>
    PersonalLoan = 1,
    
    /// <summary>
    /// Konut Kredisi
    /// </summary>
    MortgageLoan = 2,
    
    /// <summary>
    /// Taşıt Kredisi
    /// </summary>
    VehicleLoan = 3,
    
    // Ticari Krediler
    /// <summary>
    /// İşletme Kredisi
    /// </summary>
    BusinessLoan = 10,
    
    /// <summary>
    /// Yatırım Kredisi
    /// </summary>
    InvestmentLoan = 11,
    
    /// <summary>
    /// Makine/Ekipman Kredisi
    /// </summary>
    EquipmentLoan = 12,
    
    /// <summary>
    /// İhracat Kredisi
    /// </summary>
    ExportLoan = 13
}


