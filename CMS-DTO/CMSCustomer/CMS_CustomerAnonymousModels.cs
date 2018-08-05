﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class CMS_CustomerAnonymousModels
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập họ!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập tên!")]
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập tên thành phố")]
        public string City { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập số điện thoại")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Làm ơn nhập số điện thoại!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập tên quốc gia")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập địa chỉ giao hàng")]
        public string Address { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập mã bưu điện")]
        public string PostCode { get; set; }
    }
}
