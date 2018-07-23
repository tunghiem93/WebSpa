namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Order()
        {
            CMS_Order1 = new HashSet<CMS_Order>();
            CMS_OrderDetail = new HashSet<CMS_OrderDetail>();
            CMS_OrderPaid = new HashSet<CMS_OrderPaid>();
            CMS_Refund = new HashSet<CMS_Refund>();
            CMS_Reservation = new HashSet<CMS_Reservation>();
            CMS_TableOnAssigned = new HashSet<CMS_TableOnAssigned>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreID { get; set; }

        [StringLength(100)]
        public string ParentID { get; set; }

        [StringLength(100)]
        public string ReceiptNo { get; set; }

        [StringLength(100)]
        public string OrderNo { get; set; }

        [StringLength(50)]
        public string TagNumber { get; set; }

        public byte SplitNo { get; set; }

        public byte? Cover { get; set; }

        public double? TotalBill { get; set; }

        public double? TotalDiscount { get; set; }

        public double? SubTotal { get; set; }

        public double? Tip { get; set; }

        public double? Tax { get; set; }

        public double? Remaining { get; set; }

        public double? ServiceCharged { get; set; }

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
        public string CustomerID { get; set; }

        [StringLength(100)]
        public string DrawerID { get; set; }

        public double? RoundingAmount { get; set; }

        [StringLength(100)]
        public string Cashier { get; set; }

        public string Remark { get; set; }

        public double? TotalPromotion { get; set; }

        public DateTime? ReceiptCreatedDate { get; set; }

        public bool IsTemp { get; set; }

        [StringLength(100)]
        public string DrawerCreated { get; set; }

        [StringLength(100)]
        public string DeliveryOrderID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Order> CMS_Order1 { get; set; }

        public virtual CMS_Order CMS_Order2 { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_OrderDetail> CMS_OrderDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_OrderPaid> CMS_OrderPaid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Refund> CMS_Refund { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Reservation> CMS_Reservation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_TableOnAssigned> CMS_TableOnAssigned { get; set; }
    }
}
