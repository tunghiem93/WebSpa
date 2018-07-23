using CMS_DTO.CMSCategories;
using CMS_DTO.CMSEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSAbout
{
    public class CMS_AboutViewModels
    {
        public List<CMSCategoriesModels> Categories { get; set; }
        public List<CMS_EmployeeModels> Employees { get; set; }  
        
        public CMS_AboutViewModels()
        {
            Categories = new List<CMSCategoriesModels>();
            Employees = new List<CMS_EmployeeModels>();
        }
    }
}
