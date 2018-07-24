using CMS_DTO.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_DataModel.Models;

namespace CMS_Shared.CMSDiscount
{
    public class CMSDiscountFactory
    {
        public bool CreateOrUpdate(CMSDiscountModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("DiscountCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        var e = new CMS_Discount
                        {
                            ID = Id,
                            StoreID = model.StoreID,
                            Name = model.Name,
                            Description = model.Description,
                            ImageUrl = model.ImageURL,
                            IsAllowOpenValue = model.IsAllowOpenValue,
                            IsApplyTotalBill = model.IsApplyTotalBill,
                            IsActive = model.IsActive,
                            ValueType = model.ValueType,
                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                        };
                        cxt.CMS_Discount.Add(e);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Discount.Find(model.Id);
                        if (e != null)
                        {
                            e.Name = model.Name;
                            e.Description = model.Description;
                            e.ImageUrl = model.ImageURL;
                            e.IsAllowOpenValue = model.IsAllowOpenValue;
                            e.IsApplyTotalBill = model.IsApplyTotalBill;
                            e.IsActive = model.IsActive;
                            e.ValueType = model.ValueType;
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

        public CMSDiscountModels GetDetail(string Id)
        {
            NSLog.Logger.Info("DiscountGetDetail", Id);
            CMSDiscountModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Discount.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMSDiscountModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            Description = o.Description,
                            ImageUrl = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + o.ImageUrl,
                            IsAllowOpenValue = o.IsAllowOpenValue?? false,
                            IsApplyTotalBill = o.IsApplyTotalBill,
                            IsActive = o.IsActive??false,
                            ValueType = o.ValueType??(byte)Commons.EValueType.Percent,
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

        public List<CMSDiscountModels> GetList()
        {
            NSLog.Logger.Info("DiscountGetList");

            List<CMSDiscountModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Discount.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMSDiscountModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            Description = o.Description,
                            ImageUrl = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + o.ImageUrl,
                            IsAllowOpenValue = o.IsAllowOpenValue ?? false,
                            IsApplyTotalBill = o.IsApplyTotalBill,
                            IsActive = o.IsActive ?? false,
                            ValueType = o.ValueType ?? (byte)Commons.EValueType.Percent,
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
