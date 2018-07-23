namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Store()
        {
            CMS_BussinessHour = new HashSet<CMS_BussinessHour>();
            CMS_Categories = new HashSet<CMS_Categories>();
            CMS_Discount = new HashSet<CMS_Discount>();
            CMS_EmployeeWorking = new HashSet<CMS_EmployeeWorking>();
            CMS_ModulePermission = new HashSet<CMS_ModulePermission>();
            CMS_Order = new HashSet<CMS_Order>();
            CMS_Price = new HashSet<CMS_Price>();
            CMS_Reservation = new HashSet<CMS_Reservation>();
            CMS_Role = new HashSet<CMS_Role>();
            CMS_Season = new HashSet<CMS_Season>();
            CMS_StoreSetting = new HashSet<CMS_StoreSetting>();
            CMS_Table = new HashSet<CMS_Table>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [StringLength(100)]
        public string CompanyID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(100)]
        public string IndustryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        [StringLength(256)]
        public string Street { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Zipcode { get; set; }

        [StringLength(50)]
        public string GSTRegNo { get; set; }

        [StringLength(50)]
        public string TimeZone { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string StoreCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_BussinessHour> CMS_BussinessHour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Categories> CMS_Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Discount> CMS_Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_EmployeeWorking> CMS_EmployeeWorking { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_ModulePermission> CMS_ModulePermission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Order> CMS_Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Price> CMS_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Reservation> CMS_Reservation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Role> CMS_Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Season> CMS_Season { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_StoreSetting> CMS_StoreSetting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_Table> CMS_Table { get; set; }
    }
}
