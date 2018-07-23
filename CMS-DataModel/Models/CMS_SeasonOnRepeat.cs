namespace CMS_DataModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMS_SeasonOnRepeat
    {
        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SeasonID { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string ModifiedUser { get; set; }

        public DateTime LastModified { get; set; }

        public byte Day { get; set; }

        public virtual CMS_Season CMS_Season { get; set; }
    }
}
