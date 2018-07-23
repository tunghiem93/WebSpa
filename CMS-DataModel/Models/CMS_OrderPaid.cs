namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_OrderPaid
    {
        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(100)]
        public string OrderID { get; set; }

        [StringLength(100)]
        public string PaymentMethodID { get; set; }

        public double? Amount { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public string Description { get; set; }

        public virtual CMS_Order CMS_Order { get; set; }
    }
}
