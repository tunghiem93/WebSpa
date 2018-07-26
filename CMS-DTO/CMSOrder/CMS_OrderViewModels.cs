using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSOrder
{
    public class CMS_OrderViewModels
    {
        public List<CMS_ItemModels> ListItem { get; set; }
        public double TotalPrice { get; set; }
        public double SubTotalPrice { get; set; }
    }
}
