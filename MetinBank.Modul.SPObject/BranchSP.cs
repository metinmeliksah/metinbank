using System;
using System.Data;
using System.Data.SqlClient;

namespace MetinBank.Modul.SPObject
{
    /// <summary>
    /// Şube işlemleri için SP çağrılarını yapan sınıf
    /// </summary>
    public class BranchSP : BaseSP
    {
        /// <summary>
        /// Tüm şubeleri getirir - sp_Branch_GetAll
        /// </summary>
        public DataTable GetAllBranches()
        {
            return ExecuteReader("sp_Branch_GetAll", null);
        }

        /// <summary>
        /// Şube detayını getirir - sp_Branch_GetById
        /// </summary>
        public DataTable GetBranchById(int branchId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BranchId", SqlDbType.Int) { Value = branchId }
            };

            return ExecuteReader("sp_Branch_GetById", parameters);
        }
    }
}
