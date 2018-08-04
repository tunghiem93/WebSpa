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
    public class CMSDiscountModels : CMS_BaseModel
    {
        public string Id { get; set; }
        public string StoreID { get; set; }
        [Required(ErrorMessage="Vui lòng nhập tên giảm giá")]
        [MaxLength(60,ErrorMessage ="Tên giảm giá tối đa 250 kí tự")]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool IsAllowOpenValue { get; set; }
        public bool IsActive { get; set; }
        public string sStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApplyTotalBill { get; set; }
        public byte ValueType { get; set; }
        public CMSDiscountModels()
        {            
        }
    }
}
