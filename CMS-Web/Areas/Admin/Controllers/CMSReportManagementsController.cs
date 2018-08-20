using CMS_Common;
using CMS_DTO.CMSReport;
using CMS_Shared.CMSReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class CMSReportManagementsController : Controller
    {
        CMSReportFactory _fac = null;
        public CMSReportManagementsController()
        {
            _fac = new CMSReportFactory();
        }
        // GET: Admin/ReportManagement
        public ActionResult Index()
        {
            CMS_ReportModels model = new CMS_ReportModels();
            return View(model);
        }

        public ActionResult Report(CMS_ReportModels model)
        {
            // Check date
            if (model.From > model.To)
            {
                ModelState.AddModelError("From", "Vui lòng chọn lại thời gian!");
            }
            

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("Index", model);
            }

            var wb = _fac.Report(model);
            ViewBag.wb = wb;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = UTF8Encoding.UTF8.WebName;
            Response.ContentEncoding = UTF8Encoding.UTF8;
            string sheetName = string.Format("Báo cáo_Thu_Chi_{0}", DateTime.Now.ToString("MMddyyyy")).Replace(" ", "_");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", String.Format(@"attachment;filename={0}.xlsx", sheetName));
            using (var memoryStream = new System.IO.MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(HttpContext.Response.OutputStream);
                memoryStream.Close();
            }
            HttpContext.Response.End();
            return RedirectToAction("Index");
        }
    }
}