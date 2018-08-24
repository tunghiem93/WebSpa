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
        [Required(ErrorMessage = "Vui lòng chọn liệu trình")]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Vui lòng nhập số")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BookDay { get; set; }
        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan FromTime { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public List<CustomerModels> ListCustomer { get; set; }
        public List<CMSCategoriesModels> ListCategories { get; set; }
        public List<CMS_EmployeeModels> ListEmployees { get; set; }
        public string StoreID { get; set; }
        public CMS_ReservationViewModels()
        {
            BookDay = DateTime.Now;
            StoreID = "Spa";
            ListCustomer = new List<CustomerModels>();
            ListCategories = new List<CMSCategoriesModels>();
            ListEmployees = new List<CMS_EmployeeModels>();
        }
    }
}
