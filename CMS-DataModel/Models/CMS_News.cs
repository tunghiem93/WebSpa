namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_News
    {
        [StringLength(100)]
        public string Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Short_Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [StringLength(60)]
        public string ImageURL { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(250)]
        public string Source { get; set; }

        [StringLength(250)]
        public string Category { get; set; }

        public bool IsActive { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }
    }
}
