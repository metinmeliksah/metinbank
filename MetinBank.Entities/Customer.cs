using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// Müşteri bilgilerini tutan entity sınıfı
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public byte[]? Photo { get; set; }
        public byte[]? Signature { get; set; }
    }
}
