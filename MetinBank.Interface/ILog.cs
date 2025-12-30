using System.Data;

namespace MetinBank.Interface
{
    /// <summary>
    /// Log işlemleri interface
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// İşlem logu kaydeder
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="islemTipi">İşlem tipi</param>
        /// <param name="tabloAdi">Tablo adı</param>
        /// <param name="kayitID">Kayıt ID</param>
        /// <param name="oncekiDeger">Önceki değer (JSON)</param>
        /// <param name="yeniDeger">Yeni değer (JSON)</param>
        /// <param name="islemDetay">İşlem detayı</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="basariliMi">Başarılı mı</param>
        /// <param name="hataMesaji">Hata mesajı</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemLoguKaydet(int? kullaniciID, string islemTipi, string tabloAdi, long? kayitID, string oncekiDeger, string yeniDeger, string islemDetay, string ipAdresi, bool basariliMi, string hataMesaji);

        /// <summary>
        /// Görüntüleme logu kaydeder
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="goruntulenentablo">Görüntülenen tablo</param>
        /// <param name="goruntulenenKayitID">Görüntülenen kayıt ID</param>
        /// <param name="sorguParametreleri">Sorgu parametreleri (JSON)</param>
        /// <param name="kayitSayisi">Kayıt sayısı</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="islemSuresi">İşlem süresi (ms)</param>
        /// <returns>Hata mesajı veya null</returns>
        string GoruntulemeLoguKaydet(int kullaniciID, string goruntulenenTablo, long? goruntulenenKayitID, string sorguParametreleri, int kayitSayisi, string ipAdresi, int islemSuresi);

        /// <summary>
        /// Login logu kaydeder
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="kullaniciAdi">Kullanıcı adı</param>
        /// <param name="islemTipi">İşlem tipi (Login/Logout/FailedLogin)</param>
        /// <param name="basariliMi">Başarılı mı</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="macAdresi">MAC adresi</param>
        /// <param name="tarayici">Tarayıcı</param>
        /// <param name="cihaz">Cihaz</param>
        /// <param name="isletimSistemi">İşletim sistemi</param>
        /// <param name="hataMesaji">Hata mesajı</param>
        /// <returns>Hata mesajı veya null</returns>
        string LoginLoguKaydet(int? kullaniciID, string kullaniciAdi, string islemTipi, bool basariliMi, string ipAdresi, string macAdresi, string tarayici, string cihaz, string isletimSistemi, string hataMesaji);

        /// <summary>
        /// Güvenlik logu kaydeder
        /// </summary>
        /// <param name="olayTipi">Olay tipi</param>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="olayDetay">Olay detayı</param>
        /// <param name="riskSeviyesi">Risk seviyesi</param>
        /// <returns>Hata mesajı veya null</returns>
        string GuvenlikLoguKaydet(string olayTipi, int? kullaniciID, string ipAdresi, string olayDetay, string riskSeviyesi);

        /// <summary>
        /// İşlem loglarını getirir
        /// </summary>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="logTipi">Log tipi</param>
        /// <param name="loglar">Log DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string IslemLoglariniGetir(System.DateTime baslangicTarihi, System.DateTime bitisTarihi, string logTipi, out DataTable loglar);

        /// <summary>
        /// Kullanıcıya göre loglar
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="baslangicTarihi">Başlangıç tarihi</param>
        /// <param name="bitisTarihi">Bitiş tarihi</param>
        /// <param name="loglar">Log DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string KullaniciyaGoreLoglar(int kullaniciID, System.DateTime baslangicTarihi, System.DateTime bitisTarihi, out DataTable loglar);

        /// <summary>
        /// Güvenlik loglarını getirir
        /// </summary>
        /// <param name="riskSeviyesi">Risk seviyesi (null: tümü)</param>
        /// <param name="islemeAlindiMi">İşleme alındı mı (null: tümü)</param>
        /// <param name="loglar">Log DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        string GuvenlikLoglariniGetir(string riskSeviyesi, bool? islemeAlindiMi, out DataTable loglar);
    }
}

