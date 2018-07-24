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
            NSLog.Logger.Info("ProductCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        var Id = Guid.NewGuid().ToString();
                        var e = new CMS_Products
                        {
                            ID = Id,
                            TypeCode = model.TypeCode,
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
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Products.Find(model.Id);
                        if (e != null)
                        {
                            e.TypeCode = model.TypeCode;
                            e.CategoryID = model.CategoryId;
                            e.Name = model.ProductName;
                            e.ProductCode = model.ProductCode;
                            e.BarCode = model.BarCode;
                            e.Description = model.Description;
                            e.PrintOutText = model.PrintOutText;
                            e.IsActive = model.IsActive;
                            e.ImageURL = model.ImageURL;
                            e.Cost = model.ProductPrice;
                            e.Unit = model.Unit;
                            e.Measure = model.Measure;
                            e.Quantity = (decimal)model.Quantity;
                            e.Limit = model.Limit;
                            e.ExtraPrice = model.ExtraPrice;
                            e.IsAllowedDiscount = model.IsAllowedDiscount;
                            e.IsCheckedStock = model.IsCheckedStock;
                            e.IsAllowedOpenPrice = model.IsAllowedOpenPrice;
                            e.IsPrintedOnCheck = model.IsPrintedOnCheck;
                            e.ExpiredDate = model.ExpiredDate;
                            e.IsAutoAddToOrder = model.IsAutoAddToOrder;
                            e.IsComingSoon = model.IsComingSoon;
                            e.IsShowInReservation = model.IsShowInReservation;
                            e.IsRecommend = model.IsRecommend;
                            e.StoreID = model.StoreID;

                            e.ModifiedUser = model.CreatedBy;
                            e.LastModified = DateTime.Now;
                        }
                        else
                        {
                            Result = false;
                            msg = "Unable to find products.";
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
                            TypeCode = o.TypeCode,
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

        public List<CMS_ProductsModels> GetList()
        {
            NSLog.Logger.Info("ProductGetList");

            List<CMS_ProductsModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_ProductsModels
                        {
                            Id = o.ID,
                            TypeCode = o.TypeCode,
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
                        }).ToList();

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
