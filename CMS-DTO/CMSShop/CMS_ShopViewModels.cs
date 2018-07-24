using CMS_DTO.CMSCategories;
using CMS_DTO.CMSProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSShop
{
    public class CMS_ShopViewModels
    {
        public List<CMS_ProductsModels> Products { get; set; }
        public List<CMS_CategoryViewModels> Categories { get; set; }
        public int TotalPage { get; set; }
    }
}
