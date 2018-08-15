using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSDiscount
{
    public class CMS_DiscountModels : CMS_BaseModel
    {
        public string Id { get; set; }
        public string StoreID { get; set; }
        [MaxLength(50,ErrorMessage ="Tên giảm giá tối đa 50 kí tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã giảm giá")]
        [MaxLength(50, ErrorMessage = "Mã giảm giá tối đa 50 kí tự")]
        public string DiscountCode { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string ValueText { get { return Value.ToString() + (IsPercent ? "%" : " VNĐ"); } }
        public bool IsAllowOpenValue { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApplyTotalBill { get; set; }
        public bool IsPercent { get; set; }
        public CMS_DiscountModels()
        {
            StoreID = "Spa";
            IsActive = true;
        }
    }
}
