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
using CMS_Shared.Utilities;
using CMS_Shared.CMSEmployees;
using CMS_Common;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSOrdersController : HQController
    {
        private CMSOrderFactory _fac;
        private CMSProductFactory _facPro;
        private CMSCustomersFactory _facCus;
        private CMSDiscountFactory _facDiscount;
        private CMSEmployeeFactory _facEmp;
        public CMSOrdersController()
        {
            _fac = new CMSOrderFactory();
            _facPro = new CMSProductFactory();
            _facCus = new CMSCustomersFactory();
            _facDiscount = new CMSDiscountFactory();
            _facEmp = new CMSEmployeeFactory();
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
                model = _fac.GetListOrder().Where(o => o.OrderType == (byte)CMS_Common.Commons.EOrderType.Normal).ToList();
                if(model != null && model.Any())
                {
                    model.ForEach(o =>
                    {
                        o.sCreatedDate = o.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
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
            model.Employees = _facEmp.GetList();
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
                    TotalPrice = Order.TotalPrice,
                    TotalDiscount = Order.TotalDiscount,
                    SubTotalPrice = Order.SubTotal,
                    ListItem = Order.Items,
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
                }
                var OrderId = string.Empty;
                var result = _fac.CreateOrder(model,ref OrderId);
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

        public JsonResult Delete(string id,string Reason)
        {
            var status = 200;
            try
            {
                var result = _fac.Delete(id,Reason);
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
            var pathReceipt = Commons._PublicImages + "Receipt/" + Id + ".pdf";
            try
            {
                model = _fac.GetDetailOrder(Id);
                if (model != null)
                {
                    model.sCreatedDate = model.CreatedDate.ToString("dd/MM/yyyy hh:mm tt");
                }

                var path = Server.MapPath("~/Uploads/Receipt/" + model.Id + ".pdf");
                PrintHelper.Print(model, path);

            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("getDetail_Order:", ex);
            }
            return Redirect(pathReceipt);

            //return View("Print", model);
        }

        public JsonResult Checkout(string id)
        {
            var status = 200;
            try
            {
                NSLog.Logger.Info("Checkout_Request:", id);
                var result = _fac.CheckOut(id, CurrentUser.UserId);
                if (!result)
                    status = 500;
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Checkout_Admin:", ex);
                status = 500;
            }
            var obj = new
            {
                Status = status
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}