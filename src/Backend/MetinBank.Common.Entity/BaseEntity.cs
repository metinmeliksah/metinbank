/*
 * MetinBank - Base Entity Sınıfı
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Tüm entity'lerin türeyeceği base sınıf
 * Standartlara uygun property isimlendirmeleri
 */

using System;

namespace MetinBank.Common.Entity
{
    /// <summary>
    /// Tüm entity'lerin türeyeceği base sınıf
    /// </summary>
    public abstract class BaseEntity
    {
        private Guid _id;
        private DateTime _createdAt;
        private DateTime? _updatedAt;
        private string? _createdBy;
        private string? _updatedBy;
        private bool _isActive;

        /// <summary>
        /// Primary Key - GUID
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        /// <summary>
        /// Güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }

        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public string? CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        /// Güncelleyen kullanıcı
        /// </summary>
        public string? UpdatedBy
        {
            get { return _updatedBy; }
            set { _updatedBy = value; }
        }

        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseEntity()
        {
            _id = Guid.NewGuid();
            _createdAt = DateTime.UtcNow;
            _isActive = true;
        }
    }
}


