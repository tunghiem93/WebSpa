using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSDiscount
{
    public class CMS_DiscountViewModels
    {
        public List<CMSDiscountModels> LstChildren { get; set; }
        public CMS_DiscountViewModels()
        {
            LstChildren = new List<CMSDiscountModels>();
        }
    }
}
