using System.Collections.Generic;
using MetinBank.Entities;

namespace MetinBank.Modul.Interface
{
    /// <summary>
    /// Şube işlemleri için interface
    /// </summary>
    public interface IBranchService
    {
        string? GetAllBranches(out List<Branch>? branches);
        string? GetBranchById(int branchId, out Branch? branch);
    }
}
