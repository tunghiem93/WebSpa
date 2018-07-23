using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSGallery
{
    public class CMS_GalleryViewModels
    {
        public List<CMS_GalleryModels> Gallerys { get; set; }
        public CMS_GalleryViewModels()
        {
            Gallerys = new List<CMS_GalleryModels>();
        }
    }
}
