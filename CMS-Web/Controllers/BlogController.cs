using CMS_Common;
using CMS_DTO.CMSNews;
using CMS_Shared;
using CMS_Shared.CMSNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class BlogController : Controller
    {
        private CMSNewsFactory _fac;
        public BlogController()
        {
            _fac = new CMSNewsFactory();
        }
        // GET: Blog
        public ActionResult Index()
        {
            var model = new CMS_NewsViewModel();
            var data = _fac.GetList().OrderByDescending(x => x.CreatedDate).ToList();
            if (data != null)
            {
                data.ForEach(x =>
                {
                    x.ImageURL = Commons.HostImage + "News/" + x.ImageURL;
                });

                model.ListNews = data;
                model.ListNewsNew = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(9).ToList();
            }
            return View(model);
        }
        public ActionResult BlogDetail(string id)
        {
            var model = new CMS_NewsViewModel();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "NotFound");
                }
                else
                {
                    var data = _fac.GetDetail(id);
                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.ImageURL))
                        {
                            data.ImageURL = Commons.HostImage + "News/" + data.ImageURL;
                        }
                    }

                    model.CMS_News = data;              
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "NotFound");
            }
            return View(model);
        }
    }
}