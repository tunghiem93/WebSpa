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
                        ImageUrl = "~/Uploads/Silder/" +  list[i]
                    });
                }
            }
            System.Web.HttpContext.Current.Session["SliderSession"] = ListSlider;

            //For Procedures
            CMSProcedureFactory _facPro = new CMSProcedureFactory();
            var ListProce = new List<ProcedureUI>();
            var LstPro = _facPro.GetList().Skip(0).Take(9).OrderBy(o=>o.ProceduresName).ToList();
            if (LstPro != null && LstPro.Count > 0)
            {
                ListProce = LstPro.GroupBy(o => new { o.CategoryId, o.CategoryName }).Select(s=> new ProcedureUI
                {
                     CateID = s.Key.CategoryId,
                     CateName = s.Key.CategoryName,
                     ListProcedureUI = LstPro.OrderBy(p=>p.ProceduresName).Where(w=>w.CategoryId == s.Key.CategoryId).Select(q=> new ProcedureUI {
                         ID = q.Id,
                         Name = q.ProceduresName,
                     }).ToList()
                }).ToList();
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
    }
}