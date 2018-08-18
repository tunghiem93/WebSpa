using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSImage;
using CMS_DTO.CMSProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSProducts
{
    public class CMSProductFactory
    {
        public bool CreateOrUpdate(CMS_ProductsModels model, ref string msg)
        {
            model.PictureUpload = null;
            NSLog.Logger.Info("ProductCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        var _Id = Guid.NewGuid().ToString();
                        model.ProductCode = model.ProductCode.Trim();
                        model.BarCode = model.BarCode.Trim();
                        var isDupProdctCode = cxt.CMS_Products.Where(o => o.ProductCode.Contains(model.ProductCode) && o.Status == (byte)Commons.EStatus.Actived).Count() > 0;
                        if (isDupProdctCode == false) /* don't duplicate */
                        {
                            var e = new CMS_Products
                            {
                                ID = _Id,
                                TypeCode = model.ProductTypeCode,
                                CategoryID = model.CategoryId,
                                Name = model.ProductName,
                                ProductCode = model.ProductCode,
                                BarCode = model.BarCode,
                                Description = model.Description,
                                PrintOutText = model.PrintOutText,
                                IsActive = model.IsActive,
                                ImageURL = model.ImageURL,
                                Cost = model.ProductPrice,
                                Unit = model.Unit,
                                Measure = model.Measure,
                                Quantity = (decimal)model.Quantity,
                                Limit = model.Limit,
                                ExtraPrice = model.ExtraPrice,
                                IsAllowedDiscount = model.IsAllowedDiscount,
                                IsCheckedStock = model.IsCheckedStock,
                                IsAllowedOpenPrice = model.IsAllowedOpenPrice,
                                IsPrintedOnCheck = model.IsPrintedOnCheck,
                                ExpiredDate = model.ExpiredDate,
                                IsAutoAddToOrder = model.IsAutoAddToOrder,
                                IsComingSoon = model.IsComingSoon,
                                IsShowInReservation = model.IsShowInReservation,
                                IsRecommend = model.IsRecommend,
                                StoreID = model.StoreID,

                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                            };
                            cxt.CMS_Products.Add(e);

                            if (model.ListImages != null && model.ListImages.Any())
                            {
                                var _e = new List<CMS_ImagesLink>();
                                model.ListImages.ForEach(x =>
                                {
                                    _e.Add(new CMS_ImagesLink
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        ImageURL = x.ImageURL,
                                        ProductId = _Id,
                                        Offset = x.OffSet,
                                        CreatedUser = model.CreatedBy,
                                        CreatedDate = DateTime.Now,
                                        ModifiedUser = model.CreatedBy,
                                        LastModified = DateTime.Now
                                    });
                                });
                                cxt.CMS_ImagesLink.AddRange(_e);
                            }

                        }
                        else
                        {
                            Result = false;
                            msg = "Duplicate product Code.";
                        }

                    }
                    else /* updated */
                    {
                        var isDupProdctCode = cxt.CMS_Products.Where(o => o.ProductCode.Contains(model.ProductCode) && o.Status == (byte)Commons.EStatus.Actived && o.ID != model.Id).Count() > 0;
                        if (isDupProdctCode == false) /* don't duplicate */
                        {
                            var proCheck = cxt.CMS_Products.Find(model.Id);
                            if (proCheck != null)
                            {
                                proCheck.TypeCode = model.ProductTypeCode;
                                proCheck.CategoryID = model.CategoryId;
                                proCheck.Name = model.ProductName;
                                proCheck.ProductCode = model.ProductCode;
                                proCheck.BarCode = model.BarCode;
                                proCheck.Description = model.Description;
                                proCheck.PrintOutText = model.PrintOutText;
                                proCheck.IsActive = model.IsActive;
                                proCheck.ImageURL = model.ImageURL;
                                proCheck.Cost = model.ProductPrice;
                                proCheck.Unit = model.Unit;
                                proCheck.Measure = model.Measure;
                                proCheck.Quantity = (decimal)model.Quantity;
                                proCheck.Limit = model.Limit;
                                proCheck.ExtraPrice = model.ExtraPrice;
                                proCheck.IsAllowedDiscount = model.IsAllowedDiscount;
                                proCheck.IsCheckedStock = model.IsCheckedStock;
                                proCheck.IsAllowedOpenPrice = model.IsAllowedOpenPrice;
                                proCheck.IsPrintedOnCheck = model.IsPrintedOnCheck;
                                proCheck.ExpiredDate = model.ExpiredDate;
                                proCheck.IsAutoAddToOrder = model.IsAutoAddToOrder;
                                proCheck.IsComingSoon = model.IsComingSoon;
                                proCheck.IsShowInReservation = model.IsShowInReservation;
                                proCheck.IsRecommend = model.IsRecommend;
                                proCheck.StoreID = model.StoreID;

                                proCheck.ModifiedUser = model.CreatedBy;
                                proCheck.LastModified = DateTime.Now;

                                if (model.ListImages != null && model.ListImages.Any())
                                {
                                    var _edel = cxt.CMS_ImagesLink.Where(x => x.ProductId.Equals(proCheck.ID));
                                    cxt.CMS_ImagesLink.RemoveRange(_edel);

                                    var _e = new List<CMS_ImagesLink>();
                                    model.ListImages.ForEach(x =>
                                    {
                                        _e.Add(new CMS_ImagesLink
                                        {
                                            Id = Guid.NewGuid().ToString(),
                                            ImageURL = x.ImageURL,
                                            ProductId = proCheck.ID,
                                            Offset = x.OffSet,
                                            CreatedUser = model.CreatedBy,
                                            CreatedDate = DateTime.Now,
                                            ModifiedUser = model.CreatedBy,
                                            LastModified = DateTime.Now
                                        });
                                    });
                                    cxt.CMS_ImagesLink.AddRange(_e);
                                }
                            }
                            else
                            {
                                Result = false;
                                msg = "Unable to find products.";
                            }
                        }
                        else
                        {
                            Result = false;
                            msg = "Duplicate product Code.";
                        }

                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseProductCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorProductCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("ProductDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Products.Find(Id);
                    if (e != null)
                    {
                        e.Status = (byte)Commons.EStatus.Deleted;
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Unable to find data to delete.";
                        result = false;
                    }

                    NSLog.Logger.Info("ResponseProductDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorProductDelete", ex);
            }
            return result;
        }

        public CMS_ProductsModels GetDetail(string Id)
        {
            NSLog.Logger.Info("ProductGetDetail", Id);
            CMS_ProductsModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_ProductsModels
                        {
                            Id = o.ID,
                            ProductTypeCode = o.TypeCode,
                            CategoryId = o.CategoryID,
                            ProductName = o.Name,
                            ProductCode = o.ProductCode,
                            BarCode = o.BarCode,
                            Description = o.Description,
                            PrintOutText = o.PrintOutText,
                            IsActive = o.IsActive,
                            ImageURL = o.ImageURL,
                            ProductPrice = o.Cost,
                            Unit = o.Unit ?? 1,
                            Measure = o.Measure,
                            Quantity = (double)o.Quantity,
                            Limit = o.Limit,
                            ExtraPrice = o.ExtraPrice,
                            IsAllowedDiscount = o.IsAllowedDiscount,
                            IsCheckedStock = o.IsCheckedStock,
                            IsAllowedOpenPrice = o.IsAllowedOpenPrice,
                            IsPrintedOnCheck = o.IsPrintedOnCheck,
                            ExpiredDate = o.ExpiredDate,
                            IsAutoAddToOrder = o.IsAutoAddToOrder,
                            IsComingSoon = o.IsComingSoon,
                            IsShowInReservation = o.IsShowInReservation,
                            IsRecommend = o.IsRecommend,
                            StoreID = o.StoreID,
                        }).FirstOrDefault();

                    data.ListImages = cxt.CMS_ImagesLink.Where(o => o.ProductId == Id).OrderBy(o => o.Offset).Select(o => new CMS_ImagesModels()
                    {
                        Id = o.Id,
                        OffSet = o.Offset,
                        ImageName = o.ImageURL,
                        ProductId = o.ProductId,
                        ImageURL = string.IsNullOrEmpty(o.ImageURL) ? "" : Commons._PublicImages + "Products/" + o.ImageURL,
                    }).ToList();

                    /* response data */
                    result = data;

                    NSLog.Logger.Info("ResponseProductGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorProductGetDetail", ex);
            }
            return result;
        }

        public List<CMS_ProductsModels> GetList(int productType = 0)
        {
            NSLog.Logger.Info("ProductGetList");

            List<CMS_ProductsModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var query = cxt.CMS_Products.Where(o => o.Status != (byte)Commons.EStatus.Deleted);
                    if (productType != 0)
                        query = query.Where(o => o.TypeCode == productType);

                    var data = query.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Join(cxt.CMS_Categories, p => p.CategoryID, c => c.ID, (p, c) => new { p, c })
                        .Select(o => new CMS_ProductsModels
                        {
                            Id = o.p.ID,
                            ProductTypeCode = o.p.TypeCode,
                            CategoryId = o.p.CategoryID,
                            CategoryName = o.c.Name,
                            ProductName = o.p.Name,
                            ProductCode = o.p.ProductCode,
                            BarCode = o.p.BarCode,
                            Description = o.p.Description,
                            PrintOutText = o.p.PrintOutText,
                            IsActive = o.p.IsActive,
                            //ImageURL = o.p.ImageURL,
                            ProductPrice = o.p.Cost,
                            Unit = o.p.Unit ?? 1,
                            Measure = o.p.Measure,
                            Quantity = (double)o.p.Quantity,
                            Limit = o.p.Limit,
                            ExtraPrice = o.p.ExtraPrice,
                            IsAllowedDiscount = o.p.IsAllowedDiscount,
                            IsCheckedStock = o.p.IsCheckedStock,
                            IsAllowedOpenPrice = o.p.IsAllowedOpenPrice,
                            IsPrintedOnCheck = o.p.IsPrintedOnCheck,
                            ExpiredDate = o.p.ExpiredDate,
                            IsAutoAddToOrder = o.p.IsAutoAddToOrder,
                            IsComingSoon = o.p.IsComingSoon,
                            IsShowInReservation = o.p.IsShowInReservation,
                            IsRecommend = o.p.IsRecommend,
                            StoreID = o.p.StoreID,
                        }).ToList();

                    var listProductID = data.Select(o => o.Id).ToList();
                    var listImage = cxt.CMS_ImagesLink.Where(o => listProductID.Contains(o.ProductId)).Select(o => new CMS_ImagesModels
                    {
                        Id = o.Id,
                        OffSet = o.Offset,
                        ImageName = o.ImageURL,
                        ProductId = o.ProductId,
                        ImageURL = string.IsNullOrEmpty(o.ImageURL) ? "" : Commons._PublicImages + "Products/" + o.ImageURL,
                    }).ToList();

                    data.ForEach(o =>
                    {
                        o.ImageURL = listImage.Where(i => i.ProductId == o.Id).OrderBy(i => i.OffSet).Select(i => i.ImageURL).FirstOrDefault();
                        o.ImageURL = string.IsNullOrEmpty(o.ImageURL) ? Commons._ImageProductDefault : o.ImageURL;
                    });

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseProductGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorProductGetList", ex);
            }
            return result;
        }


        public List<CMS_ImagesModels> GetListImage()
        {
            NSLog.Logger.Info("ProductGetListImage");
            List<CMS_ImagesModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ImagesLink
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    result = data;
                    NSLog.Logger.Info("ResponseProductGetListImage", result);

                }
            }
            catch (Exception ex) { NSLog.Logger.Error("ErrorProductGetListImage", ex); }
            return result;
        }

        public List<CMS_ImagesModels> GetListImageOfProduct(string ProductId)
        {
            NSLog.Logger.Info("ProductGetListImageOfProduct", ProductId);
            List<CMS_ImagesModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ImagesLink.Where(x => x.ProductId.Equals(ProductId))
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    result = data;
                    NSLog.Logger.Info("ResponseProductGetListImageOfProduct", result);
                }
            }
            catch (Exception ex) { NSLog.Logger.Error("ErrorProductGetListImageOfProduct", ex); }
            return result;
        }

        public bool DeleteImage(string Id, ref string msg)
        {
            NSLog.Logger.Info("ProductDeleteImage", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_ImagesLink.Find(Id);
                    if (e != null)
                    {
                        msg = e.ImageURL;
                        cxt.CMS_ImagesLink.Remove(e);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                    }

                    NSLog.Logger.Info("ResponseProductDeleteImage", new { result, msg });

                }
            }
            catch (Exception ex)
            {
                result = false;
                msg = "Vui lòng kiểm tra đường truyền";
                NSLog.Logger.Error("ErrorProductDeleteImage", ex);
            }
            return result;
        }
    }
}
