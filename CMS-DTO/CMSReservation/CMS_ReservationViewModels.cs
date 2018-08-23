using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSEmployee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSReservation
{
    public class CMS_ReservationViewModels
    {
        public string Id { get; set; }
        public string CustomerID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string CustomerName { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập e-mail")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Vui lòng nhập số")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BookDay { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string sStatus { get; set; }
        public bool IsActive { get; set; }
        public List<CustomerModels> ListCustomer { get; set; }
        public List<CMSCategoriesModels> ListCategories { get; set; }
        public List<CMS_EmployeeModels> ListEmployees { get; set; }

        public CMS_ReservationViewModels()
        {
            ListCustomer = new List<CustomerModels>();
            ListCategories = new List<CMSCategoriesModels>();
            ListEmployees = new List<CMS_EmployeeModels>();
        }
    }
}
