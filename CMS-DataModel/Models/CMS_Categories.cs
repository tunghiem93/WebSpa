namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Categories()
        {
            CMS_Categories1 = new HashSet<CMS_Categories>();
            CMS_Products = new HashSet<CMS_Products>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? TotalProducts { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string ImageURL { get; set; }

        [StringLength(100)]
        public string ParentID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsShowInReservation { get; set; }

        public int Sequence { get; set; }

        public int ProductTypeCode { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Categories> CMS_Categories1 { get; set; }

        public virtual CMS_Categories CMS_Categories2 { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Products> CMS_Products { get; set; }
    }
}
