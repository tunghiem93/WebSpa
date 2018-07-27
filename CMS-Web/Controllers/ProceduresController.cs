using CMS_DTO.CMSProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ProceduresController : Controller
    {
        // GET: Procedures
        public ActionResult Index()
        {
            var models = new CMS_ProceduresViewModels();
            try
            {

            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Procedures_Index", ex);
            }
            return View(models);
        }
    }
}