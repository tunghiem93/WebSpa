namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_ShiftLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_ShiftLog()
        {
            CMS_DrawerLog = new HashSet<CMS_DrawerLog>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(100)]
        public string BusinessDaySession { get; set; }

        public DateTime? StartedOn { get; set; }

        public DateTime? ClosedOn { get; set; }

        [Required]
        [StringLength(100)]
        public string StartedStaff { get; set; }

        [StringLength(100)]
        public string ClosedStaff { get; set; }

        public bool? CurrentState { get; set; }

        public double? ElapsedTime { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CMS_BusinessDayLog CMS_BusinessDayLog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_DrawerLog> CMS_DrawerLog { get; set; }
    }
}
