using CMS_DTO.CMSOrder;
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
        public CMSOrdersController()
        {
            _fac = new CMSOrderFactory();
            _facPro = new CMSProductFactory(); 
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
                model = _fac.GetListOrder();
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
                    },
                    ListItem = Order.Items,
                    TotalPrice = Order.Items != null ? Order.Items.Sum(o => o.TotalPrice) : 0,
                    SubTotalPrice = Order.Items != null ? Order.Items.Sum(o => o.TotalPrice) : 0,
                    IsTemp = false //admin
                };
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
    }
}