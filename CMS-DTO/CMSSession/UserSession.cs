﻿using CMS_DTO.CMSRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSSession
{
    public class UserSession
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdminClient { get; set; }
        public string CurrencySymbol { get; set; }
        public bool RememberMe { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<CMS_PermissionModels> ListPermision { get; set; }

        public UserSession()
        {
        }
    }
}
