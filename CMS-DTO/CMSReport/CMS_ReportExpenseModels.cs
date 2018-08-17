using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSReport
{
    public class CMS_ReportExpensetModels
    {
        public double TotalBill  { get; set; }
        public double TotalItem { get; set; }
        public List<CMS_OrderModels> ListOrder { get; set; }
    }
}
