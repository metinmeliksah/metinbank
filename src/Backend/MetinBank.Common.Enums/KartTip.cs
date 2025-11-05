/*
 * MetinBank - KartTip Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Kart tipi enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Kart tipleri
    /// </summary>
    public enum KartTip
    {
        /// <summary>
        /// Banka Kartı (Debit Card)
        /// </summary>
        BankaKart = 1,
        
        /// <summary>
        /// Kredi Kartı (Credit Card)
        /// </summary>
        KrediKart = 2,
        
        /// <summary>
        /// Sanal Kart (Virtual Card)
        /// </summary>
        SanalKart = 3
    }
}


