using CMS_DTO.CMSCategories;
using CMS_DTO.CMSShop;
using CMS_Shared;
using CMS_Shared.CMSCategories;
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
        private CMSCategoriesFactory _facCate;
        public ShopController()
        {
            _fac = new CMSProductFactory();
            _facCate = new CMSCategoriesFactory();
        }
        // GET: Shop
        public ActionResult Index()
        {
            var models = new CMS_ShopViewModels();
            try
            {
                var data =  _fac.GetList();
                if(data != null)
                {
                    models.ProductNew = data.OrderBy(x => x.CreatedDate).Skip(0).Take(3).ToList();
                }
                var _cate = _facCate.GetListProductCate();
                models.Products = data;
                models.Categories = _cate;
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Index Shop", ex);
            }
            return View(models);
        }
    }
}