using CMS_DTO.CMSCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSOrder
{
    public class CMS_CheckOutModels
    {
        public List<CMS_ItemModels> ListItem { get; set; }
        public CMS_CustomerAnonymousModels Customer { get; set; }
        public double TotalPrice { get; set; }
        public double SubTotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public string StoreID { get; set; }
        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }
        public bool IsError { get; set; }
        public bool IsTemp { get; set; }
        public string DiscountID { get; set; }
        public float DiscountValue { get; set; }
        public byte DiscountType { get; set; }
        public byte OrderType { get; set; }
        public CMS_CheckOutModels()
        {
            ListItem = new List<CMS_ItemModels>();
            StoreID = "Spa";
            Customer = new CMS_CustomerAnonymousModels();
            IsTemp = true;// client
        }
    }
}
