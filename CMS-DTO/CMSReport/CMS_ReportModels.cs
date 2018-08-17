using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSReport
{
    public class CMS_ReportModels
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public CMS_ReportReceiptModels ReportReceipt { get; set; }
        public CMS_ReportExpensetModels ReportExpense { get; set; }
    }
}
