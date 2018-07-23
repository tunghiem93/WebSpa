namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_ReservationDetail
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ReservationID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductID { get; set; }

        [StringLength(250)]
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public byte ProductType { get; set; }

        public double Price { get; set; }

        public virtual CMS_Reservation CMS_Reservation { get; set; }
    }
}
