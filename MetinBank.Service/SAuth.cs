using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using MetinBank.Business;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Service
{
    /// <summary>
    /// Authentication servis sınıfı
    /// </summary>
    public class SAuth
    {
        private readonly BAuth _bAuth;
        private readonly BLog _bLog;
        private const string JWT_SECRET_KEY = "MetinBank2024SecretKeyForJWTTokenGeneration!@#$%";

        public SAuth()
        {
            _bAuth = new BAuth();
            _bLog = new BLog();
        }

        /// <summary>
        /// Kullanıcı girişi
        /// </summary>
        /// <param name="kullaniciAdi">Kullanıcı adı</param>
        /// <param name="sifre">Şifre</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <param name="macAdresi">MAC adresi</param>
        /// <param name="kullanici">Kullanıcı modeli</param>
        /// <returns>Hata mesajı veya null</returns>
        public string Login(string kullaniciAdi, string sifre, string ipAdresi, string macAdresi, out KullaniciModel kullanici)
        {
            kullanici = null;

            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(kullaniciAdi))
                    return "Kullanıcı adı boş olamaz.";

                if (string.IsNullOrWhiteSpace(sifre))
                    return "Şifre boş olamaz.";

                // Login işlemi
                string hata = _bAuth.Login(kullaniciAdi, sifre, ipAdresi, macAdresi, out kullanici);
                
                // Login logu kaydet
                _bLog.LoginLoguKaydet(
                    kullanici?.KullaniciID,
                    kullaniciAdi,
                    "Login",
                    hata == null,
                    ipAdresi,
                    macAdresi,
                    "Windows Application",
                    Environment.MachineName,
                    Environment.OSVersion.ToString(),
                    hata
                );

                if (hata != null)
                {
                    // Güvenlik logu (başarısız giriş)
                    _bLog.GuvenlikLoguKaydet(
                        "BasarisizGiris",
                        null,
                        ipAdresi,
                        $"Başarısız giriş denemesi: {kullaniciAdi}",
                        "Dusuk"
                    );
                    
                    return hata;
                }

                // Başarılı giriş logu
                _bLog.IslemLoguKaydet(
                    kullanici.KullaniciID,
                    "Login",
                    "Kullanici",
                    kullanici.KullaniciID,
                    null,
                    null,
                    $"Kullanıcı giriş yaptı: {kullanici.TamAd}",
                    ipAdresi,
                    true,
                    null
                );

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Login hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kullanıcı çıkışı
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="ipAdresi">IP adresi</param>
        /// <returns>Hata mesajı veya null</returns>
        public string Logout(int kullaniciID, string ipAdresi)
        {
            try
            {
                string hata = _bAuth.Logout(kullaniciID, ipAdresi);

                // Logout logu kaydet
                _bLog.LoginLoguKaydet(
                    kullaniciID,
                    null,
                    "Logout",
                    hata == null,
                    ipAdresi,
                    null,
                    "Windows Application",
                    Environment.MachineName,
                    Environment.OSVersion.ToString(),
                    hata
                );

                return hata;
            }
            catch (Exception ex)
            {
                return $"Logout hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Şifre değiştirme
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="eskiSifre">Eski şifre</param>
        /// <param name="yeniSifre">Yeni şifre</param>
        /// <returns>Hata mesajı veya null</returns>
        public string SifreDegistir(int kullaniciID, string eskiSifre, string yeniSifre)
        {
            try
            {
                // Şifre güvenlik kontrolü
                string hata = SecurityHelper.ValidatePasswordStrength(yeniSifre);
                if (hata != null) return hata;

                // Şifre değiştirme
                hata = _bAuth.SifreDegistir(kullaniciID, eskiSifre, yeniSifre);

                // Log kaydet
                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "SifreDegistir",
                    "Kullanici",
                    kullaniciID,
                    null,
                    null,
                    "Kullanıcı şifresini değiştirdi",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                if (hata == null)
                {
                    // Güvenlik logu
                    _bLog.GuvenlikLoguKaydet(
                        "SifreDegistirme",
                        kullaniciID,
                        CommonFunctions.GetLocalIPAddress(),
                        "Kullanıcı şifresini başarıyla değiştirdi",
                        "Dusuk"
                    );
                }

                return hata;
            }
            catch (Exception ex)
            {
                return $"Şifre değiştirme hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// JWT Token oluşturur (Web API için)
        /// </summary>
        /// <param name="kullanici">Kullanıcı modeli</param>
        /// <returns>JWT Token</returns>
        public string GenerateJwtToken(KullaniciModel kullanici)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JWT_SECRET_KEY);
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciID.ToString()),
                        new Claim(ClaimTypes.Name, kullanici.KullaniciAdi),
                        new Claim(ClaimTypes.GivenName, kullanici.TamAd),
                        new Claim(ClaimTypes.Role, kullanici.RolAdi),
                        new Claim("RolID", kullanici.RolID.ToString()),
                        new Claim("SubeID", kullanici.SubeID?.ToString() ?? "0")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "MetinBankAPI",
                    Audience = "MetinBankClients",
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"JWT Token oluşturma hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// JWT Token doğrulama
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <returns>Token geçerli ise true</returns>
        public bool ValidateJwtToken(string token, out int kullaniciID)
        {
            kullaniciID = 0;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JWT_SECRET_KEY);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "MetinBankAPI",
                    ValidateAudience = true,
                    ValidAudience = "MetinBankClients",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                kullaniciID = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Yetki kontrolü
        /// </summary>
        /// <param name="kullaniciID">Kullanıcı ID</param>
        /// <param name="gerekliYetkiSeviyesi">Gerekli yetki seviyesi</param>
        /// <returns>Hata mesajı veya null</returns>
        public string YetkiKontrol(int kullaniciID, int gerekliYetkiSeviyesi)
        {
            try
            {
                return _bAuth.YetkiKontrol(kullaniciID, gerekliYetkiSeviyesi);
            }
            catch (Exception ex)
            {
                return $"Yetki kontrolü hatası: {ex.Message}";
            }
        }
    }

}

