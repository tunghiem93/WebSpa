using CMS_Common;
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
        [MaxLength(20, ErrorMessage = "Mã sản phẩm tối đa 20 kí tự")]
        public string ProductCode { get; set; }
        public string BarCode { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload { get; set; }
        public byte[] PictureByte { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        [MaxLength(100, ErrorMessage = "Mã sản phẩm tối đa 100 kí tự")]
        public string ProceduresName { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string PrintOutText { get; set; }
        [MaxLength(2000, ErrorMessage = "Mô tả tối đa 2000 kí tự")]
        public string ShortDescription { get; set; }
        public string Preparation { get; set; }
        [AllowHtml]
        [MaxLength(2000, ErrorMessage = "Mô tả tối đa 2000 kí tự")]
        public string Process { get; set; }
        [AllowHtml]
        [MaxLength(2000, ErrorMessage = "Mô tả tối đa 2000 kí tự")]
        public string SpaTreatment { get; set; }
        public string Effect { get; set; }
        public string Duration { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá dịch vụ")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Giá dịch vụ phải lớn hơn 0")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CateSequence { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public int ProductTypeCode { get; set; }
        public decimal Quantity { get; set; }
        public int Limit { get; set; }
        public string StoreID { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsAutoAddToOrder { get; set; }
        public bool IsComingSoon { get; set; }
        public bool IsShowInReservation { get; set; }
        public double ExtraPrice { get; set; }
        public bool IsRecommend { get; set; }
        public string Measure { get; set; }

        public List<CMS_ProceduresModels> ListProceduresDTOChild { get; set; }
        public CMS_ProceduresModels()
        {
            IsActive = true;
            Measure = "0";
            ExpiredDate = Commons.MaxDate;
            ProductTypeCode = (byte)Commons.EProductType.Procudure;
            ListProceduresDTOChild = new List<CMS_ProceduresModels>();
        }
    }
}
