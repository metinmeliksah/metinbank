using System;
using System.Collections.Generic;
using System.Data;
using MetinBank.Entities;
using MetinBank.Modul.SPObject;

namespace MetinBank.Modul.Business
{
    /// <summary>
    /// Kullanıcı iş kurallarını yöneten sınıf
    /// </summary>
    public class UserBusiness
    {
        private readonly UserSP _userSP;

        public UserBusiness()
        {
            _userSP = new UserSP();
        }

        /// <summary>
        /// Kullanıcı giriş kontrolü yapar
        /// </summary>
        public User? Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            DataTable dt = _userSP.Login(userName, password);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new User
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    UserName = row["UserName"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Email = row["Email"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    RoleId = row["RoleId"] != DBNull.Value ? Convert.ToInt32(row["RoleId"]) : null,
                    BranchId = row["BranchId"] != DBNull.Value ? Convert.ToInt32(row["BranchId"]) : null
                };
            }

            return null;
        }

        /// <summary>
        /// Kullanıcının ekran yetkilerini getirir
        /// </summary>
        public List<UserScreen> GetUserScreens(int userId)
        {
            List<UserScreen> screens = new List<UserScreen>();
            DataTable dt = _userSP.GetUserScreens(userId);

            foreach (DataRow row in dt.Rows)
            {
                screens.Add(new UserScreen
                {
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    ScreenCode = row["ScreenCode"].ToString(),
                    ScreenName = row["ScreenName"].ToString(),
                    CanView = Convert.ToBoolean(row["CanView"]),
                    CanAdd = Convert.ToBoolean(row["CanAdd"]),
                    CanEdit = Convert.ToBoolean(row["CanEdit"]),
                    CanDelete = Convert.ToBoolean(row["CanDelete"])
                });
            }

            return screens;
        }

        /// <summary>
        /// Kullanıcının ekran erişim yetkisini kontrol eder
        /// </summary>
        public bool CheckScreenPermission(int userId, string screenCode)
        {
            return _userSP.CheckScreenPermission(userId, screenCode);
        }
    }
}
