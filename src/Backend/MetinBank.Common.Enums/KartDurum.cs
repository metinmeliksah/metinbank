/*
 * MetinBank - KartDurum Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Kart durumu enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Kart durumları
    /// </summary>
    public enum KartDurum
    {
        /// <summary>
        /// Aktif - Kart kullanılabilir
        /// </summary>
        Aktif = 1,
        
        /// <summary>
        /// Blokeli - Kart geçici olarak engellenmiş
        /// </summary>
        Blokeli = 2,
        
        /// <summary>
        /// İptal - Kart kalıcı olarak iptal edilmiş
        /// </summary>
        Iptal = 3,
        
        /// <summary>
        /// Kayıp - Kart kayıp olarak bildirilmiş
        /// </summary>
        Kayip = 4,
        
        /// <summary>
        /// Çalıntı - Kart çalıntı olarak bildirilmiş
        /// </summary>
        Calinti = 5
    }
}


