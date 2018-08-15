using CMS_DTO.CMSCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSOrder
{
    public class CMS_OrderAdminModels
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public List<CMS_ItemModels> Items { get; set; }
        public List<CMS_CustomerAnonymousModels> Customers { get; set; }

        public CMS_OrderAdminModels()
        {
            Items = new List<CMS_ItemModels>();
            Customers = new List<CMS_CustomerAnonymousModels>();
        }
    }
}
