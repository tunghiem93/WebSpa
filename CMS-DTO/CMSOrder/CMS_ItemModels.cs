using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSOrder
{
    public class CMS_ItemModels
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string ProductName { get; set; }
        public double TotalPrice { get; set; }
        public string DiscountID { get; set; }
        public double DiscountValue { get; set; }
        public byte DiscountType { get; set; }
        public string Description { get; set; }
    }
}
