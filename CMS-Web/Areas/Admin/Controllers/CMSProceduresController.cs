using CMS_Common;
using CMS_DTO.CMSProcedures;
using CMS_Shared.CMSProcedures;
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
    public class CMSProceduresController : HQController
    {
        private CMSProcedureFactory _factory;
        public CMSProceduresController()
        {
            _factory = new CMSProcedureFactory();
            //ViewBag.Category = GetListCategorySelectItem();
        }
        // GET: Admin/CMSProcedures
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList();
            model.ForEach(x =>
            {
                x.sStatus = x.IsActive ? "Kích hoạt" : "Chưa kích hoạt";
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_ProceduresModels model = new CMS_ProceduresModels();
            return PartialView("_Create", model);
        }

        public CMS_ProceduresModels GetDetail(string Id)
        {
            return _factory.GetDetail(Id);
        }

        [HttpPost]
        public ActionResult Create(CMS_ProceduresModels model)
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
                    model.ImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }
                var msg = "";
                model.CreatedBy = CurrentUser.UserId;
                model.UpdatedBy = CurrentUser.UserId;
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.ImageUrl) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Procedures/" + model.ImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageUrl, ref photoByte, Commons.WidthProcedure, Commons.WidthProcedure, Commons.HeightProcedure);
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ProcedureCode", msg);
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
        public ActionResult Edit(CMS_ProceduresModels model)
        {
            var temp = model.ImageUrl;
            try
            {
                byte[] photoByte = null;
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                var bkImageUrl = model.ImageUrl;
                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    model.ImageUrl = model.ImageUrl.Replace(Commons._PublicImages, "").Replace("Procedures/", "").Replace(Commons.Image500_500, "");
                    temp = model.ImageUrl;
                }

                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }
                var msg = "";
                model.CreatedBy = CurrentUser.UserId;
                model.UpdatedBy = CurrentUser.UserId;
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.ImageUrl) && model.PictureByte != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Uploads/Procedures/" + temp)))
                        {
                            ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Procedures/" + temp));
                        }

                        var path = Server.MapPath("~/Uploads/Procedures/" + model.ImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageUrl, ref photoByte, Commons.WidthProcedure, Commons.WidthProcedure, Commons.HeightProcedure);
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ProductCode", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                model.ImageUrl = bkImageUrl;
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
            if (!string.IsNullOrEmpty(model.ImageUrl))
                model.ImageUrl = Commons.HostImage + "Procedures/" + model.ImageUrl;
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CMS_ProceduresModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var orgImageUrl = "";
                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    model.ImageUrl = model.ImageUrl.Replace(Commons._PublicImages, "").Replace("Procedures/", "").Replace(Commons.Image200_100, "");
                    orgImageUrl = model.ImageUrl;
                }

                var msg = "";
                var result = _factory.Delete(model.Id, ref msg);
                if (result)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Uploads/Procedures/" + orgImageUrl)))
                    {
                        ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Procedures/" + orgImageUrl));
                    }

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ProcedureName", msg);
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