namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_EmployeeWorking
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeID { get; set; }

        [Required]
        [StringLength(100)]
        public string WorkingTimeID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        [StringLength(100)]
        public string StoreID { get; set; }

        public virtual CMS_Employee CMS_Employee { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }

        public virtual CMS_WorkingTimes CMS_WorkingTimes { get; set; }
    }
}
