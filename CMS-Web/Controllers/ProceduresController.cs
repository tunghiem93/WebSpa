using CMS_DTO.CMSProcedures;
using CMS_Shared.CMSDiscount;
using CMS_Shared.CMSProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ProceduresController : Controller
    {
        private CMSProcedureFactory _facProce;
        private CMSDiscountFactory _facDis;
        public ProceduresController()
        {
            _facProce = new CMSProcedureFactory();
            _facDis = new CMSDiscountFactory();
        }
        // GET: Procedures
        public ActionResult Index()
        {
            var models = new CMS_ProceduresViewModels();
            try
            {
                var data = _facProce.GetList();
                if (data != null && data.Any())
                {
                    models.ListProcedures = data.GroupBy(o => new { o.CategoryId, o.CategoryName }).Select(s => new CMS_ProceduresModels {
                        CategoryId = s.Key.CategoryId,
                        CategoryName = s.Key.CategoryName,
                        ListProceduresDTOChild = data.OrderBy(o => o.ProceduresName).Where(w => w.CategoryId == s.Key.CategoryId).ToList(),
                    } ).ToList();
                }
                models.LstDiscount = _facDis.GetList(true);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Procedures_Index", ex);
            }
            return View(models);
        }
    }
}