namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_ProductOnStore
    {
        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(100)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public int? Sequence { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public double Cost { get; set; }

        public int? Unit { get; set; }

        [Required]
        [StringLength(50)]
        public string Measure { get; set; }

        public decimal? Quantity { get; set; }

        public int Limit { get; set; }

        public double ExtraPrice { get; set; }

        public bool IsAllowedDiscount { get; set; }

        public bool IsActive { get; set; }

        public bool IsCheckedStock { get; set; }

        public bool IsAllowedOpenPrice { get; set; }

        public bool IsPrintedOnCheck { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsAutoAddToOrder { get; set; }

        public bool IsComingSoon { get; set; }

        public bool IsShowInReservation { get; set; }

        public bool IsRecommend { get; set; }

        public virtual CMS_Products CMS_Products { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }
    }
}
