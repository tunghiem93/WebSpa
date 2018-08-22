using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSReport
{
    public class CMS_ReportModels
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
        public bool IsIncludeDelete { get; set; }
        public CMS_ReportReceiptModels ReportReceipt { get; set; }
        public CMS_ReportExpensetModels ReportExpense { get; set; }
        public CMS_ReportModels()
        {
            From = DateTime.Now.AddDays(- 1);
            To = DateTime.Now;
        }
    }
}
