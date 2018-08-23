using CMS_DTO.CMSReservation;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSCustomers;
using CMS_Shared.CMSEmployees;
using CMS_Shared.CMSReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class ReservationController : HQController
    {
        private CMSReservationFactory _fac;
        private CMSCategoriesFactory _facCate;
        private CMSEmployeeFactory _facEmp;
        private CMSCustomersFactory _facCus;

        public ReservationController()
        {
            _fac = new CMSReservationFactory();
            _facCate = new CMSCategoriesFactory();
            _facEmp = new CMSEmployeeFactory();
            _facCus = new CMSCustomersFactory();
            ViewBag.ListFromTime = GetListFromTime();
            ViewBag.ListToTime = GetListToTime();
        }
        // GET: Reservation
        public ActionResult Index()
        {
            var model = new CMS_ReservationViewModels();
            try
            {
                model.ListCustomer = _facCus.GetList();
                model.ListCategories = _facCate.GetList().Where(o => o.ProductTypeCode == (int)CMS_Common.Commons.EProductType.Procudure).ToList();
                model.ListEmployees = _facEmp.GetList();
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Reservation Index", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Book(CMS_ReservationViewModels model)
        {
            try
            {
                var result = true; // _fac.CreateOrUpdate(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomerName", "Thông tin đặt chỗ không thành công!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Reservation Index", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}