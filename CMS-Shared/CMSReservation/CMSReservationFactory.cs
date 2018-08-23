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
        public bool CreateOrUpdate(CMS_ReservationViewModels model, ref string msg)
        {
            NSLog.Logger.Info("ReservationCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        var e = new CMS_Reservation
                        {
                            ID = Guid.NewGuid().ToString(),
                            CustomerName = model.CustomerName,
                            Mobile = model.Phone,
                            Date = model.BookDay,
                            Remark = model.Description,
                            StoreID = model.StoreID,

                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                        };
                        cxt.CMS_Reservation.Add(e);

                        var reDtl = new CMS_ReservationDetail()
                        {
                            ID = Guid.NewGuid().ToString(),
                            ReservationID = e.ID,
                            ProductID = model.ProductID,
                            ProductName = model.ProductName,
                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                        };
                        cxt.CMS_ReservationDetail.Add(reDtl);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Reservation.Find(model.Id);
                        if (e != null)
                        {
                            e.CustomerName = model.CustomerName;
                            e.Mobile = model.Phone;
                            e.Date = model.BookDay;
                            e.Remark = model.Description;
                            e.ModifiedUser = model.CreatedBy;
                            e.LastModified = DateTime.Now;

                            var reDtl = cxt.CMS_ReservationDetail.Where(o => o.ReservationID == e.ID).FirstOrDefault();
                            if (reDtl != null)
                            {
                                reDtl.ProductID = model.ProductID;
                                reDtl.ProductName = model.ProductName;
                                reDtl.ModifiedUser = model.CreatedBy;
                                reDtl.LastModified = DateTime.Now;
                            }
                        }
                        else
                        {
                            Result = false;
                            msg = "Unable to find Reservation.";
                        }
                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseReservationCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "Vui lòng kiểm tra đường truyền";
                    NSLog.Logger.Error("ErrorReservationCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("ReservationDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Reservation.Find(Id);
                    if (e != null)
                    {
                        e.Status = (byte)Commons.EStatus.Deleted;
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Unable to find Reservation.";
                        result = false;
                    }

                    NSLog.Logger.Info("ResponseReservationDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa thể loại này";
                result = false;
                NSLog.Logger.Error("ErrorReservationDelete", ex);
            }
            return result;
        }

        public CMS_ReservationViewModels GetDetail(string Id)
        {
            NSLog.Logger.Info("CateReservationDetail", Id);
            CMS_ReservationViewModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Reservation.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Join(cxt.CMS_ReservationDetail, r=> r.ID, d=> d.ReservationID, (r,d)=> new { r, d })
                        .Select(o => new CMS_ReservationViewModels
                        {
                            Id = o.r.ID,
                            CustomerName = o.r.CustomerName,
                            Phone = o.r.Mobile,
                            BookDay = o.r.Date,
                            Description = o.r.Remark,
                            ProductID = o.d.ProductID,
                            ProductName = o.d.ProductName,
                        }).FirstOrDefault();

                    result = data;

                    NSLog.Logger.Info("ResponseReservationGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorReservationGetDetail", ex);
            }
            return result;
        }

        public List<CMS_ReservationViewModels> GetList(int productType = 0)
        {
            NSLog.Logger.Info("ReservationGetList");

            List<CMS_ReservationViewModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var query = cxt.CMS_Reservation.Where(o => o.Status != (byte)Commons.EStatus.Deleted);

                    var data = query.Where(o => o.Status != (byte)Commons.EStatus.Deleted).OrderByDescending(o => o.Date)
                        .Join(cxt.CMS_ReservationDetail, r => r.ID, d => d.ReservationID, (r, d) => new { r, d })
                        .Select(o => new CMS_ReservationViewModels
                        {
                            Id = o.r.ID,
                            CustomerName = o.r.CustomerName,
                            Phone = o.r.Mobile,
                            BookDay = o.r.Date,
                            Description = o.r.Remark,
                            ProductID = o.d.ProductID,
                            ProductName = o.d.ProductName,
                        }).ToList();

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseReservationGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorReservationGetList", ex);
            }
            return result;
        }
    }
}
