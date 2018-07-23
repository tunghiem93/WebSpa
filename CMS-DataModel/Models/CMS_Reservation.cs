namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Reservation()
        {
            CMS_ReservationDetail = new HashSet<CMS_ReservationDetail>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreID { get; set; }

        [StringLength(100)]
        public string ReservationNo { get; set; }

        [StringLength(100)]
        public string CustomerID { get; set; }

        [StringLength(100)]
        public string TableID { get; set; }

        [StringLength(100)]
        public string OrderID { get; set; }

        public string BookingMethod { get; set; }

        public byte ReservationStatus { get; set; }

        public byte Cover { get; set; }

        public string Remark { get; set; }

        public DateTime Date { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(200)]
        public string ChangedReason { get; set; }

        public virtual CMS_Customer CMS_Customer { get; set; }

        public virtual CMS_Order CMS_Order { get; set; }

        public virtual CMS_Store CMS_Store { get; set; }

        public virtual CMS_Table CMS_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_ReservationDetail> CMS_ReservationDetail { get; set; }
    }
}
