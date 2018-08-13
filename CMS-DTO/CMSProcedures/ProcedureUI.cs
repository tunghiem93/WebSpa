using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProcedures
{
    public class ProcedureUI
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CateID { get; set; }
        public string CateName { get; set; }
        public List<ProcedureUI> ListProcedureUI { get; set; }
        public ProcedureUI()
        {
            ListProcedureUI = new List<ProcedureUI>();
        }
    }
}
