using System.Data;
using MetinBank.Models;

namespace MetinBank.Interface
{
    /// <summary>
    /// İşlem işlemleri interface
    /// </summary>
    public interface IIslem
    {
        /// <summary>
        /// Para yatırma işlemi
        /// </summary>
        /// <param name="hesapID">Hedef hesap ID</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="aciklama">Açıklama</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="subeID">Şube ID</param>
        /// <param name="islemID">Oluşturulan işlem ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string ParaYatir(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID);

        /// <summary>
        /// Para çekme işlemi
        /// </summary>
        /// <param name="hesapID">Kaynak hesap ID</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="aciklama">Açıklama</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="subeID">Şube ID</param>
        /// <param name="islemID">Oluşturulan işlem ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string ParaCek(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID);

        /// <summary>
        /// Havale işlemi
        /// </summary>
        /// <param name="kaynakHesapID">Kaynak hesap ID</param>
        /// <param name="hedefIBAN">Hedef IBAN</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="aciklama">Açıklama</param>
        /// <param name="aliciAdi">Alıcı adı</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="subeID">Şube ID</param>
        /// <param name="islemID">Oluşturulan işlem ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string Havale(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, int kullaniciID, int subeID, out long islemID);

        /// <summary>
        /// EFT işlemi
        /// </summary>
        /// <param name="kaynakHesapID">Kaynak hesap ID</param>
        /// <param name="hedefIBAN">Hedef IBAN</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="aciklama">Açıklama</param>
        /// <param name="aliciAdi">Alıcı adı</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="subeID">Şube ID</param>
        /// <param name="islemID">Oluşturulan işlem ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string EFT(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, int kullaniciID, int subeID, out long islemID);

        /// <summary>
        /// Virman işlemi (aynı müşterinin hesapları arası)
        /// </summary>
        /// <param name="kaynakHesapID">Kaynak hesap ID</param>
        /// <param name="hedefHesapID">Hedef hesap ID</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="aciklama">Açıklama</param>
        /// <param name="kullaniciID">İşlemi yapan kullanıcı ID</param>
        /// <param name="subeID">Şube ID</param>
        /// <param name="islemID">Oluşturulan işlem ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string Virman(int kaynakHesapID, int hedefHesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID);

        /// <summary>
        /// İşlem detaylarını getirir
        /// </summary>
        /// <param name="islemID">İşlem ID</param>
        /// <param name="islem">İşlem modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemGetir(long islemID, out IslemModel islem);

        /// <summary>
        /// İşlem referans numarası ile işlem getirir
        /// </summary>
        /// <param name="referansNo">Referans numarası</param>
        /// <param name="islem">İşlem modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemGetirReferans(string referansNo, out IslemModel islem);

        /// <summary>
        /// Hesaba göre işlem listesi
        /// </summary>
        /// <param name="hesapID">Hesap ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="islemler">İşlem DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapIslemleri(int hesapID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable islemler);

        /// <summary>
        /// Şubeye göre işlem listesi
        /// </summary>
        /// <param name="subeID">Şube ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="islemler">İşlem DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string SubeIslemleri(int subeID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable islemler);

        /// <summary>
        /// Günlük işlem özeti
        /// </summary>
        /// <param name="subeID">Şube ID (0: tüm şubeler)</param>
        /// <param name="tarih">Tarih</param>
        /// <param name="ozet">Özet DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string GunlukIslemOzeti(int subeID, System.DateTime tarih, out DataTable ozet);

        /// <summary>
        /// İşlem onayı bekleyen listesi
        /// </summary>
        /// <param name="rolID">Rol ID</param>
        /// <param name="islemler">İşlem DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string OnayBekleyenIslemler(int rolID, out DataTable islemler);
    }
}

