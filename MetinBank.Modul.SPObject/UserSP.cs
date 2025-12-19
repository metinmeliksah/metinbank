using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MetinBank.Entities;

namespace MetinBank.Modul.SPObject
{
    /// <summary>
    /// Kullanıcı işlemleri için SP çağrılarını yapan sınıf
    /// </summary>
    public class UserSP : BaseSP
    {
        /// <summary>
        /// Kullanıcı giriş kontrolü - sp_User_Login
        /// </summary>
        public DataTable Login(string userName, string password)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = userName },
                new SqlParameter("@Password", SqlDbType.NVarChar, 100) { Value = password }
            };

            return ExecuteReader("sp_User_Login", parameters);
        }

        /// <summary>
        /// Kullanıcının ekran yetkilerini getirir - sp_User_GetScreens
        /// </summary>
        public DataTable GetUserScreens(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
            };

            return ExecuteReader("sp_User_GetScreens", parameters);
        }

        /// <summary>
        /// Kullanıcının belirli bir ekrana erişim yetkisi var mı - sp_User_CheckScreenPermission
        /// </summary>
        public bool CheckScreenPermission(int userId, string screenCode)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@ScreenCode", SqlDbType.NVarChar, 50) { Value = screenCode }
            };

            var result = ExecuteScalar("sp_User_CheckScreenPermission", parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}
