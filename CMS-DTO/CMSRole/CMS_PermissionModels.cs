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
    public class CMS_PermissionModels : CMS_BaseModel
    {
        public string Id { get; set; }
        public string StoreID { get; set; }
        public string RoleID { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string IsView { get; set; }
        public string IsAction { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public CMS_PermissionModels()
        {
            StoreID = "Spa";
        }
    }
}
