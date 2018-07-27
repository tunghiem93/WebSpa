using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProcedures
{
    public class CMS_ProceduresModels
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProceduresName { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Preparation { get; set; }
        public string Process { get; set; }
        public string SpaTreatment { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
    }
}
