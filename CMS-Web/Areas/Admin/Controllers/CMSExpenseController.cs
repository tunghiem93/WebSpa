using CMS_Common;
using CMS_DTO.CMSOrder;
using CMS_Shared.CMSOrders;
using CMS_Shared.CMSProducts;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSExpenseController : HQController
    {
        private CMSOrderFactory _fac;
        private CMSProductFactory _facPro;
        public CMSExpenseController()
        {
            _fac = new CMSOrderFactory();
            _facPro = new CMSProductFactory();
            ViewBag.ListCus = GetListCustomer();
            ViewBag.ListExpenseType = GetListExpenseType();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _fac.GetListOrder();
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_OrderModels model = new CMS_OrderModels();
            return PartialView("_Create", model);
        }

        public CMS_OrderModels GetDetail(string Id)
        {
            return _fac.GetDetailOrder(Id);
        }

        [HttpPost]
        public ActionResult Create(CMS_OrderModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                var result = true; // _fac.CreateOrder(model);
                if (result)      
                    return RedirectToAction("Index");
                ModelState.AddModelError("OrderNo", "Không thể thêm mới order!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CMS_CheckOutModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                var result = true; // _fac.CreateOrder(model);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("OrderNo", "Không thể chỉnh order này!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public ActionResult View(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CMS_OrderModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                
                var result = _fac.Delete(model.Id);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("OrderNo", "không thể xóa order!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }

        public ActionResult LoadProducts()
        {
            var model = new CMS_OrderModels();
            model.LstProduct = _facPro.GetList();
            if (model.LstProduct != null && model.LstProduct.Any())
            {
                int index = 0;
                model.LstProduct.ForEach(o => {
                    o.OffSet = index++;
                });
            }
            return PartialView("_TableChooseProduct", model);
        }
    }
}