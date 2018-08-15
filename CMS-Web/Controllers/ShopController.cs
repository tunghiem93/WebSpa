using CMS_DTO.CMSCategories;
using CMS_DTO.CMSOrder;
using CMS_DTO.CMSSession;
using CMS_DTO.CMSShop;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSOrders;
using CMS_Shared.CMSProducts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ShopController : HQController
    {
        private CMSProductFactory _fac;
        private CMSOrderFactory _facOrder;
        private CMSCategoriesFactory _facCate;
        public ShopController()
        {
            _fac = new CMSProductFactory();
            _facCate = new CMSCategoriesFactory();
            _facOrder = new CMSOrderFactory();
        }
        // GET: Shop
        public ActionResult Index()
        {
            var models = new CMS_ShopViewModels();
            try
            {
                var data =  _fac.GetList().ToList();
                if(data != null)
                {
                    models.ProductNew = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(3).ToList();
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

        public ActionResult Cart()
        {
            CMS_OrderViewModels model = new CMS_OrderViewModels();
            try
            {
                var _Orders = GetListOrderCookie();
                NSLog.Logger.Info("List Order Cookie", JsonConvert.SerializeObject(_Orders));
                if(_Orders != null && _Orders.Any())
                {
                    var ItemIds = _Orders.Select(x => x.ItemId).ToList();
                    var data = _fac.GetList().Where(o => ItemIds.Contains(o.Id))
                                             .Select(o => new CMS_ItemModels
                                             {
                                                 Price = o.ProductPrice,
                                                 ProductID = o.Id,
                                                 ProductName = o.ProductName,
                                                 Quantity = o.Quantity
                                             }).ToList();
                    if(data != null && data.Any())
                    {
                       data.ForEach(o =>
                       {
                           var item = _Orders.FirstOrDefault(z => z.ItemId.Equals(o.ProductID));
                           o.Quantity = item.Quantity;
                           o.TotalPrice = Convert.ToDouble(o.Price * item.Quantity);
                       });
                        model.ListItem = data;
                        model.TotalPrice = data.Sum(o => o.TotalPrice);
                        model.SubTotalPrice = data.Sum(o => o.TotalPrice);
                    }
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Cart", ex);
            }
            return View(model);
        }

        public ActionResult CheckOut()
        {
            CMS_CheckOutModels model = new CMS_CheckOutModels();
            try
            {
                var _Orders = GetListOrderCookie();
                NSLog.Logger.Info("List Order Cookie", JsonConvert.SerializeObject(_Orders));
                if (_Orders != null && _Orders.Any())
                {
                    var ItemIds = _Orders.Select(x => x.ItemId).ToList();
                    var data = _fac.GetList().Where(o => ItemIds.Contains(o.Id))
                                             .Select(o => new CMS_ItemModels
                                             {
                                                 Price = o.ProductPrice,
                                                 ProductID = o.Id,
                                                 ProductName = o.ProductName,
                                                 Quantity = o.Quantity
                                             }).ToList();
                    if (data != null && data.Any())
                    {
                        data.ForEach(o =>
                        {
                            var item = _Orders.FirstOrDefault(z => z.ItemId.Equals(o.ProductID));
                            o.Quantity = item.Quantity;
                            o.TotalPrice = Convert.ToDouble(o.Price * item.Quantity);
                        });
                        model.ListItem = data;
                        model.TotalPrice = data.Sum(o => o.TotalPrice);
                        model.SubTotalPrice = data.Sum(o => o.TotalPrice);
                    }
                }

                /* get information customer from session */
                if(Session["UserClient"] != null)
                {
                    var CusInfo = Session["UserClient"] as UserSession;
                    model.Customer.FirstName = CusInfo.FirstName;
                    model.Customer.LastName = CusInfo.LastName;
                    model.Customer.Phone = CusInfo.Phone;
                    model.Customer.Email = CusInfo.Email;
                    model.Customer.Address = CusInfo.Address;
                    model.Customer.Id = CusInfo.UserId;
                    model.Customer.PostCode = CusInfo.PostCode;
                    model.Customer.City = CusInfo.City;
                    model.Customer.Country = CusInfo.Country;
                }
                
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("CheckOut", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckOut(CMS_CheckOutModels model)
        {
            try
            {
                var _Orders = GetListOrderCookie();
                NSLog.Logger.Info("List Order Cookie", JsonConvert.SerializeObject(_Orders));
                if (_Orders != null && _Orders.Any())
                {
                    var ItemIds = _Orders.Select(x => x.ItemId).ToList();
                    var data = _fac.GetList().Where(o => ItemIds.Contains(o.Id))
                                             .Select(o => new CMS_ItemModels
                                             {
                                                 Price = o.ProductPrice,
                                                 ProductID = o.Id,
                                                 ProductName = o.ProductName,
                                                 Quantity = o.Quantity
                                             }).ToList();
                    if (data != null && data.Any())
                    {
                        data.ForEach(o =>
                        {
                            var item = _Orders.FirstOrDefault(z => z.ItemId.Equals(o.ProductID));
                            o.Quantity = item.Quantity;
                            o.TotalPrice = Convert.ToDouble(o.Price * item.Quantity);
                        });
                        model.ListItem = data;
                        model.TotalPrice = data.Sum(o => o.TotalPrice);
                        model.SubTotalPrice = data.Sum(o => o.TotalPrice);
                    }
                    var result =  _facOrder.CreateOrder(model);
                    if(result)
                    {
                        HttpCookie currentUserCookie = HttpContext.Request.Cookies["cms-order"];
                        HttpContext.Response.Cookies.Remove("cms-order");
                        currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                        currentUserCookie.Value = null;
                        HttpContext.Response.SetCookie(currentUserCookie);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        model.IsError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("CheckOut", ex);
            }
            return View(model);
        }
    }
}