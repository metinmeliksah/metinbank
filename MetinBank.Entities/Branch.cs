using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// Şube bilgilerini tutan entity sınıfı
    /// </summary>
    public class Branch
    {
        public int BranchId { get; set; }
        public string? BranchCode { get; set; }
        public string? BranchName { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
