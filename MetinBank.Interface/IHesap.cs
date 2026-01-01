using System.Data;
using MetinBank.Models;

namespace MetinBank.Interface
{
    /// <summary>
    /// Hesap işlemleri interface
    /// </summary>
    public interface IHesap
    {
        /// <summary>
        /// Yeni hesap açar
        /// </summary>
        /// <param name="hesap">Hesap modeli</param>
        /// <param name="hesapID">Oluşturulan hesap ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapAc(HesapModel hesap, out int hesapID);

        /// <summary>
        /// Hesap bilgilerini günceller
        /// </summary>
        /// <param name="hesap">Hesap modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapGuncelle(HesapModel hesap);

        /// <summary>
        /// Hesabı kapatır
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapKapat(int hesapID, int kullaniciID);

        /// <summary>
        /// Hesabı pasif eder
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapPasifEt(int hesapID, int kullaniciID);

        /// <summary>
        /// Hesabı bloke eder
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="sebep">Bloke sebebi</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapBloke(int hesapID, int kullaniciID, string sebep);

        /// <summary>
        /// Hesap blokesini kaldırır
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapBlokeKaldir(int hesapID, int kullaniciID);

        /// <summary>
        /// Hesap detaylarını getirir
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="hesap">Hesap modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapGetir(int hesapID, out HesapModel hesap);

        /// <summary>
        /// IBAN ile hesap getirir
        /// </summary>
        /// <param name="iban">IBAN</param>
        /// <param name="hesap">Hesap modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapGetirIBAN(string iban, out HesapModel hesap);

        /// <summary>
        /// Hesap numarası ile hesap getirir
        /// </summary>
        /// <param name="hesapNo">Hesap numarası</param>
        /// <param name="hesap">Hesap modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapGetirHesapNo(long hesapNo, out HesapModel hesap);

        /// <summary>
        /// Müşteriye ait hesapları getirir
        /// </summary>
        /// <param name="musteriID">Müşteri ID</param>
        /// <param name="hesaplar">Hesap DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string MusterininHesaplari(int musteriID, out DataTable hesaplar);

        /// <summary>
        /// Hesap bakiyesini günceller
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="tutar">Tutar (pozitif: artış, negatif: azalış)</param>
        /// <returns>Hata mesajı veya null</returns>
        string BakiyeGuncelle(int hesapID, decimal tutar);

        /// <summary>
        /// Bloke bakiye günceller
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="tutar">Tutar</param>
        /// <returns>Hata mesajı veya null</returns>
        string BlokeBakiyeGuncelle(int hesapID, decimal tutar);

        /// <summary>
        /// Hesap ekstresi getirir
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="ekstRe">Ekstre DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapEkstresi(int hesapID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable ekstre);

        /// <summary>
        /// Şubeye göre hesap listesi
        /// </summary>
        /// <param name="subeID">Şube ID</param>
        /// <param name="hesaplar">Hesap DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string SubeyeGoreHesaplar(int subeID, out DataTable hesaplar);

        /// <summary>
        /// Hesap istatistikleri
        /// </summary>
        /// <param name="toplamHesap">Toplam hesap sayısı</param>
        /// <param name="aktifHesap">Aktif hesap sayısı</param>
        /// <param name="toplamBakiye">Toplam bakiye</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapIstatistikleri(out int toplamHesap, out int aktifHesap, out decimal toplamBakiye);
    }
}

