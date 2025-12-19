using System.Collections.Generic;
using MetinBank.Entities;

namespace MetinBank.Modul.Interface
{
    /// <summary>
    /// Kullanıcı işlemleri için interface
    /// </summary>
    public interface IUserService
    {
        string? Login(string userName, string password, out User? user);
        string? GetUserScreens(int userId, out List<UserScreen>? screens);
        string? CheckScreenPermission(int userId, string screenCode);
    }
}
