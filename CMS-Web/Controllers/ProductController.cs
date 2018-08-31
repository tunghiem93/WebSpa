using CMS_DTO.CMSProduct;
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
    public class ProductController : Controller
    {
        private CMSProductFactory _fac;
        private CMSCategoriesFactory _facCate;
        public ProductController()
        {
            _fac = new CMSProductFactory();
            _facCate = new CMSCategoriesFactory();
        }

        // GET: Product
        public ActionResult Index(string product_id)
        {
            NSLog.Logger.Info("Request_Product_Index", product_id);
            var models = new ProductDetailViewModels();
            try
            {
                if (string.IsNullOrEmpty(product_id))
                    return RedirectToAction("Index", "Home");
                var _product = _fac.GetDetail(product_id);
                if(_product != null)
                {
                    var data = _fac.GetList().Where(o => o.ProductTypeCode == (int)CMS_Common.Commons.EProductType.Product && o.IsActive).ToList();
                    if (data != null)
                    {
                        /* get list product by category */
                        models.ListProduct = data.Where(o => o.CategoryId.Equals(_product.CategoryId) && !o.Id.Equals(product_id)).ToList();
                        /* get top 3 product new */
                        models.ProductNew = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(3).ToList();
                        /* get list product category */
                        var _cate = _facCate.GetListProductCate();
                        models.Product = _product;
                        if(models.Product != null)
                        {
                            models.Product.ImageURL = models.Product.ListImages != null && models.Product.ListImages.Count > 0 ? models.Product.ListImages.FirstOrDefault().ImageURL : "";
                        }
                        models.Categories = _cate;
                    }
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Index Product", ex);
            }
            return View(models);
        }

        public ActionResult category(string d ,string i)
        {
            NSLog.Logger.Info("category Product", i);
            var models = new CMS_ShopViewModels();
            models.CategoryName = d;
            try
            {
                var data = _fac.GetList();
                if (data != null)
                {
                    models.ProductNew = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(3).ToList();

                    data = data.Where(o => o.CategoryId.Equals(i)).ToList();
                }
                var _cate = _facCate.GetListProductCate();
                models.Products = data;
                models.Categories = _cate;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("category Product", ex);
            }
            return View(models);
        }
    }
}