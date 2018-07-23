using CMS_DTO.CMSCategories;
using CMS_DTO.CMSEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSReservation
{
    public class CMS_ReservationViewModels
    {
        public List<CMSCategoriesModels> Categories { get; set; }
        public List<CMS_EmployeeModels> Employees { get; set; }

        public CMS_ReservationViewModels()
        {
            Categories = new List<CMSCategoriesModels>();
            Employees = new List<CMS_EmployeeModels>();
        }
    }
}
