namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_OrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_OrderDetail()
        {
            CMS_OrderDetail1 = new HashSet<CMS_OrderDetail>();
            CMS_RefundDetail = new HashSet<CMS_RefundDetail>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string OrderID { get; set; }

        [StringLength(100)]
        public string ParentID { get; set; }

        [StringLength(100)]
        public string ProductID { get; set; }

        [StringLength(100)]
        public string DiscountID { get; set; }

        public decimal? Quantity { get; set; }

        public double? Price { get; set; }

        public double? Tax { get; set; }

        public double? ServiceCharged { get; set; }

        public int? QueueNumber { get; set; }

        public byte? State { get; set; }

        public double DiscountAmount { get; set; }
        public string EmployeeID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public double DiscountValue { get; set; }

        public byte DiscountType { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }

        public virtual CMS_Discount CMS_Discount { get; set; }

        public virtual CMS_Order CMS_Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_OrderDetail> CMS_OrderDetail1 { get; set; }

        public virtual CMS_OrderDetail CMS_OrderDetail2 { get; set; }

        public virtual CMS_Products CMS_Products { get; set; }

        public virtual CMS_Employee CMS_Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_RefundDetail> CMS_RefundDetail { get; set; }
    }
}
