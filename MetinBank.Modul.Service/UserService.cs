using System;
using System.Collections.Generic;
using MetinBank.Entities;
using MetinBank.Modul.Business;
using MetinBank.Modul.Interface;

namespace MetinBank.Modul.Service
{
    /// <summary>
    /// Kullanıcı servisi - Hata yönetimi ve API/Db çağrıları
    /// Service katmanı her zaman string döner (null = başarılı, değilse hata mesajı)
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserBusiness _userBusiness;

        public UserService()
        {
            _userBusiness = new UserBusiness();
        }

        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        /// <returns>Başarılı ise null, hata varsa hata mesajı</returns>
        public string? Login(string userName, string password, out User? user)
        {
            user = null;

            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return "Kullanıcı adı boş olamaz!";

                if (string.IsNullOrWhiteSpace(password))
                    return "Şifre boş olamaz!";

                user = _userBusiness.Login(userName, password);

                if (user == null)
                    return "Kullanıcı adı veya şifre hatalı!";

                if (!user.IsActive)
                    return "Kullanıcı hesabı aktif değil!";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Giriş hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kullanıcının ekran yetkilerini getirir
        /// </summary>
        public string? GetUserScreens(int userId, out List<UserScreen>? screens)
        {
            screens = null;

            try
            {
                screens = _userBusiness.GetUserScreens(userId);
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Ekran yetkileri alınırken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Kullanıcının ekran erişim yetkisini kontrol eder
        /// </summary>
        public string? CheckScreenPermission(int userId, string screenCode)
        {
            try
            {
                bool hasPermission = _userBusiness.CheckScreenPermission(userId, screenCode);
                
                if (!hasPermission)
                    return "Bu ekrana erişim yetkiniz yok!";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Yetki kontrolü hatası: {ex.Message}";
            }
        }
    }
}
