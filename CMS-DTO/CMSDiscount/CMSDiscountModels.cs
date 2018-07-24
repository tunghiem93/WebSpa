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
        [Required(ErrorMessage="Vui lòng nhập tên thể loại")]
        [MaxLength(60,ErrorMessage ="Tên thể loại tối đa 250 kí tự")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAllowOpenValue { get; set; }
        public bool IsApplyTotalBill { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte ValueType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public CMSDiscountModels()
        {
            IsActive = true;
        }
    }
}
