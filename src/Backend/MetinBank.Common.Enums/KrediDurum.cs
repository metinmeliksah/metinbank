/*
 * MetinBank - KrediDurum Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Kredi durumu enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Kredi durumları
    /// </summary>
    public enum KrediDurum
    {
        /// <summary>
        /// Başvuru Yapıldı - Değerlendirme aşamasında
        /// </summary>
        BasvuruYapildi = 1,
        
        /// <summary>
        /// Onaylandı - Kredi onaylandı
        /// </summary>
        Onaylandi = 2,
        
        /// <summary>
        /// Reddedildi - Kredi başvurusu reddedildi
        /// </summary>
        Reddedildi = 3,
        
        /// <summary>
        /// Aktif - Kredi kullandırıldı, ödeme devam ediyor
        /// </summary>
        Aktif = 4,
        
        /// <summary>
        /// Kapandı - Kredi tamamen ödendi
        /// </summary>
        Kapandi = 5,
        
        /// <summary>
        /// Gecikmiş - Ödemede gecikme var
        /// </summary>
        Gecikmis = 6,
        
        /// <summary>
        /// Takipte - Kredi takip aşamasında
        /// </summary>
        Takipte = 7
    }
}


