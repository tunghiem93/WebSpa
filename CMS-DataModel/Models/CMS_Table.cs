namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Table()
        {
            CMS_Reservation = new HashSet<CMS_Reservation>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(100)]
        public string ZoneID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Cover { get; set; }

        public byte? ViewMode { get; set; }

        public double? XPoint { get; set; }

        public double? YPoint { get; set; }

        public double? ZPoint { get; set; }

        public bool? IsActive { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsAllowReservation { get; set; }

        public bool IsTemp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Reservation> CMS_Reservation { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }

        public virtual CMS_Zone CMS_Zone { get; set; }
    }
}
