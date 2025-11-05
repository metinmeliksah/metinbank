/*
 * MetinBank - HesapTip Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Hesap tipi enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Hesap tipleri
    /// </summary>
    public enum HesapTip
    {
        /// <summary>
        /// Vadesiz TL Hesabı
        /// </summary>
        Vadesiz = 1,
        
        /// <summary>
        /// Vadeli TL Hesabı
        /// </summary>
        Vadeli = 2,
        
        /// <summary>
        /// Döviz Hesabı (USD, EUR, GBP vb.)
        /// </summary>
        Doviz = 3,
        
        /// <summary>
        /// Kredili Mevduat Hesabı (KMH)
        /// </summary>
        KMH = 4,
        
        /// <summary>
        /// Yatırım Hesabı
        /// </summary>
        Yatirim = 5
    }
}

