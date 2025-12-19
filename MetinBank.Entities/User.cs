using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// Kullanıcı bilgilerini tutan entity sınıfı
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? RoleId { get; set; }
        public int? BranchId { get; set; }
    }
}
