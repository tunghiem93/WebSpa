using CMS_DTO.CMSContact;
using CMS_DTO.CMSLocation;
using CMS_Shared.CMSDiscount;
using CMS_Shared.Utilities;
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

        [HttpPost]
        public ActionResult SendToMail(CMSLocationModels model)
        {
            try
            {
                string msg = "";
                var result = CommonHelper.ContactAdmin(model.ContactDTO);
                if (result)
                {
                    return RedirectToAction("Index", "Location");
                }
                else
                {
                    ModelState.AddModelError("ContactDTO.Name", msg);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Contact_Email", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}