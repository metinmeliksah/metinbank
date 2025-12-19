using System;
using System.Data;
using System.Data.SqlClient;
using MetinBank.Entities;

namespace MetinBank.Modul.SPObject
{
    /// <summary>
    /// Müşteri işlemleri için SP çağrılarını yapan sınıf
    /// </summary>
    public class CustomerSP : BaseSP
    {
        /// <summary>
        /// Tüm müşterileri getirir - sp_Customer_GetAll
        /// </summary>
        public DataTable GetAllCustomers()
        {
            return ExecuteReader("sp_Customer_GetAll", null);
        }

        /// <summary>
        /// Müşteri detayını getirir - sp_Customer_GetById
        /// </summary>
        public DataTable GetCustomerById(int customerId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId }
            };

            return ExecuteReader("sp_Customer_GetById", parameters);
        }

        /// <summary>
        /// Müşteri ekler/günceller - sp_Customer_Save
        /// </summary>
        public int SaveCustomer(Customer customer)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customer.CustomerId },
                new SqlParameter("@IdentityNumber", SqlDbType.NVarChar, 11) { Value = (object?)customer.IdentityNumber ?? DBNull.Value },
                new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = (object?)customer.FirstName ?? DBNull.Value },
                new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = (object?)customer.LastName ?? DBNull.Value },
                new SqlParameter("@BirthDate", SqlDbType.DateTime) { Value = (object?)customer.BirthDate ?? DBNull.Value },
                new SqlParameter("@Gender", SqlDbType.NVarChar, 10) { Value = (object?)customer.Gender ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object?)customer.Email ?? DBNull.Value },
                new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = (object?)customer.Phone ?? DBNull.Value },
                new SqlParameter("@Address", SqlDbType.NVarChar, 500) { Value = (object?)customer.Address ?? DBNull.Value },
                new SqlParameter("@BranchId", SqlDbType.Int) { Value = (object?)customer.BranchId ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = customer.IsActive },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = (object?)customer.CreatedBy ?? DBNull.Value },
                new SqlParameter("@Photo", SqlDbType.VarBinary, -1) { Value = (object?)customer.Photo ?? DBNull.Value },
                new SqlParameter("@Signature", SqlDbType.VarBinary, -1) { Value = (object?)customer.Signature ?? DBNull.Value }
            };

            return ExecuteNonQuery("sp_Customer_Save", parameters);
        }

        /// <summary>
        /// Müşteri siler - sp_Customer_Delete
        /// </summary>
        public int DeleteCustomer(int customerId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId }
            };

            return ExecuteNonQuery("sp_Customer_Delete", parameters);
        }
    }
}
