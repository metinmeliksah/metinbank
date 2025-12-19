using System;
using System.Collections.Generic;
using MetinBank.Entities;
using MetinBank.Modul.Business;
using MetinBank.Modul.Interface;

namespace MetinBank.Modul.Service
{
    /// <summary>
    /// Şube servisi - Hata yönetimi ve API/Db çağrıları
    /// Service katmanı her zaman string döner (null = başarılı, değilse hata mesajı)
    /// </summary>
    public class BranchService : IBranchService
    {
        private readonly BranchBusiness _branchBusiness;

        public BranchService()
        {
            _branchBusiness = new BranchBusiness();
        }

        /// <summary>
        /// Tüm şubeleri getirir
        /// </summary>
        public string? GetAllBranches(out List<Branch>? branches)
        {
            branches = null;

            try
            {
                branches = _branchBusiness.GetAllBranches();
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Şubeler listelenirken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Şube detayını getirir
        /// </summary>
        public string? GetBranchById(int branchId, out Branch? branch)
        {
            branch = null;

            try
            {
                if (branchId <= 0)
                    return "Geçersiz şube ID!";

                branch = _branchBusiness.GetBranchById(branchId);

                if (branch == null)
                    return "Şube bulunamadı!";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Şube bilgisi alınırken hata: {ex.Message}";
            }
        }
    }
}
