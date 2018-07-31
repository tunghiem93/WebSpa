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
                    ShortDescription = "Trong cuộc sống nhộn nhịp với bao lo toan, các chị em phụ nữ nên dành cho mình ít thời gian massage thư giãn làm đẹp, đồng thời massage cũng  kích thích các nguyệt đạo cơ thể giúp giảm stress cũng như làm lưu thông mạch máu cơ thể.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures1.jpg",
                    ProceduresName = "Massage",
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "Thanh lọc da tiêu chuẩn, Cung cấp sức sống cho làn da, Liệu pháp giảm căng thẳng bằng hương thơm, Chăm sóc da cơ bản bằng vitamin C, Chăm sóc da khô và lão hóa bằng vitamin C, Chăm sóc da bằng thuốc bắc, Chăm sóc da bằng mật ong sữa chua.",
                    ImageUrl =CMS_Common.Commons._PublicImages+ "Procedures/popular-procedures2.jpg",
                    ProceduresName = "Chăm sóc da",
                },
                new CMS_ProceduresModels()
                {
                    ShortDescription = "Thảo dược là một trong những bí quyết làm đẹp an toàn, hiệu quả đến từ tự nhiên. Thành phần khoáng chất, dinh dưỡng chứa trong thảo dược giúp duy trì độ săn chắc của làn da, giúp tái tạo các tế bào mới, cho làn da tươi sáng và khoẻ mạnh hơn.",
                    ImageUrl = CMS_Common.Commons._PublicImages + "Procedures/popular-procedures3.jpg",
                    ProceduresName = "Spa thảo dược",
                }
            };
        }
    }
}
