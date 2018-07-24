namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Products()
        {
            CMS_ImagesLink = new HashSet<CMS_ImagesLink>();
            CMS_OrderDetail = new HashSet<CMS_OrderDetail>();
            CMS_ProductDetail = new HashSet<CMS_ProductDetail>();
            CMS_Products1 = new HashSet<CMS_Products>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(100)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(100)]
        public string ParentID { get; set; }
        
        public int TypeCode { get; set; }

        [StringLength(100)]
        public string CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [StringLength(20)]
        public string BarCode { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string PrintOutText { get; set; }

        public bool IsActive { get; set; }

        public string ImageURL { get; set; }


        public double Cost { get; set; }

        public int? Unit { get; set; }

        [Required]
        [StringLength(50)]
        public string Measure { get; set; }

        public decimal? Quantity { get; set; }

        public int Limit { get; set; }

        public double ExtraPrice { get; set; }

        public bool IsAllowedDiscount { get; set; }

        public bool IsCheckedStock { get; set; }

        public bool IsAllowedOpenPrice { get; set; }

        public bool IsPrintedOnCheck { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsAutoAddToOrder { get; set; }

        public bool IsComingSoon { get; set; }

        public bool IsShowInReservation { get; set; }

        public bool IsRecommend { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CMS_Categories CMS_Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_ImagesLink> CMS_ImagesLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_OrderDetail> CMS_OrderDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_ProductDetail> CMS_ProductDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Products> CMS_Products1 { get; set; }

        public virtual CMS_Products CMS_Products2 { get; set; }
    }
}
