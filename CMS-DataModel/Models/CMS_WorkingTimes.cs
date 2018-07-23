namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_WorkingTimes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_WorkingTimes()
        {
            CMS_EmployeeWorking = new HashSet<CMS_EmployeeWorking>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(100)]
        public string Store_ID { get; set; }

        public byte DayOfWeek { get; set; }

        public bool IsActive { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_EmployeeWorking> CMS_EmployeeWorking { get; set; }
    }
}
