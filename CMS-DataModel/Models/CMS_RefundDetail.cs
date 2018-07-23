namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_RefundDetail
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string RefundID { get; set; }

        [Required]
        [StringLength(100)]
        public string DetailID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CMS_OrderDetail CMS_OrderDetail { get; set; }

        public virtual CMS_Refund CMS_Refund { get; set; }
    }
}
