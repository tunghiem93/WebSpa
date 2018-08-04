using CMS_DTO.CMSProcedures;
using CMS_Shared.CMSDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ProceduresController : Controller
    {
        private CMSDiscountFactory _facDis;
        public ProceduresController()
        {
            _facDis = new CMSDiscountFactory();
        }
        // GET: Procedures
        public ActionResult Index()
        {
            var models = new CMS_ProceduresViewModels();
            try
            {
                models.LstDiscount = _facDis.GetList();
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Procedures_Index", ex);
            }
            return View(models);
        }
    }
}