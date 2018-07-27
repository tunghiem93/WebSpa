using CMS_DTO.CMSBase;
using CMS_DTO.CMSCategories;
using CMS_DTO.CMSProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS_DTO.CMSNews
{
    public class CMS_NewsModels : CMS_BaseModel
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết")]
        [MaxLength(150, ErrorMessage = "Tiêu đề bài viết tối đa 150 kí tự")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả bài viết")]
        [MaxLength(500, ErrorMessage = "Mô tả bài viết tối đa 500 kí tự")]
        public string Short_Description { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [MaxLength(249, ErrorMessage = "Tên tác giả (tối đa 250 kí tự)")]
        public string Author { get; set; }
        [MaxLength(249, ErrorMessage = "Thể loại (tối đa 250 kí tự)")]
        public string Category { get; set; }
        [MaxLength(249, ErrorMessage = "Nguồn (tối đa 250 kí tự)")]
        public string Source { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string sStatus { get; set; }
        public List<SelectListItem> ListBlogType { get; set; }

        public CMS_NewsModels()
        {
            IsActive = true;
            ListBlogType = new List<SelectListItem>();
        }
    }

    public class CMS_NewsViewModel
    {
        public List<CMS_NewsModels> ListNews { get; set; }
        public List<CMS_NewsModels> ListNewsNew { get; set; }
        public CMS_NewsModels CMS_News { get; set; }
        public List<CMSCategoriesModels> ListCate { get; set; }
        public List<CMS_ProductsModels> ListProduct { get; set; }
        public int TotalBlog { get; set; }
        public bool IsAddMore { get; set; }
        public int TotalPage { get; set; }
        public int First { get; set; }
        public int Later { get; set; }

        public CMS_NewsViewModel()
        {
            First = 1;
            Later = 2;
            ListNews = new List<CMS_NewsModels>();
            ListNewsNew = new List<CMS_NewsModels>();
            CMS_News = new CMS_NewsModels();
            ListCate = new List<CMSCategoriesModels>();
            ListProduct = new List<CMS_ProductsModels>();
        }
    }
}
