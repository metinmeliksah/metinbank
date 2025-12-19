using System;
using System.Collections.Generic;
using System.Data;
using MetinBank.Entities;
using MetinBank.Modul.SPObject;

namespace MetinBank.Modul.Business
{
    /// <summary>
    /// Şube iş kurallarını yöneten sınıf
    /// </summary>
    public class BranchBusiness
    {
        private readonly BranchSP _branchSP;

        public BranchBusiness()
        {
            _branchSP = new BranchSP();
        }

        /// <summary>
        /// Tüm şubeleri getirir
        /// </summary>
        public List<Branch> GetAllBranches()
        {
            List<Branch> branches = new List<Branch>();
            DataTable dt = _branchSP.GetAllBranches();

            foreach (DataRow row in dt.Rows)
            {
                branches.Add(new Branch
                {
                    BranchId = Convert.ToInt32(row["BranchId"]),
                    BranchCode = row["BranchCode"].ToString(),
                    BranchName = row["BranchName"].ToString(),
                    City = row["City"].ToString(),
                    District = row["District"].ToString(),
                    Address = row["Address"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Email = row["Email"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return branches;
        }

        /// <summary>
        /// Şube detayını getirir
        /// </summary>
        public Branch? GetBranchById(int branchId)
        {
            DataTable dt = _branchSP.GetBranchById(branchId);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Branch
                {
                    BranchId = Convert.ToInt32(row["BranchId"]),
                    BranchCode = row["BranchCode"].ToString(),
                    BranchName = row["BranchName"].ToString(),
                    City = row["City"].ToString(),
                    District = row["District"].ToString(),
                    Address = row["Address"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Email = row["Email"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                };
            }

            return null;
        }
    }
}
