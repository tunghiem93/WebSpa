namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_Companies
    {
        [StringLength(100)]
        public string Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Phone { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string LinkBlog { get; set; }

        [StringLength(250)]
        public string LinkFB { get; set; }

        [StringLength(250)]
        public string LinkTwiter { get; set; }

        [StringLength(250)]
        public string LinkInstagram { get; set; }

        [StringLength(60)]
        public string ImageURL { get; set; }

        [StringLength(250)]
        public string BusinessHour { get; set; }

        public bool IsActive { get; set; }

        [StringLength(60)]
        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [StringLength(60)]
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
