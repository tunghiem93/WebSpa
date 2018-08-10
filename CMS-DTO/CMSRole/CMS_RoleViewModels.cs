using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSRole
{
    public class CMS_RoleViewModels
    {
        public List<CMS_RoleModels> LstChildren { get; set; }
        public CMS_RoleViewModels()
        {
            LstChildren = new List<CMS_RoleModels>();
        }
    }
}
