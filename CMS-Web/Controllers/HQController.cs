using CMS_DTO.CMSProcedures;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSProcedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class HQController : Controller
    {
        public HQController()
        {
            var _Path = HostingEnvironment.MapPath("~/Uploads/Silder/");
            var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();
            var ListSlider = new List<SliderSession>();
            if (list != null && list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    ListSlider.Add(new SliderSession
                    {
                        ImageUrl = "~/Uploads/Silder/" + list[i]
                    });
                }
            }
            System.Web.HttpContext.Current.Session["SliderSession"] = ListSlider;

            //For Procedures
            CMSProcedureFactory _facPro = new CMSProcedureFactory();
            var ListProce = new List<ProcedureUI>();
            var LstPro = _facPro.GetList();
            if (LstPro != null && LstPro.Count > 0)
            {
                ListProce = LstPro.GroupBy(o => new { o.CategoryId, o.CategoryName }).Select(s => new ProcedureUI
                {
                    CateID = s.Key.CategoryId,
                    CateName = s.Key.CategoryName,
                    ListProcedureUI = LstPro.OrderBy(p => p.ProceduresName).Where(w => w.CategoryId == s.Key.CategoryId).Select(q => new ProcedureUI
                                        {
                                            ID = q.Id,
                                            Name = q.ProceduresName,
                                        }).OrderBy(p => p.Name).ToList()
                }).OrderBy(o => o.CateName).ToList();
            }
            System.Web.HttpContext.Current.Session["ProcedureUI"] = ListProce;

        }

        public List<OrderCookie> GetListOrderCookie()
        {
            if (Request.Cookies["cms-order"] != null)
            {
                var _Orders = Request.Cookies["cms-order"].Value;
                var strOrder = Server.UrlDecode(_Orders);
                var ListOrder = JsonConvert.DeserializeObject<List<OrderCookie>>(strOrder);
                return ListOrder;
            }
            return null;
        }

        public List<SelectListItem> GetListFromTime()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                 new SelectListItem() {Text = "9 am", Value= "9" },
                 new SelectListItem() {Text = "10 am", Value= "10" },
                 new SelectListItem() {Text = "11 am", Value= "11" },
                 new SelectListItem() {Text = "12 pm", Value= "12" },
                 new SelectListItem() {Text = "1 pm", Value= "13" },
                 new SelectListItem() {Text = "2 pm", Value= "14" },
                 new SelectListItem() {Text = "3 pm", Value= "15" },
                 new SelectListItem() {Text = "4 pm", Value= "16" },
                 new SelectListItem() {Text = "5 pm", Value= "17" },
                 new SelectListItem() {Text = "6 pm", Value= "18" },
                 new SelectListItem() {Text = "7 pm", Value= "19" },
            };
            return data;
        }

        public List<SelectListItem> GetListToTime()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "9 am", Value= "9" },
                 new SelectListItem() {Text = "10 am", Value= "10" },
                 new SelectListItem() {Text = "11 am", Value= "11" },
                 new SelectListItem() {Text = "12 pm", Value= "12" },
                 new SelectListItem() {Text = "1 pm", Value= "13" },
                 new SelectListItem() {Text = "2 pm", Value= "14" },
                 new SelectListItem() {Text = "3 pm", Value= "15" },
                 new SelectListItem() {Text = "4 pm", Value= "16" },
                 new SelectListItem() {Text = "5 pm", Value= "17" },
                 new SelectListItem() {Text = "6 pm", Value= "18" },
                 new SelectListItem() {Text = "7 pm", Value= "19" },
            };
            return data;
        }
    }
}