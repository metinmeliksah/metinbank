/*
 * MetinBank - MusteriTip Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri tipi enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Müşteri tipi: Bireysel veya Kurumsal
    /// </summary>
    public enum MusteriTip
    {
        /// <summary>
        /// Bireysel Müşteri (TCKN ile)
        /// </summary>
        Bireysel = 1,
        
        /// <summary>
        /// Kurumsal Müşteri (VKN ile)
        /// </summary>
        Kurumsal = 2
    }
}

