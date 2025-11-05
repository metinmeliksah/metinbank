/*
 * MetinBank - TransferTip Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Transfer tipi enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Transfer tipleri
    /// </summary>
    public enum TransferTip
    {
        /// <summary>
        /// Virman - Kendi hesaplarım arası transfer
        /// </summary>
        Virman = 1,
        
        /// <summary>
        /// Havale - Aynı banka içi başka müşteriye transfer
        /// </summary>
        Havale = 2,
        
        /// <summary>
        /// EFT - Farklı bankaya transfer
        /// </summary>
        EFT = 3,
        
        /// <summary>
        /// FAST - Hızlı para transferi
        /// </summary>
        FAST = 4,
        
        /// <summary>
        /// SWIFT - Uluslararası transfer
        /// </summary>
        SWIFT = 5
    }
}


