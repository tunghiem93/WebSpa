namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_DrawerLog
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DrawerID { get; set; }

        [Required]
        [StringLength(100)]
        public string ShiftSession { get; set; }

        public bool? CurrentState { get; set; }

        public DateTime? StartedOn { get; set; }

        public DateTime? ClosedOn { get; set; }

        [Required]
        [StringLength(100)]
        public string StartedStaff { get; set; }

        [StringLength(100)]
        public string ClosedStaff { get; set; }

        public double? ActualCash { get; set; }

        public double? StartedCash { get; set; }

        public double? PaidIn { get; set; }

        public double? PaidOut { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CMS_Drawer CMS_Drawer { get; set; }

        public virtual CMS_ShiftLog CMS_ShiftLog { get; set; }
    }
}
