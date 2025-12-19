using MetinBank.Entities;

namespace MetinBank.Modul.Forms
{
    /// <summary>
    /// Oturum bilgilerini tutan statik sınıf
    /// </summary>
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; }
        public static List<UserScreen>? UserScreens { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        /// <summary>
        /// Kullanıcının belirli bir ekrana erişim yetkisi var mı?
        /// </summary>
        public static bool HasScreenPermission(string screenCode)
        {
            if (UserScreens == null)
                return false;

            return UserScreens.Any(s => s.ScreenCode == screenCode && s.CanView);
        }

        /// <summary>
        /// Oturumu sonlandırır
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
            UserScreens = null;
        }
    }
}
