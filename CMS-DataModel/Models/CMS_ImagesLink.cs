namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_ImagesLink
    {
        [StringLength(100)]
        public string Id { get; set; }

        [StringLength(100)]
        public string ImageURL { get; set; }

        [StringLength(100)]
        public string ProductId { get; set; }

        public byte Status { get; set; }
        public int Offset { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public virtual CMS_Products CMS_Products { get; set; }
    }
}
