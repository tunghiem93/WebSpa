﻿using CMS_Common;
using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSOrder;
using CMS_Shared;
using CMS_Shared.CMSCustomers;
using CMS_Shared.CMSOrders;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSCustomersController : HQController
    {
        private CMSCustomersFactory _factory;
        private CMSOrderFactory _facOrder;
        public CMSCustomersController()
        {
            _factory = new CMSCustomersFactory();
            _facOrder = new CMSOrderFactory();
        }
        // GET: Admin/CMSCategories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList();
            model.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + "Customers/" + x.ImageURL;
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CustomerModels model = new CustomerModels();
            return PartialView("_Create", model);
        }

        public CustomerModels GetDetail(string Id)
        {
            var data = _factory.GetDetail(Id);
            if (data != null)
            {
                data.Password = CommonHelper.Decrypt(data.Password);
                data.ConfirmPassword = data.Password;
            }                
            return data;
        }

        [HttpPost]
        public ActionResult Create(CustomerModels model)
        {
            try
            {
                byte[] photoByte = null;
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageURL = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }

                var msg = "";
                var id = "";
                model.CreatedBy = CurrentUser.Email;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.CreateOrUpdate(model, ref id, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.ImageURL) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Employees/" + model.ImageURL);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL, ref photoByte);
                    }
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("FirstName", msg);
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = model.Password;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
            catch (Exception ex)
            {
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = model.Password;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = GetDetail(Id);
            if (!string.IsNullOrEmpty(model.ImageURL))
                model.ImageURL = Commons.HostImage + "Customers/" + model.ImageURL;
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModels model)
        {
            var temp = model.ImageURL;
            try
            {
                byte[] photoByte = null;
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                if (!string.IsNullOrEmpty(model.ImageURL))
                {
                    model.ImageURL = model.ImageURL.Replace(Commons._PublicImages, "").Replace("Customers/", "").Replace(Commons.Image200_100, "");
                    temp = model.ImageURL;
                }

                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageURL = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }

                var msg = "";
                var id = "";

                model.CreatedBy = CurrentUser.Email;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.CreateOrUpdate(model, ref id, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.ImageURL) && model.PictureByte != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Uploads/Customers/" + temp)))
                        {
                            ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Customers/" + temp));
                        }

                        var path = Server.MapPath("~/Uploads/Customers/" + model.ImageURL);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL, ref photoByte);
                    }
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("FirstName", msg);
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = model.Password;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
            catch (Exception ex)
            {
                model.Password = CommonHelper.Decrypt(model.Password);
                model.ConfirmPassword = model.Password;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public ActionResult View(string Id)
        {
            var model = GetDetail(Id);
            if (!string.IsNullOrEmpty(model.ImageURL))
                model.ImageURL = Commons.HostImage + "Customers/" + model.ImageURL;
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomerModels model)
        {
            try
            {
                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var msg = "";
                var result = _factory.Delete(model.ID, ref msg);
                if (result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("Name", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }

        public ActionResult LoadOrders()
        {
            var model = new CustomerModels();
            model.ListOrders = _facOrder.GetListOrder();
            if (model.ListOrders != null && model.ListOrders.Any())
            {
                int index = 0;
                model.ListOrders.ForEach(o => {
                    o.OffSet = index++;
                });
            }
            return PartialView("_TableChooseOrders", model);
        }

        public ActionResult LoadOrderDetail(string OrderID)
        {
            try
            {
                var model = new CMS_OrderModels();
                model = _facOrder.GetDetailOrder(OrderID);
                return PartialView("_OrderDetail", model);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetOrderDetail", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }            
        }
    }
}