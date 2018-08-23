using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSReservation
{
    public class CMSReservationFactory
    {
        //public bool CreateOrUpdate(CMS_ReservationViewModels model, ref string Id, ref string msg)
        //{
        //    NSLog.Logger.Info("ReservationCreateOrUpdate", model);
        //    var Result = true;
        //    using (var cxt = new CMS_Context())
        //    {
        //        try
        //        {
        //            if (string.IsNullOrEmpty(model.Id)) /* insert */
        //            {
        //                Id = Guid.NewGuid().ToString();
        //                var e = new CMS_Reservation
        //                {
        //                    ID = Id,
        //                    Name = model.CategoryName,
        //                    TotalProducts = 0,
        //                    Description = model.Description,
        //                    Status = (byte)Commons.EStatus.Actived,
        //                    CreatedDate = DateTime.Now,
        //                    CreatedUser = model.CreatedBy,
        //                    ModifiedUser = model.CreatedBy,
        //                    LastModified = DateTime.Now,
        //                    IsActive = model.IsActive,
        //                };
        //                cxt.CMS_Reservation.Add(e);
        //            }
        //            else /* updated */
        //            {
        //                var e = cxt.CMS_Reservation.Find(model.Id);
        //                if (e != null)
        //                {
        //                    e.Name = model.CategoryName;
        //                    e.Description = model.Description;
        //                    e.ModifiedUser = model.CreatedBy;
        //                    e.LastModified = DateTime.Now;
        //                }
        //                else
        //                {
        //                    Result = false;
        //                    msg = "Unable to find Reservation.";
        //                }
        //            }

        //            cxt.SaveChanges();
        //            NSLog.Logger.Info("ResponseReservationCreateOrUpdate", new { Result, msg });

        //        }
        //        catch (Exception ex)
        //        {
        //            Result = false;
        //            msg = "Vui lòng kiểm tra đường truyền";
        //            NSLog.Logger.Error("ErrorReservationCreateOrUpdate", ex);
        //        }
        //    }
        //    return Result;
        //}

        //public bool Delete(string Id, ref string msg)
        //{
        //    NSLog.Logger.Info("ReservationDelete", Id);

        //    var result = true;
        //    try
        //    {
        //        using (var cxt = new CMS_Context())
        //        {
        //            var e = cxt.CMS_Reservation.Find(Id);
        //            if (e != null)
        //            {
        //                var isExistProduct = cxt.CMS_Products.Where(o => o.CategoryID == Id && o.Status != (byte)Commons.EStatus.Deleted).Count() > 0;
        //                if (!isExistProduct)
        //                {
        //                    e.Status = (byte)Commons.EStatus.Deleted;
        //                    cxt.SaveChanges();
        //                }
        //                else
        //                {
        //                    result = false;
        //                    msg = "Have product in this cate, unable to delete Reservation.";
        //                }
        //            }
        //            else
        //            {
        //                msg = "Unable to find Reservation.";
        //                result = false;
        //            }

        //            NSLog.Logger.Info("ResponseReservationDelete", new { result, msg });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Không thể xóa lịch đặt chỗ này";
        //        result = false;
        //        NSLog.Logger.Error("ErrorReservationDelete", ex);
        //    }
        //    return result;
        //}

        //public CMS_ReservationViewModels GetDetail(string Id)
        //{
        //    NSLog.Logger.Info("ReservationGetDetail", Id);
        //    CMS_ReservationViewModels result = null;

        //    try
        //    {
        //        using (var cxt = new CMS_Context())
        //        {
        //            var data = cxt.CMS_Categories.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
        //                .Select(x => new CMS_ReservationViewModels
        //                {
        //                    Id = x.ID,
        //                    ParentId = x.ParentID,
        //                    CategoryName = x.Name,
        //                    StoreID = x.StoreID,
        //                    Description = x.Description,
        //                    ProductTypeCode = x.ProductTypeCode,
        //                    IsActive = x.IsActive,
        //                }).FirstOrDefault();

        //            result = data;

        //            NSLog.Logger.Info("ResponseCateGetDetail", result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NSLog.Logger.Error("ErrorCateGetDetail", ex);
        //    }
        //    return result;
        //}

        //public List<CMS_ReservationViewModels> GetList(int productType = 0)
        //{
        //    NSLog.Logger.Info("CateGetList");

        //    List<CMS_ReservationViewModels> result = null;
        //    try
        //    {
        //        using (var cxt = new CMS_Context())
        //        {
        //            var query = cxt.CMS_Reservation.Where(o => o.Status != (byte)Commons.EStatus.Deleted);
        //            var data = query.Where(o => o.Status != (byte)Commons.EStatus.Deleted).OrderBy(o => o.Sequence)
        //                .Select(x => new CMS_ReservationViewModels
        //                {
        //                    Id = x.ID,
        //                    CategoryName = x.Name,
        //                    StoreID = x.StoreID,
        //                    Description = x.Description,
        //                    IsActive = x.IsActive,
        //                }).ToList();                    
        //            result = data;
        //            NSLog.Logger.Info("ResponseReservationGetList", result);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NSLog.Logger.Error("ErrorReservationGetList", ex);
        //    }
        //    return result;
        //}        
    }
}
