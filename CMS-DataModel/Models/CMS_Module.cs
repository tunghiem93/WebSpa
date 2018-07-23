namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMS_Module()
        {
            CMS_ModuleOnStore = new HashSet<CMS_ModuleOnStore>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ParentID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public int Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMS_ModuleOnStore> CMS_ModuleOnStore { get; set; }
    }
}
