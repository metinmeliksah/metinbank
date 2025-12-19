using System;
using System.Collections.Generic;
using System.Data;
using MetinBank.Entities;
using MetinBank.Modul.SPObject;

namespace MetinBank.Modul.Business
{
    /// <summary>
    /// Hesap iş kurallarını yöneten sınıf
    /// </summary>
    public class AccountBusiness
    {
        private readonly AccountSP _accountSP;

        public AccountBusiness()
        {
            _accountSP = new AccountSP();
        }

        /// <summary>
        /// Müşteriye ait hesapları getirir
        /// </summary>
        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            List<Account> accounts = new List<Account>();
            DataTable dt = _accountSP.GetAccountsByCustomerId(customerId);

            foreach (DataRow row in dt.Rows)
            {
                accounts.Add(MapAccount(row));
            }

            return accounts;
        }

        /// <summary>
        /// Hesap detayını getirir
        /// </summary>
        public Account? GetAccountById(int accountId)
        {
            DataTable dt = _accountSP.GetAccountById(accountId);
            
            if (dt.Rows.Count > 0)
            {
                return MapAccount(dt.Rows[0]);
            }

            return null;
        }

        /// <summary>
        /// Yeni hesap açar
        /// </summary>
        public string? CreateAccount(Account account)
        {
            // İş kuralları validasyonu
            if (account.CustomerId <= 0)
                return "Müşteri seçilmelidir!";

            if (account.BranchId <= 0)
                return "Şube seçilmelidir!";

            if (string.IsNullOrWhiteSpace(account.AccountType))
                return "Hesap tipi seçilmelidir!";

            if (string.IsNullOrWhiteSpace(account.CurrencyCode))
                return "Para birimi seçilmelidir!";

            try
            {
                int result = _accountSP.CreateAccount(account);
                return result > 0 ? null : "Hesap açılamadı!";
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Hesap bakiyesini getirir
        /// </summary>
        public decimal GetAccountBalance(int accountId)
        {
            return _accountSP.GetAccountBalance(accountId);
        }

        private Account MapAccount(DataRow row)
        {
            return new Account
            {
                AccountId = Convert.ToInt32(row["AccountId"]),
                AccountNumber = row["AccountNumber"].ToString(),
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                CustomerName = row["CustomerName"].ToString(),
                BranchId = Convert.ToInt32(row["BranchId"]),
                BranchName = row["BranchName"].ToString(),
                AccountType = row["AccountType"].ToString(),
                CurrencyCode = row["CurrencyCode"].ToString(),
                Balance = Convert.ToDecimal(row["Balance"]),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : null
            };
        }
    }
}
