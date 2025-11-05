/*
 * MetinBank - TransferDurum Enum
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Transfer durumu enum - Türkçe isimlendirme
 */

namespace MetinBank.Common.Enums
{
    /// <summary>
    /// Transfer durumları
    /// </summary>
    public enum TransferDurum
    {
        /// <summary>
        /// Beklemede - İşlem onay bekliyor
        /// </summary>
        Beklemede = 1,
        
        /// <summary>
        /// Başarılı - Transfer tamamlandı
        /// </summary>
        Basarili = 2,
        
        /// <summary>
        /// Başarısız - Transfer gerçekleşemedi
        /// </summary>
        Basarisiz = 3,
        
        /// <summary>
        /// İptal - İşlem iptal edildi
        /// </summary>
        Iptal = 4,
        
        /// <summary>
        /// İade - Transfer iade edildi
        /// </summary>
        Iade = 5
    }
}


