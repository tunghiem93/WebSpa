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
                model.ListNewsNew = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(10).ToList();
                model.TotalBlog = data.Count;
                var pageIndex = 0;
                if (data.Count % 10 == 0)
                    pageIndex = data.Count / 10;
                else
                    pageIndex = Convert.ToInt32(data.Count / 10) + 1;

                if (pageIndex > 1)
                { 
                    model.TotalPage = 2;
                    model.IsAddMore = true;
                }
                else
                    model.IsAddMore = false;
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

        public PartialViewResult LoadPagging(int pageIndex)
        {
            CMS_NewsViewModel model = new CMS_NewsViewModel();
            var data = _fac.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(10 * pageIndex).ToList();
            if (data != null && data.Any())
            {
                data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + "News/" + x.ImageURL;
                });

                model.TotalBlog = data.Count;
                model.ListNews = data;
                model.ListNewsNew = data;
                var page = 0;
                if (data.Count % 10 != 0)
                    page = Convert.ToInt32(data.Count / 10) + 1;
                if (pageIndex > 2)
                {
                    if (page > pageIndex)
                    {
                        model.First = pageIndex - 1;
                        model.Later = pageIndex ;
                        model.IsAddMore = true;
                    }
                    else
                    {
                        model.First = pageIndex - 2;
                        model.Later = pageIndex - 1;
                        model.IsAddMore = false;
                    }
                }
                else
                {
                    if (page > pageIndex)
                    {
                        model.First = pageIndex + 1;
                        model.Later = pageIndex + 2;
                        model.IsAddMore = true;
                    }
                    else
                    {
                        model.First = pageIndex;
                        model.Later = pageIndex + 1;
                        model.IsAddMore = false;
                    }
                }

            }
            return PartialView("_ListItem", model);
        }
    }
}