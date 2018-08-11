using CMS_DTO.CMSOrder;
using CMS_Shared.CMSOrders;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSOrdersController : HQController
    {
        private CMSOrderFactory _fac;
        public CMSOrdersController()
        {
            _fac = new CMSOrderFactory();
        }
        // GET: Admin/CMSOrders
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getDetail(string id)
        {
            var model = new CMS_OrderModels();
            try
            {
                model = _fac.GetDetailOrder(id);
                if(model != null)
                {
                    model.sCreatedDate = model.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("getDetail_Order:", ex);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadGrid()
        {
            var model = new List<CMS_OrderModels>();
            try
            {
                model = _fac.GetListOrder();
                if(model != null && model.Any())
                {
                    model.ForEach(o =>
                    {
                        o.sCreatedDate = o.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                    });
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("LoadGrid_Order : ", ex);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}