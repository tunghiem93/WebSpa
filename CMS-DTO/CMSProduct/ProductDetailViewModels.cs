using CMS_DTO.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProduct
{
    public class ProductDetailViewModels
    {
        public CMS_ProductsModels Product { get; set; }
        public List<CMS_ProductsModels> ListProduct { get; set; }
        public List<CMS_ProductsModels> ProductNew { get; set; }
        public List<CMS_CategoryViewModels> Categories { get; set; }
        public string KeyWord { get; set; }

        public ProductDetailViewModels()
        {
            Product = new CMS_ProductsModels();
            ListProduct = new List<CMS_ProductsModels>();
            Categories = new List<CMS_CategoryViewModels>();
        }
    }
}
