using CMS_Common;
using CMS_DTO.CMSImage;
using CMS_DTO.CMSProduct;
using CMS_Shared.CMSProducts;
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
    public class CMSProductsController : HQController
    {
        private CMSProductFactory _factory;
        private int ProductType = (int)Commons.EProductType.Product;

        public CMSProductsController()
        {
            _factory = new CMSProductFactory();
            ViewBag.Category = GetListCategorySelectItem(ProductType);
        }
        // GET: Admin/CMSCategories
        public ActionResult Index()
        {
            if (HttpContext.Session != null && HttpContext.Session["Upload"] != null)
                HttpContext.Session.Remove("Upload");
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList(ProductType);
            model.ForEach(x =>
            {
                x.sStatus = x.IsActive ? "Kích hoạt" : "Chưa kích hoạt";
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            if (HttpContext.Session != null && HttpContext.Session["Upload"] != null)
                HttpContext.Session.Remove("Upload");
            CMS_ProductsModels model = new CMS_ProductsModels();
            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(CMS_ProductsModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                byte[] photoByte = null;
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var data = new List<CMS_ImagesModels>();
                if (model.PictureUpload.Length > 0 && model.PictureUpload.Any() && model.PictureUpload[0] != null)
                {
                    var _FileUpload = Session["Upload"] as SessionUpload;
                    var tempUpload = _FileUpload.ListProduct;
                    foreach (HttpPostedFileBase File in tempUpload)
                    {
                        if (model.ListImages != null && model.ListImages.Any())
                        {
                            var _temp = model.ListImages.Where(x => x.ImageName.Equals(File.FileName) && !x.IsDeleted).FirstOrDefault();
                            if (_temp != null)
                            {
                                if (File != null && File.ContentLength > 0)
                                {
                                    Byte[] imgByte = new Byte[File.ContentLength];
                                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                                    _temp.PictureByte = imgByte;
                                    _temp.TempImageURL = _temp.ImageURL;
                                    _temp.ImageURL = Guid.NewGuid() + Path.GetExtension(File.FileName);
                                    _temp.PictureUpload = null;
                                    lstImgByte.Add(_temp.OffSet, imgByte);
                                }
                                data.Add(new CMS_ImagesModels
                                {
                                    ImageURL = _temp.ImageURL,
                                    TempImageURL = _temp.TempImageURL,
                                    PictureByte = _temp.PictureByte,
                                    OffSet = _temp.OffSet
                                });
                            }
                        }
                    }
                }

                if (model.ListImages != null && model.ListImages.Any())
                {
                    model.ListImages.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.ImageURL) && x.PictureByte == null)
                        {
                            x.ImageURL = x.ImageURL.Replace(Commons._PublicImages +"Products/", "").Replace(Commons.Image200_100, "");
                            data.Add(new CMS_ImagesModels
                            {
                                ImageURL = x.ImageURL,
                                PictureByte = x.PictureByte,
                                OffSet = x.OffSet,
                            });
                        }
                    });
                }

                var msg = "";
                model.ListImages = data;
                model.CreatedBy = CurrentUser.UserId;
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Uploads/Products/" + item.TempImageURL)))
                            {
                                ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Products/" + item.TempImageURL));
                            }

                            var path = Server.MapPath("~/Uploads/Products/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte,400,Commons.WidthProduct,Commons.HeightProduct);
                            model.PictureByte = photoByte;
                        }
                    }
                    return RedirectToAction("Index");
                }
                    
                ModelState.AddModelError("ProductCode", msg);
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
            if (HttpContext.Session != null && HttpContext.Session["Upload"] != null)
                HttpContext.Session.Remove("Upload");
            var model = _factory.GetDetail(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CMS_ProductsModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                byte[] photoByte = null;
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var data = new List<CMS_ImagesModels>();
                if (model.PictureUpload.Length > 0 && model.PictureUpload.Any() && model.PictureUpload[0] != null)
                {
                    var _FileUpload = Session["Upload"] as SessionUpload;
                    var tempUpload = _FileUpload.ListProduct;
                    foreach (HttpPostedFileBase File in tempUpload)
                    {
                        if (model.ListImages != null && model.ListImages.Any())
                        {
                            var _temp = model.ListImages.Where(x => x.ImageName.Equals(File.FileName) && !x.IsDeleted).FirstOrDefault();
                            if (_temp != null)
                            {
                                if (File != null && File.ContentLength > 0)
                                {
                                    Byte[] imgByte = new Byte[File.ContentLength];
                                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                                    _temp.PictureByte = imgByte;
                                    _temp.TempImageURL = _temp.ImageURL;
                                    _temp.ImageURL = Guid.NewGuid() + Path.GetExtension(File.FileName);
                                    _temp.PictureUpload = null;
                                    lstImgByte.Add(_temp.OffSet, imgByte);
                                }
                                data.Add(new CMS_ImagesModels
                                {
                                    ImageURL = _temp.ImageURL,
                                    TempImageURL = _temp.TempImageURL,
                                    PictureByte = _temp.PictureByte,
                                    OffSet = _temp.OffSet
                                });
                            }
                        }
                    }
                }

                if (model.ListImages != null && model.ListImages.Any())
                {
                    model.ListImages.ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.ImageURL) && x.PictureByte == null)
                        {
                            x.ImageURL = x.ImageURL.Replace(Commons._PublicImages + "Products/", "").Replace(Commons.Image200_100, "");
                            data.Add(new CMS_ImagesModels
                            {
                                ImageURL = x.ImageURL,
                                PictureByte = x.PictureByte,
                                OffSet = x.OffSet,
                            });
                        }
                    });
                }
                var msg = "";
                model.ListImages = data;
                model.CreatedBy = CurrentUser.UserId;

                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/Uploads/Products/" + item.TempImageURL)))
                            {
                                ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Products/" + item.TempImageURL));
                            }

                            var path = Server.MapPath("~/Uploads/Products/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte, 400, Commons.WidthProduct, Commons.HeightProduct);
                            model.PictureByte = photoByte;
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ProductCode", msg);
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
            var model = _factory.GetDetail(Id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = _factory.GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CMS_ProductsModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var msg = "";
                var _LstImageOfProduct = _factory.GetListImageOfProduct(model.Id);
                var result = _factory.Delete(model.Id, ref msg);
                if (result)
                {
                    if(_LstImageOfProduct != null && _LstImageOfProduct.Any())
                    {
                        foreach(var item in _LstImageOfProduct)
                        {
                            // delete image for folder
                            if (System.IO.File.Exists(Server.MapPath("~/Uploads/Products/" + item.ImageURL)))
                            {
                                ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Products/" + item.ImageURL));
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                    
                ModelState.AddModelError("ProductCode", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }

        [HttpPost]
        public PartialViewResult AddImageItem()
        {
            List<CMS_ImagesModels> model = new List<CMS_ImagesModels>();
            var _OffSet = Convert.ToInt32(Request["OffSet"]);
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (var i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    model.Add(new CMS_ImagesModels
                    {
                        OffSet = _OffSet,
                        IsDeleted = false,
                        PictureUpload = file,
                    });
                    _OffSet = _OffSet + 1;

                    if (Session["Upload"] == null)
                    {
                        SessionUpload _sUpload = new SessionUpload();
                        _sUpload.ListProduct.Add(file);
                        Session.Add("Upload", _sUpload);
                    }
                    else
                    {
                        var _Upload = Session["Upload"] as SessionUpload;
                        _Upload.ListProduct.Add(file);
                        Session.Add("Upload", _Upload);
                    }
                }

            }
            return PartialView("_ListItem", model);
        }

        [HttpPost]
        public ActionResult DeleteImage(string Id,string ProductId)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteImage(Id,ref msg);
                if (!result)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new HttpStatusCodeResult(400, "Have an error when you delete a Payment Method");
                }
                // delete image for folder
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/Products/" + msg)))
                {
                    ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Products/" + msg));
                }
                var model = new CMS_ProductsModels();
                model.ListImages = _factory.GetListImageOfProduct(ProductId);
                var _OffSet = 0;
                if (model.ListImages != null && model.ListImages.Any())
                {
                    model.ListImages.ForEach(x =>
                    {
                        x.OffSet = _OffSet;
                        _OffSet = _OffSet + 1;
                        x.ImageName = " ";
                        x.ImageURL = Commons.HostImage + "Products/" + x.ImageURL;
                    });
                }
                return PartialView("_ListItem", model.ListImages);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}