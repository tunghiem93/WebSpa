using CMS_DTO.CMSProduct;
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
                    if (!string.IsNullOrEmpty(_product.ImageURL))
                        _product.ImageURL = Commons._PublicImages + "Products/" + _product.ImageURL;

                    var data = _fac.GetList();
                    if (data != null)
                    {
                        data.ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.ImageURL))
                                x.ImageURL = Commons._PublicImages + "Products/" + x.ImageURL;
                        });

                        /* get list product by category */
                        models.ListProduct = data.Where(o => o.CategoryId.Equals(_product.CategoryId) && !o.Id.Equals(product_id)).ToList();
                        /* get top 3 product new */
                        models.ProductNew = data.OrderBy(x => x.CreatedDate).Skip(0).Take(3).ToList();
                        /* get list product category */
                        var _cate = _facCate.GetListProductCate();
                        models.Product = _product;
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
    }
}