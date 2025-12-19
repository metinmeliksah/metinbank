using System;
using System.Data;
using System.Data.SqlClient;
using MetinBank.Entities;

namespace MetinBank.Modul.SPObject
{
    /// <summary>
    /// Hesap işlemleri için SP çağrılarını yapan sınıf
    /// </summary>
    public class AccountSP : BaseSP
    {
        /// <summary>
        /// Müşteriye ait hesapları getirir - sp_Account_GetByCustomerId
        /// </summary>
        public DataTable GetAccountsByCustomerId(int customerId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId }
            };

            return ExecuteReader("sp_Account_GetByCustomerId", parameters);
        }

        /// <summary>
        /// Hesap detayını getirir - sp_Account_GetById
        /// </summary>
        public DataTable GetAccountById(int accountId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AccountId", SqlDbType.Int) { Value = accountId }
            };

            return ExecuteReader("sp_Account_GetById", parameters);
        }

        /// <summary>
        /// Yeni hesap açar - sp_Account_Create
        /// </summary>
        public int CreateAccount(Account account)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AccountNumber", SqlDbType.NVarChar, 20) { Value = (object?)account.AccountNumber ?? DBNull.Value },
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = account.CustomerId },
                new SqlParameter("@BranchId", SqlDbType.Int) { Value = account.BranchId },
                new SqlParameter("@AccountType", SqlDbType.NVarChar, 50) { Value = (object?)account.AccountType ?? DBNull.Value },
                new SqlParameter("@CurrencyCode", SqlDbType.NVarChar, 3) { Value = (object?)account.CurrencyCode ?? DBNull.Value },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = (object?)account.CreatedBy ?? DBNull.Value }
            };

            return ExecuteNonQuery("sp_Account_Create", parameters);
        }

        /// <summary>
        /// Hesap bakiyesini getirir - sp_Account_GetBalance
        /// </summary>
        public decimal GetAccountBalance(int accountId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AccountId", SqlDbType.Int) { Value = accountId }
            };

            var result = ExecuteScalar("sp_Account_GetBalance", parameters);
            return result != null ? Convert.ToDecimal(result) : 0;
        }
    }
}
