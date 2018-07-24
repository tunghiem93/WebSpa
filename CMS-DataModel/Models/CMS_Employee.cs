namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Employee()
        {
            CMS_EmployeeWorking = new HashSet<CMS_EmployeeWorking>();
        }

        [StringLength(100)]
        public string ID { get; set; }
        [StringLength(100)]
        public string StoreID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }

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
        public string Phone { get; set; }

        [StringLength(50)]
        public string PinCode { get; set; }

        public bool Gender { get; set; }

        public bool Marital { get; set; }

        public DateTime HiredDate { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        [StringLength(200)]
        public string Country { get; set; }

        public string ImageUrl { get; set; }
        public bool IsSupperAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_EmployeeWorking> CMS_EmployeeWorking { get; set; }
    }
}
