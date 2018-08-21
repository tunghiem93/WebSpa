using CMS_Common;
using CMS_DTO.CMSDiscount;
using CMS_DTO.CMSRole;
using CMS_DTO.CMSSupport;
using CMS_Shared.CMSRole;
using CMS_Shared.CMSSupport;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class CMSSupportController : HQController
    {
        public CMSSupportController()
        {
        }

        // GET: Admin/CMSDiscounts
        public ActionResult Index()
        {
            CMSSupportFactory factory = new CMSSupportFactory();
            var model = factory.IsDirty();
            return View(model);
        }
    }
}