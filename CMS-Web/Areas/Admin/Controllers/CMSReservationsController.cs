using CMS_DTO.CMSReservation;
using CMS_Shared.CMSReservation;
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
    public class CMSReservationsController : HQController
    {
        private CMSReservationFactory _fac;

        public CMSReservationsController()
        {
            _fac = new CMSReservationFactory();
            ViewBag.Category = GetListCateSelectItem();
            ViewBag.Employee = GetListEmployeeSelectItem();
            ViewBag.From = GetListFromTime();
            ViewBag.To = GetListToTime();
            //ViewBag.Category = GetListCategorySelectItem();
        }
        // GET: Admin/CMSReservations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = new List<CMS_ReservationViewModels>(); // _fac.GetList();
            model.ForEach(x =>
            {
                x.sStatus = x.IsActive ? "Kích hoạt" : "Chưa kích hoạt";
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_ReservationViewModels model = new CMS_ReservationViewModels();
            return PartialView("_Create", model);
        }

        public CMS_ReservationViewModels GetDetail(string Id)
        {
            CMS_ReservationViewModels model = new CMS_ReservationViewModels();
            return model; // _fac.GetDetail(Id);
        }

        [HttpPost]
        public ActionResult Create(CMS_ReservationViewModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                var msg = "";
                model.CreatedBy = CurrentUser.UserId;
                model.UpdatedBy = CurrentUser.UserId;
                var result = true; // _fac.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("CustomerName", msg);
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
        public ActionResult Edit(CMS_ReservationViewModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                
                var msg = "";
                model.CreatedBy = CurrentUser.UserId;
                model.UpdatedBy = CurrentUser.UserId;
                var result = true; // _fac.CreateOrUpdate(model, ref msg);
                if (result)
                {                    
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("CustomerName", msg);
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
        public ActionResult Delete(CMS_ReservationViewModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }                
                var msg = "";
                var result = true; // _fac.Delete(model.Id, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("CustomerName", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}