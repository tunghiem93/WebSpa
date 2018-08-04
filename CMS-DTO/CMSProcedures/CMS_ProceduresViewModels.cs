using CMS_DTO.CMSDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProcedures
{
    public class CMS_ProceduresViewModels
    {
        public List<CMS_ProceduresModels> ListProcedures { get; set; }
        public List<CMS_DiscountModels> LstDiscount { get; set; }

        public CMS_ProceduresViewModels()
        {
            LstDiscount = new List<CMS_DiscountModels>();
            ListProcedures = new List<CMS_ProceduresModels>();                
        }
    }
}
