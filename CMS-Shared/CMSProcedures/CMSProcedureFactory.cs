using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSProcedures
{
    public class CMSProcedureFactory
    {
        public bool CreateOrUpdate(CMS_ProceduresModels model, ref string msg)
        {
            model.PictureUpload = null;
            NSLog.Logger.Info("ProcedureCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        var _Id = Guid.NewGuid().ToString();
                        model.ProductCode = model.ProductCode.Trim();
                        //model.BarCode = model.BarCode.Trim();
                        var isDupProdctCode = cxt.CMS_Products.Where(o => o.ProductCode.Contains(model.ProductCode) && o.Status == (byte)Commons.EStatus.Actived).Count() > 0;
                        if (isDupProdctCode == false) /* don't duplicate */
                        {
                            var e = new CMS_Products
                            {
                                ID = _Id,
                                TypeCode = model.ProductTypeCode,
                                CategoryID = model.CategoryId,
                                Name = model.ProceduresName,
                                ProductCode = model.ProductCode,
                                //BarCode = model.BarCode,
                                Description = model.Description,
                                ShortDescription = model.ShortDescription,
                                //PrintOutText = model.PrintOutText,
                                IsActive = model.IsActive,
                                ImageURL = model.ImageUrl,
                                Cost = model.Price,
                                //Unit = model.Unit,
                                Measure = model.Measure,
                                //Quantity = model.Quantity,
                                //Limit = model.Limit,
                                ExtraPrice = model.ExtraPrice,
                                //IsAllowedDiscount = model.IsAllowedDiscount,
                                //IsCheckedStock = model.IsCheckedStock,
                                //IsAllowedOpenPrice = model.IsAllowedOpenPrice,
                                //IsPrintedOnCheck = model.IsPrintedOnCheck,
                                ExpiredDate = model.ExpiredDate,
                                //IsAutoAddToOrder = model.IsAutoAddToOrder,
                                //IsComingSoon = model.IsComingSoon,
                                //IsShowInReservation = model.IsShowInReservation,
                                //IsRecommend = model.IsRecommend,
                                //StoreID = model.StoreID,
                                Process = model.Process,
                                Preparation = model.Preparation,
                                Effect = model.Effect,
                                SpaTreatment = model.SpaTreatment,
                                Duration = model.Duration,

                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                            };
                            cxt.CMS_Products.Add(e);
                        }
                        else
                        {
                            Result = false;
                            msg = "Duplicate Procedure/Product Code.";
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
                                proCheck.Name = model.ProceduresName;
                                proCheck.ProductCode = model.ProductCode;
                                proCheck.BarCode = model.BarCode;
                                proCheck.Description = model.Description;
                                proCheck.ShortDescription = model.ShortDescription;
                                proCheck.PrintOutText = model.PrintOutText;
                                proCheck.IsActive = model.IsActive;
                                proCheck.ImageURL = model.ImageUrl;
                                proCheck.Cost = (double) model.Price;
                                //proCheck.Unit = model.Unit;
                                //proCheck.Measure = model.Measure;
                                proCheck.Quantity = (decimal)model.Quantity;
                                proCheck.Limit = model.Limit;
                                proCheck.ExtraPrice = model.ExtraPrice;
                                //proCheck.IsAllowedDiscount = model.IsAllowedDiscount;
                                //proCheck.IsCheckedStock = model.IsCheckedStock;
                                //proCheck.IsAllowedOpenPrice = model.IsAllowedOpenPrice;
                                //roCheck.IsPrintedOnCheck = model.IsPrintedOnCheck;
                                proCheck.ExpiredDate = model.ExpiredDate;
                                proCheck.IsAutoAddToOrder = model.IsAutoAddToOrder;
                                proCheck.IsComingSoon = model.IsComingSoon;
                                proCheck.IsShowInReservation = model.IsShowInReservation;
                                proCheck.IsRecommend = model.IsRecommend;
                                proCheck.StoreID = model.StoreID;
                                proCheck.Process = model.Process;
                                proCheck.Preparation = model.Preparation;
                                proCheck.Effect = model.Effect;
                                proCheck.SpaTreatment = model.SpaTreatment;
                                proCheck.Duration = model.Duration;

                                proCheck.ModifiedUser = model.UpdatedBy;
                                proCheck.LastModified = DateTime.Now;

                                cxt.SaveChanges();
                            }
                            else
                            {
                                Result = false;
                                msg = "Unable to find Procedure/products.";
                            }
                        }
                        else
                        {
                            Result = false;
                            msg = "Duplicate Procedure/product Code.";
                        }

                    }

                    NSLog.Logger.Info("ResponseProcedureCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorProcedureCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("ProcedureDelete", Id);

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

                    NSLog.Logger.Info("ResponseProcedureDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorProcedureDelete", ex);
            }
            return result;
        }

        public CMS_ProceduresModels GetDetail(string Id)
        {
            NSLog.Logger.Info("ProcedureGetDetail", Id);
            CMS_ProceduresModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_ProceduresModels
                        {
                            Id = o.ID,
                            ProductTypeCode = o.TypeCode,
                            CategoryId = o.CategoryID,
                            ProceduresName = o.Name,
                            ProductCode = o.ProductCode,
                            BarCode = o.BarCode,
                            Description = o.Description,
                            ShortDescription = o.ShortDescription,
                            PrintOutText = o.PrintOutText,
                            IsActive = o.IsActive,
                            ImageUrl = string.IsNullOrEmpty(o.ImageURL) ? "" : Commons._PublicImages + "Procedures/" + o.ImageURL,
                            Price = o.Cost,
                            //Unit = o.Unit ?? 1,
                            //Measure = o.Measure,
                            Quantity = o.Quantity ?? 0,
                            Limit = o.Limit,
                            ExtraPrice = o.ExtraPrice,
                            //IsAllowedDiscount = o.IsAllowedDiscount,
                            //IsCheckedStock = o.IsCheckedStock,
                            //IsAllowedOpenPrice = o.IsAllowedOpenPrice,
                            //IsPrintedOnCheck = o.IsPrintedOnCheck,
                            ExpiredDate = o.ExpiredDate,
                            IsAutoAddToOrder = o.IsAutoAddToOrder,
                            IsComingSoon = o.IsComingSoon,
                            IsShowInReservation = o.IsShowInReservation,
                            IsRecommend = o.IsRecommend,
                            StoreID = o.StoreID,
                            Process = o.Process,
                            Preparation = o.Preparation,
                            SpaTreatment = o.SpaTreatment,
                            Effect = o.Effect,
                            Duration = o.Duration,
                        }).FirstOrDefault();
                    

                    /* response data */
                    result = data;

                    NSLog.Logger.Info("ResponseProcedureGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorProcedureGetDetail", ex);
            }
            return result;
        }

        public List<CMS_ProceduresModels> GetList()
        {
            NSLog.Logger.Info("ProcedureGetList");

            List<CMS_ProceduresModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Where(o => o.Status != (byte)Commons.EStatus.Deleted && o.TypeCode == (byte)Commons.EProductType.Procudure)
                        .Select(o => new CMS_ProceduresModels
                        {
                            Id = o.ID,
                            ProductTypeCode = o.TypeCode,
                            //CategoryId = o.p.CategoryID,
                            //CategoryName = o.c.Name,
                            ProceduresName = o.Name,
                            ProductCode = o.ProductCode,
                            BarCode = o.BarCode,
                            Description = o.Description,
                            ShortDescription = o.ShortDescription,
                            PrintOutText = o.PrintOutText,
                            IsActive = o.IsActive,
                            ImageUrl = string.IsNullOrEmpty(o.ImageURL) ? "" : Commons._PublicImages + "Procedures/" + o.ImageURL,
                            Price = o.Cost,
                            //Unit = o.p.Unit ?? 1,
                            //Measure = o.p.Measure,
                            Quantity = o.Quantity ?? 0,
                            Limit = o.Limit,
                            ExtraPrice = o.ExtraPrice,
                            //IsAllowedDiscount = o.p.IsAllowedDiscount,
                            //IsCheckedStock = o.p.IsCheckedStock,
                            //IsAllowedOpenPrice = o.p.IsAllowedOpenPrice,
                            ///IsPrintedOnCheck = o.p.IsPrintedOnCheck,
                            ExpiredDate = o.ExpiredDate,
                            IsAutoAddToOrder = o.IsAutoAddToOrder,
                            IsComingSoon = o.IsComingSoon,
                            IsShowInReservation = o.IsShowInReservation,
                            IsRecommend = o.IsRecommend,
                            StoreID = o.StoreID,
                            Process = o.Process,
                            Preparation = o.Preparation,
                            SpaTreatment = o.SpaTreatment,
                            Effect = o.Effect,
                            Duration = o.Duration,
                        }).ToList();
                   
                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseProcedureGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorProcedureGetList", ex);
            }
            return result;
        }
        
    }
}
