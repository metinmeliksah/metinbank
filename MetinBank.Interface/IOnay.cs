using System.Data;
using MetinBank.Models;

namespace MetinBank.Interface
{
    /// <summary>
    /// Onay işlemleri interface
    /// </summary>
    public interface IOnay
    {
        /// <summary>
        /// Onay talebi oluşturur
        /// </summary>
        /// <param name="islemID">İşlem ID</param>
        /// <param name="islemTipi">İşlem tipi</param>
        /// <param name="talepEdenID">Talep eden kullanıcı ID</param>
        /// <param name="beklenenRol">Beklenen onaylayan rol</param>
        /// <param name="onayLogID">Oluşturulan onay log ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string OnayTalebiOlustur(long islemID, string islemTipi, int talepEdenID, string beklenenRol, out int onayLogID);

        /// <summary>
        /// İşlemi onaylar
        /// </summary>
        /// <param name="onayLogID">Onay log ID</param>
        /// <param name="onaylayanID">Onaylayan kullanıcı ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemOnayla(int onayLogID, int onaylayanID);

        /// <summary>
        /// İşlemi reddeder
        /// </summary>
        /// <param name="onayLogID">Onay log ID</param>
        /// <param name="onaylayanID">Onaylayan kullanıcı ID</param>
        /// <param name="redNedeni">Red nedeni</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemReddet(int onayLogID, int onaylayanID, string redNedeni);

        /// <summary>
        /// Onay bekleyen işlemler
        /// </summary>
        /// <param name="rolAdi">Rol adı</param>
        /// <param name="onaylar">Onay DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string OnayBekleyenler(string rolAdi, out DataTable onaylar);

        /// <summary>
        /// Kullanıcıya göre onay bekleyen işlemler
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="onaylar">Onay DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string KullaniciyaGoreOnayBekleyenler(int kullaniciID, out DataTable onaylar);

        /// <summary>
        /// Onay geçmişi
        /// </summary>
        /// <param name="islemID">İşlem ID</param>
        /// <param name="gecmis">Geçmiş DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string OnayGecmisi(long islemID, out DataTable gecmis);

        /// <summary>
        /// Onaylanan işlemler
        /// </summary>
        /// <param name="onaylayanID">Onaylayan kullanıcı ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="onaylar">Onay DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string OnaylananIslemler(int onaylayanID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable onaylar);

        /// <summary>
        /// Reddedilen işlemler
        /// </summary>
        /// <param name="onaylayanID">Onaylayan kullanıcı ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="onaylar">Onay DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string RededilenIslemler(int onaylayanID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable onaylar);

        /// <summary>
        /// Onay gerekiyor mu kontrolü
        /// </summary>
        /// <param name="islemTipi">İşlem tipi</param>
        /// <param name="tutar">Tutar</param>
        /// <param name="gerekliRol">Gerekli rol</param>
        /// <returns>Onay gerekiyorsa true</returns>
        bool OnayGerekiyorMu(string islemTipi, decimal tutar, out string gerekliRol);
    }
}

