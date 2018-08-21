using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSSupport
{
    public class CMS_SupportModels
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object RawData { get; set; }

        public CMS_SupportModels()
        {
            Success = false;
        }
    }

}
