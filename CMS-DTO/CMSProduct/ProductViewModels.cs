using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCompany;
using CMS_DTO.CMSEmployee;
using CMS_DTO.CMSNews;
using CMS_DTO.CMSProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProduct
{
    public class ProductViewModels
    {
        public List<CMS_ProductsModels> ListProduct { get; set; }
        public CMS_CompanyModels Company { get; set; }
        public List<CMS_NewsModels> ListNews { get; set; }
        public List<CMS_EmployeeModels> ListEmployee { get; set; }
        public List<CMS_ProceduresModels> ListProcedures { get; set; }

        public string CateID { get; set; }
        public List<CMSCategoriesModels> ListCate { get; set; }
        public int TotalProduct { get; set; }
        public bool IsAddMore { get; set; }
        public int TotalPage { get; set; }
        public string Key { get; set; }
        public bool IsOrther { get; set; }
        public ProductViewModels()
        {
            ListCate = new List<CMSCategoriesModels>();
            ListProduct = new List<CMS_ProductsModels>();
            Company = new CMS_CompanyModels();
            ListNews = new List<CMS_NewsModels>();
            ListEmployee = new List<CMS_EmployeeModels>();
            ListProcedures = new List<CMS_ProceduresModels>() {
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Aromatheraphy",
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures2.jpg",
                    ProceduresName = "Skin Care",
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias provident destiny is about voles.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures3.jpg",
                    ProceduresName = "Herbal Spa",
                }
            };
        }
    }
}
