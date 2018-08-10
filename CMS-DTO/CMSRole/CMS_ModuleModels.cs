using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSRole
{
    public class CMS_ModuelModels : CMS_BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string ParentID { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public CMS_ModuelModels()
        {
        }
    }
}
