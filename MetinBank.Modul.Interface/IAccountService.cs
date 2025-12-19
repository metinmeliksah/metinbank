using System.Collections.Generic;
using MetinBank.Entities;

namespace MetinBank.Modul.Interface
{
    /// <summary>
    /// Hesap işlemleri için interface
    /// </summary>
    public interface IAccountService
    {
        string? GetAccountsByCustomerId(int customerId, out List<Account>? accounts);
        string? GetAccountById(int accountId, out Account? account);
        string? CreateAccount(Account account);
        string? GetAccountBalance(int accountId, out decimal balance);
    }
}
