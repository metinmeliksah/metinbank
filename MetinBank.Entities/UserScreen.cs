using System;

namespace MetinBank.Entities
{
    /// <summary>
    /// Kullanıcı ekran yetkilerini tutan entity sınıfı
    /// </summary>
    public class UserScreen
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ScreenCode { get; set; }
        public string? ScreenName { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
