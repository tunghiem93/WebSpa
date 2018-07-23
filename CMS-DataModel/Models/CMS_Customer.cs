namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Customer()
        {
            CMS_Reservation = new HashSet<CMS_Reservation>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(20)]
        public string IC { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool? Gender { get; set; }

        public bool? Marital { get; set; }

        public DateTime? JoinedDate { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(200)]
        public string HomeStreet { get; set; }

        [StringLength(200)]
        public string HomeCity { get; set; }

        [StringLength(50)]
        public string HomeZipCode { get; set; }

        [StringLength(100)]
        public string HomeCountry { get; set; }

        [StringLength(200)]
        public string OfficeStreet { get; set; }

        [StringLength(200)]
        public string OfficeCity { get; set; }

        [StringLength(50)]
        public string OfficeZipCode { get; set; }

        [StringLength(100)]
        public string OfficeCountry { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Anniversary { get; set; }

        public DateTime ValidTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Reservation> CMS_Reservation { get; set; }
    }
}
