using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSDiscount;

namespace CMS_Shared.CMSDiscount
{
    public class CMSDiscountFactory
    {
        public bool CreateOrUpdate(CMS_DiscountModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("DiscountCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    model.DiscountCode = model.DiscountCode.Trim();
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        var checkDup = cxt.CMS_Discount.Where(o => o.DiscountCode.Trim() == model.DiscountCode.Trim()).FirstOrDefault();
                        if (checkDup == null)
                        {
                            var e = new CMS_Discount
                            {
                                ID = Id,
                                StoreID = model.StoreID,
                                Name = model.Name,
                                DiscountCode = model.DiscountCode,
                                Description = model.Description,
                                ImageUrl = model.ImageURL,
                                IsAllowOpenValue = model.IsAllowOpenValue,
                                IsApplyTotalBill = model.IsApplyTotalBill,
                                IsActive = model.IsActive,
                                Value = model.Value,
                                ValueType = model.IsPercent ? (byte)Commons.EValueType.Percent : (byte)Commons.EValueType.Currency,
                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                            };
                            cxt.CMS_Discount.Add(e);
                        }
                        else
                        {
                            msg = "Duplicate Code";
                            Result = false;
                        }
                        
                    }
                    else /* updated */
                    {
                        var checkDupCode = cxt.CMS_Discount.Where(o => o.DiscountCode.Trim() == model.DiscountCode.Trim() && o.ID != model.Id).FirstOrDefault();
                        if (checkDupCode == null)
                        {
                            var e = cxt.CMS_Discount.Find(model.Id);
                            if (e != null)
                            {
                                e.Name = model.Name;
                                e.DiscountCode = model.DiscountCode;
                                e.Description = model.Description;
                                e.ImageUrl = model.ImageURL;
                                e.IsAllowOpenValue = model.IsAllowOpenValue;
                                e.IsApplyTotalBill = model.IsApplyTotalBill;
                                e.IsActive = model.IsActive;
                                e.Value = model.Value;
                                e.ValueType = model.IsPercent ? (byte)Commons.EValueType.Percent : (byte)Commons.EValueType.Currency;
                                e.Status = (byte)Commons.EStatus.Actived;
                                e.ModifiedUser = model.CreatedBy;
                                e.LastModified = DateTime.Now;
                            }
                            else
                            {
                                Result = false;
                                msg = "Unable to find Discount.";
                            }
                        }
                        else
                        {
                            msg = "Duplicate Code";
                            Result = false;
                        }
                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseDiscountCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorDiscountCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("DiscountDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Discount.Find(Id);
                    if (e != null)
                    {
                        e.Status = (byte)Commons.EStatus.Deleted;
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Unable to find discount.";
                        result = false;
                    }

                    NSLog.Logger.Info("ResponseDiscountDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorDiscountDelete", ex);
            }
            return result;
        }

        public CMS_DiscountModels GetDetail(string Id)
        {
            NSLog.Logger.Info("DiscountGetDetail", Id);
            CMS_DiscountModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Discount.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_DiscountModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            DiscountCode = o.DiscountCode,
                            Description = o.Description,
                            ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + "Discounts/" + o.ImageUrl,
                            IsAllowOpenValue = o.IsAllowOpenValue ?? false,
                            IsApplyTotalBill = o.IsApplyTotalBill,
                            IsActive = o.IsActive ?? false,
                            IsPercent = (o.ValueType ?? (byte)Commons.EValueType.Percent) == (byte)Commons.EValueType.Percent,
                            Value = o.Value ?? 0,
                        }).FirstOrDefault();

                    result = data;

                    NSLog.Logger.Info("ResponseDiscountGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorDiscountGetDetail", ex);
            }
            return result;
        }

        public List<CMS_DiscountModels> GetList(bool isActive = true)
        {
            NSLog.Logger.Info("DiscountGetList");

            List<CMS_DiscountModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var query = cxt.CMS_Discount.Where(o => o.Status != (byte)Commons.EStatus.Deleted);
                    if (isActive == true)
                        query = query.Where(o => o.IsActive == true);

                    var data = query.Select(o => new CMS_DiscountModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            DiscountCode = o.DiscountCode,
                            Description = o.Description,
                            ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + "Discounts/" + o.ImageUrl,
                            IsAllowOpenValue = o.IsAllowOpenValue ?? false,
                            IsApplyTotalBill = o.IsApplyTotalBill,
                            IsActive = o.IsActive ?? false,
                            Value = o.Value ?? 0,
                            IsPercent = (o.ValueType ?? (byte)Commons.EValueType.Percent) == (byte)Commons.EValueType.Percent,
                        }).ToList();

                    

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseDiscountGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorDiscountGetList", ex);
            }
            return result;
        }
    }
}
