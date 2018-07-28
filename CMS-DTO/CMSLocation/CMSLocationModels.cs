using CMS_DTO.CMSContact;
using CMS_DTO.CMSDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSLocation
{
    public class CMSLocationModels
    {
        public CMS_ContactModels ContactDTO { get; set; }
        public List<CMSDiscountModels> LstDiscount { get; set; }
        public CMSLocationModels()
        {
            ContactDTO = new CMS_ContactModels();
            LstDiscount = new List<CMSDiscountModels>();
        }
    }
}
