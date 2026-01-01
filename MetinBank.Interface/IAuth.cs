using MetinBank.Models;

namespace MetinBank.Interface
{
    /// <summary>
    /// Kimlik doğrulama interface
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Kullanıcı girişi
        /// </summary>
        /// <param name="kullaniciAdi">Kullanıcı adı</param>
        /// <param name="sifre">Şifre</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="macAdresi">MAC adresi</param>
        /// <param name="kullanici">Kullanıcı modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        string Login(string kullaniciAdi, string sifre, string ipAdresi, string macAdresi, out KullaniciModel kullanici);

        /// <summary>
        /// Kullanıcı çıkışı
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <returns>Hata mesajı veya null</returns>
        string Logout(int kullaniciID, string ipAdresi);

        /// <summary>
        /// Şifre değiştirme
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="eskiSifre">Eski şifre</param>
        /// <param name="yeniSifre">Yeni şifre</param>
        /// <returns>Hata mesajı veya null</returns>
        string SifreDegistir(int kullaniciID, string eskiSifre, string yeniSifre);

        /// <summary>
        /// Şifre sıfırlama (admin)
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="adminID">Admin ID</param>
        /// <param name="yeniSifre">Yeni şifre</param>
        /// <returns>Hata mesajı veya null</returns>
        string SifreSifirla(int kullaniciID, int adminID, out string yeniSifre);

        /// <summary>
        /// Hesap kilidini aç
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="adminID">Admin ID</param>
        /// <returns>Hata mesajı veya null</returns>
        string HesapKilidiAc(int kullaniciID, int adminID);

        /// <summary>
        /// JWT Token oluşturur
        /// </summary>
        /// <param name="kullanici">Kullanıcı modeli</param>
        /// <returns>JWT Token</returns>
        string GenerateJwtToken(KullaniciModel kullanici);

        /// <summary>
        /// JWT Token doğrulama
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <returns>Token geçerli ise true</returns>
        bool ValidateJwtToken(string token, out int kullaniciID);

        /// <summary>
        /// Yetki kontrolü
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="gerekliYetkiSeviyesi">Gerekli yetki seviyesi</param>
        /// <returns>Hata mesajı veya null</returns>
        string YetkiKontrol(int kullaniciID, int gerekliYetkiSeviyesi);
    }
}

