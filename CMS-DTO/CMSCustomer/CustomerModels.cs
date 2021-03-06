﻿using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS_DTO.CMSCustomer
{
    public class CustomerModels
    {
        public string ID { get; set; }
        public string FbID { get; set; }
        public string GoogleID { get; set; }
        //[Required(ErrorMessage = "Làm ơn nhập tên đầy đủ!")]
        public string Name { get { return (this.FirstName + " " + this.LastName); } }
        //[Required(ErrorMessage = "Xin hãy nhập họ của bạn!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Xin hãy nhập tên!")]
        public string LastName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Xin hãy nhập số điện thoại!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Xin hãy nhập số điện thoại!")]
        public string Phone { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public bool MaritalStatus { get; set; }
        public string Postcode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }        
        public List<SelectListItem> ListMarital { get; set; }
        public List<SelectListItem> ListGender { get; set; }
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ImageURL { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload { get; set; }
        public byte[] PictureByte { get; set; }

        public List<CMS_OrderModels> ListOrders { get; set; }
        public bool IsShow { get; set; }
        public CustomerModels()
        {
            IsActive = true;
            BirthDate = new DateTime(1990, 01, 01);
            ListMarital = new List<SelectListItem>()
            {
                new SelectListItem() {  Text = "Độc thân", Value = "False"},
                new SelectListItem() { Text = "Kết hôn", Value = "True"}
            };

            ListGender = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Nam", Value = "False"},
                new SelectListItem() {  Text = "Nữ", Value = "True"},
            };
            ListOrders = new List<CMS_OrderModels>();
        }
    }
}
