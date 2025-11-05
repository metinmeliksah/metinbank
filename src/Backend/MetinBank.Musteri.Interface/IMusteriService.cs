/*
 * MetinBank - IMusteriService Interface
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri servisi interface'i
 * 
 * STANDART: Interface prefix'i I
 */

using System;
using System.Data;

namespace MetinBank.Musteri.Interface
{
    /// <summary>
    /// Müşteri Service Interface
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    public interface IMusteriService
    {
        /// <summary>
        /// Müşteri ekler
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <param name="telefon">Telefon</param>
        /// <returns>Müşteri No (long tip - standart)</returns>
        long MusteriEkle(string tcKimlikNo, string ad, string soyad, string eposta, string telefon);

        /// <summary>
        /// Müşteri bulur
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <returns>DataTable</returns>
        DataTable MusteriBul(string tcKimlikNo);

        /// <summary>
        /// Müşteri günceller
        /// </summary>
        /// <param name="musteriNo">Müşteri No (long tip - standart)</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <returns>Başarılı ise null, hata ise hata mesajı</returns>
        string MusteriGuncelle(long musteriNo, string ad, string soyad, string eposta);

        /// <summary>
        /// Tüm müşterileri getirir
        /// </summary>
        /// <returns>DataTable</returns>
        DataTable MusterileriGetir();
    }
}

