using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS_DTO.CMSProcedures
{
    public class CMS_ProceduresModels
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã sản phẩm")]
        [MaxLength(50, ErrorMessage = "Mã sản phẩm tối đa 50 kí tự")]
        public string ProductCode { get; set; }
        public string BarCode { get; set; }
        public HttpPostedFileBase[] PictureUpload { get; set; }
        public byte[] PictureByte { get; set; }
        public string ImageUrl { get; set; }
        public string ProceduresName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string PrintOutText { get; set; }
        public string ShortDescription { get; set; }
        public string Preparation { get; set; }
        public string Process { get; set; }
        public string SpaTreatment { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public int ProductTypeCode { get; set; }
        public double Quantity { get; set; }
        public int Limit { get; set; }
        public string StoreID { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsAutoAddToOrder { get; set; }
        public bool IsComingSoon { get; set; }
        public bool IsShowInReservation { get; set; }
        public double ExtraPrice { get; set; }
        public bool IsRecommend { get; set; }
    }
}
