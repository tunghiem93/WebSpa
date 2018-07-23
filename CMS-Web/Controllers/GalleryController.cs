using CMS_DTO.CMSGallery;
using CMS_Shared.CMSGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class GalleryController : Controller
    {
        private CMSGalleryFactory _fac;
        public GalleryController()
        {
            _fac = new CMSGalleryFactory();
        }
        // GET: Gallery
        public ActionResult Index()
        {
            var model = new CMS_GalleryViewModels();
            try
            {
                var data = _fac.getList();
                model.Gallerys = data;
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Index Gallery", ex);
            }
            return View(model);
        }
    }
}