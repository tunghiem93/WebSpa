﻿using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSOrder;
using CMS_Shared.CMSCustomers;
using CMS_Shared.CMSDiscount;
using CMS_Shared.CMSOrders;
using CMS_Shared.CMSProducts;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSOrdersController : HQController
    {
        private CMSOrderFactory _fac;
        private CMSProductFactory _facPro;
        private CMSCustomersFactory _facCus;
        private CMSDiscountFactory _facDiscount;

        public CMSOrdersController()
        {
            _fac = new CMSOrderFactory();
            _facPro = new CMSProductFactory();
            _facCus = new CMSCustomersFactory();
            _facDiscount = new CMSDiscountFactory();
        }
        // GET: Admin/CMSOrders
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getDetail(string id)
        {
            var model = new CMS_OrderModels();
            try
            {
                model = _fac.GetDetailOrder(id);
                
                if(model != null)
                {
                    model.sCreatedDate = model.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                    if(model.Items != null && model.Items.Any())
                    {
                        model.ValueDiscount = model.Items.Where(o => string.IsNullOrEmpty(o.ProductID)).Select(o => o.DiscountValue + " " + (o.DiscountType == (byte)CMS_Common.Commons.EValueType.Percent ? "%" : "Vnd")).FirstOrDefault();
                        var valueDiscount = model.Items.Where(o => string.IsNullOrEmpty(o.ProductID)).FirstOrDefault();
                        if(valueDiscount.DiscountType == (byte)CMS_Common.Commons.EValueType.Percent)
                        {
                            model.TotalBill = model.TotalBill - (model.TotalBill * valueDiscount.DiscountValue / 100);
                        }
                        else
                        {
                            model.TotalBill = model.TotalBill - valueDiscount.DiscountValue;
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("getDetail_Order:", ex);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadGrid()
        {
            var model = new List<CMS_OrderModels>();
            try
            {
                model = _fac.GetListOrder();
                if(model != null && model.Any())
                {
                    model.ForEach(o =>
                    {
                        o.sCreatedDate = o.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                        if (o.TotalDiscount.HasValue)
                            o.TotalBill = o.TotalDiscount;
                    });
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("LoadGrid_Order : ", ex);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            CMS_OrderAdminModels model = new CMS_OrderAdminModels();
            model.Items = _facPro.GetList().Select(o => new CMS_ItemModels {
                Price = o.ProductPrice,
                ProductID = o.Id,
                ProductName = o.ProductName,
            }).OrderBy(o => o.ProductName).ToList();

            model.Customers = _facCus.GetList().Select(o => new CMS_CustomerAnonymousModels
            {
                FirstName = o.FirstName,
                LastName = o.LastName,
                Address = o.Address,
                Id = o.ID,
                City = o.City,
                Company = o.CompanyName,
                Country = o.Country,
                Email = o.Email,
                Phone = o.Phone,
                PostCode = o.Postcode
            }).OrderBy(o => o.Name).ToList();
            model.Discounts = _facDiscount.GetList();
            return PartialView("_Create", model);
        }
        [HttpPost]
        public JsonResult Create(CMS_OrderAdminModels Order)
        {
            var status = 200;
            try
            {
                var model = new CMS_CheckOutModels
                {
                    CreatedUser = CurrentUser.UserId,
                    ModifiedUser = CurrentUser.UserId,
                    Customer = new CMS_DTO.CMSCustomer.CMS_CustomerAnonymousModels
                    {
                        Address = Order.Address,
                        City = Order.City,
                        Company = Order.Company,
                        Country = Order.Country,
                        Description = Order.Description,
                        Email = Order.Email,
                        FirstName = Order.FirstName,
                        LastName = Order.LastName,
                        Phone = Order.Phone,
                        Id = Order.Id
                    },
                    ListItem = Order.Items,
                    TotalPrice = Order.Items != null ? Order.Items.Sum(o => o.TotalPrice) : 0,
                    SubTotalPrice = Order.Items != null ? Order.Items.Sum(o => o.TotalPrice) : 0,
                    IsTemp = false //admin
                };
                if(model != null && model.ListItem != null && model.ListItem.Any() && !string.IsNullOrEmpty(Order.DiscountID))
                {
                    model.ListItem.Add(new CMS_ItemModels
                    {
                        DiscountID = Order.DiscountID,
                        DiscountType = Order.DiscountType,
                        DiscountValue = Order.DiscountValue
                    });
                    if(Order.DiscountType == (byte)CMS_Common.Commons.EValueType.Percent)
                    {
                        model.TotalDiscount = model.TotalPrice - (model.TotalPrice * (Order.DiscountValue / 100));
                    }
                    else
                    {
                        model.TotalDiscount = model.TotalPrice - Order.DiscountValue;
                    }
                }
                var result = _fac.CreateOrder(model);
                if (!result)
                    status = 500;

            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Create_Error : ", ex);
            }
            var obj = new
            {
                Status = status,
            };
            return Json(obj,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string id)
        {
            var status = 200;
            try
            {
                var result = _fac.Delete(id);
                if (!result)
                    status = 500;
                NSLog.Logger.Info("Delete_Request:", id);
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Delete_Order:", ex);
            }
            var obj = new
            {
                Status = status
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(string Id)
        {
            var model = new CMS_OrderModels();
            try
            {
                model = _fac.GetDetailOrder(Id);
                if (model != null)
                {
                    model.sCreatedDate = model.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getDetail_Order:", ex);
            }
            return View("Print", model);
        }
    }
}