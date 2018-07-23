namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_TableOnAssigned
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TableID { get; set; }

        [StringLength(100)]
        public string OrderID { get; set; }

        public byte? OrderState { get; set; }

        public byte? TableState { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsCall { get; set; }

        public bool IsGuestCheck { get; set; }

        public DateTime DateCall { get; set; }

        public bool IsCallForBill { get; set; }

        public virtual CMS_Order CMS_Order { get; set; }
    }
}
