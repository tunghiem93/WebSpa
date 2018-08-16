﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSOrder
{
    public class CMS_OrderModels
    {
        public string Id { get; set; }
        public string OrderNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double? TotalBill { get; set; }
        public string Phone { get; set; }
        public string sCreatedDate { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public byte Status { get; set; }
        public int OffSet { get; set; }
        public string StrLastUpdate { get; set; }
        public bool Checked { get; set; }
        public string ValueDiscount { get; set; }
        public double? SubTotal { get; set; }
        public List<CMS_ItemModels> Items { get; set; }

        public CMS_OrderModels()
        {
            Items = new List<CMS_ItemModels>();
        }
    }
}
