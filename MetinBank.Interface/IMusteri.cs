using System.Data;
using MetinBank.Models;

namespace MetinBank.Interface
{
    /// <summary>
    /// Müşteri işlemleri interface
    /// </summary>
    public interface IMusteri
    {
        /// <summary>
        /// Yeni müşteri kaydı oluşturur
        /// </summary>
        /// <param name="musteri">Müşteri modeli</param>
        /// <param name="musteriID">Oluşturulan müşteri ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriEkle(MusteriModel musteri, out int musteriID);

        /// <summary>
        /// Müşteri bilgilerini günceller
        /// </summary>
        /// <param name="musteri">Müşteri modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriGuncelle(MusteriModel musteri);

        /// <summary>
        /// Müşteri listesini getirir
        /// </summary>
        /// <param name="musteriListesi">Müşteri DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusterileriGetir(out DataTable musteriListesi);

        /// <summary>
        /// Müşteri detaylarını getirir
        /// </summary>
        /// <param name="musteriID">Müşteri ID</param>
        /// <param name="musteri">Müşteri modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriGetir(int musteriID, out MusteriModel musteri);

        /// <summary>
        /// TCKN ile müşteri arar
        /// </summary>
        /// <param name="tckn">TCKN</param>
        /// <param name="musteri">Müşteri modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriGetirTCKN(long tckn, out MusteriModel musteri);

        /// <summary>
        /// Müşteri numarası ile müşteri arar
        /// </summary>
        /// <param name="musteriNo">Müşteri numarası</param>
        /// <param name="musteri">Müşteri modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriGetirMusteriNo(string musteriNo, out MusteriModel musteri);

        /// <summary>
        /// Müşteriyi pasif eder
        /// </summary>
        /// <param name="musteriID">Müşteri ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriPasifEt(int musteriID, int kullaniciID);

        /// <summary>
        /// Müşteriyi aktif eder
        /// </summary>
        /// <param name="musteriID">Müşteri ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriAktifEt(int musteriID, int kullaniciID);

        /// <summary>
        /// Müşteri arama (ad, soyad, TCKN)
        /// </summary>
        /// <param name="aramaKelimesi">Arama kelimesi</param>
        /// <param name="sonuclar">Sonuç DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriAra(string aramaKelimesi, out DataTable sonuclar);

        /// <summary>
        /// Şubeye göre müşteri listesi
        /// </summary>
        /// <param name="subeID">Şube ID</param>
        /// <param name="musteriListesi">Müşteri DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string SubeyeGoreMusteriler(int subeID, out DataTable musteriListesi);

        /// <summary>
        /// Müşteri istatistikleri
        /// </summary>
        /// <param name="toplamMusteri">Toplam müşteri sayısı</param>
        /// <param name="aktifMusteri">Aktif müşteri sayısı</param>
        /// <param name="vipMusteri">VIP müşteri sayısı</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusteriIstatistikleri(out int toplamMusteri, out int aktifMusteri, out int vipMusteri);
    }
}

