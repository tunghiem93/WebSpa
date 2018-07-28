using CMS_Common;
using CMS_DTO.CMSImage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS_DTO.CMSProduct
{
    public class CMS_ProductsModels
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mã sản phẩm")]
        [MaxLength(50,ErrorMessage ="Mã sản phẩm tối đa 50 kí tự")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm")]
        [MaxLength(250,ErrorMessage ="Tên sản phẩm tối đa 250 kí tự")]
        public string ProductName { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập giá sản phẩm")]
        [Range(0,Int64.MaxValue,ErrorMessage ="Giá sản phẩm phải lớn hơn 0")]
        public double ProductPrice { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn thể loại")]
        public string CategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string PrintOutText { get; set; }

        public string CategoryName { get; set; }
        public string sStatus { get; set; }

        public HttpPostedFileBase[] PictureUpload { get; set; }
        public byte[] PictureByte { get; set; }
        public string ImageURL { get; set; }

        public int TypeCode { get; set; }
        public string BarCode { get; set; }
        public int Unit { get; set; }
        public string Measure { get; set; }
        public double Quantity { get; set; }
        public int Limit { get; set; }
        public double ExtraPrice { get; set; }
        public bool IsAllowedDiscount { get; set; }
        public bool IsCheckedStock { get; set; }
        public bool IsAllowedOpenPrice { get; set; }
        public bool IsPrintedOnCheck { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsAutoAddToOrder { get; set; }
        public bool IsComingSoon { get; set; }
        public bool IsShowInReservation { get; set; }
        public bool IsRecommend { get; set; }
        public string StoreID { get; set; }
        public int ProductTypeCode { get; set; }
        public List<CMS_ImagesModels> ListImages { get; set; }

        public CMS_ProductsModels()
        {
            IsActive = true;
            Measure = "times";
            ProductCode = "";
            BarCode = "";
            ExpiredDate = Commons.MaxDate;
            ListImages = new List<CMS_ImagesModels>();
        }
    }
}
