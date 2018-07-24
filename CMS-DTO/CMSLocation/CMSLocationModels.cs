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
        public List<CMSDiscountModels> LstDiscount { get; set; }
        public CMSLocationModels()
        {
            LstDiscount = new List<CMSDiscountModels>();
        }
    }
}
