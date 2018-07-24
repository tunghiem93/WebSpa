using CMS_DTO.CMSShop;
using CMS_Shared.CMSProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ShopController : Controller
    {
        private CMSProductFactory _fac;
        public ShopController()
        {
            _fac = new CMSProductFactory();
        }
        // GET: Shop
        public ActionResult Index()
        {
            var models = new CMS_ShopViewModels();
            try
            {
                var data = _fac.GetList();
                models.Products = data;
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Index Shop", ex);
            }
            return View(models);
        }
    }
}