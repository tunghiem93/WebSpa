using CMS_DTO.CMSLocation;
using CMS_Shared.CMSDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class LocationController : HQController
    {
        private CMSDiscountFactory _facDis;
        public LocationController()
        {
            _facDis = new CMSDiscountFactory();
        }
        // GET: Location
        public ActionResult Index()
        {
            CMSLocationModels model = new CMSLocationModels();
            model.LstDiscount = _facDis.GetList();
            return View(model);
        }
    }
}