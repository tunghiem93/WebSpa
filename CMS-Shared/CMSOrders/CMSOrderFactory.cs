using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSDiscount;
using CMS_DTO.CMSOrder;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS_Shared.CMSOrders
{
    public class CMSOrderFactory
    {
        private static Semaphore m_Semaphore = new Semaphore(1, 1);

        public bool CreateOrder(CMS_CheckOutModels model)
        {
            NSLog.Logger.Info("CreateOrder_Request:", model);
            using (var db = new CMS_Context())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    m_Semaphore.WaitOne();
                    try
                    {
                        if(string.IsNullOrEmpty(model.Customer.Id) && !string.IsNullOrEmpty(model.Customer.LastName))
                        {
                            // create new customer 
                            model.Customer.Id = Guid.NewGuid().ToString();
                            var eCus = new CMS_Customer
                            {
                                ID = model.Customer.Id,
                                FirstName = string.IsNullOrEmpty(model.Customer.FirstName) ? "Anonymous" : model.Customer.FirstName,
                                LastName = string.IsNullOrEmpty(model.Customer.LastName) ? "" : model.Customer.LastName,
                                Email = string.IsNullOrEmpty(model.Customer.Email) ? "Anonymous@gmail.com" : model.Customer.Email,
                                Phone = model.Customer.Phone,
                                CreatedDate = DateTime.Now,
                                LastModified = DateTime.Now,
                                CustomerType = (int)CMS_Common.Commons.ECustomerType.Anonymous,
                                HomeCountry = string.IsNullOrEmpty(model.Customer.Country) ? "Việt Nam" : model.Customer.Country,
                                OfficeZipCode = model.Customer.PostCode,
                                Status = (byte)CMS_Common.Commons.EStatus.Actived,
                                Anniversary = Commons.MinDate,
                                ValidTo = Commons.MinDate,
                                HomeStreet = string.IsNullOrEmpty(model.Customer.Address) ? Commons.AddressCompany : model.Customer.Address,
                                OfficeStreet = string.IsNullOrEmpty(model.Customer.Address) ? Commons.AddressCompany : model.Customer.Address,
                            };
                            db.CMS_Customer.Add(eCus);
                        }
                        // create order
                        var _OrderId = Guid.NewGuid().ToString();
                        var _OrderNo = CommonHelper.GenerateOrderNo(model.StoreID, (byte)Commons.EStatus.Actived);
                        var eOrder = new CMS_Order
                        {
                            ID = _OrderId,
                            StoreID = model.StoreID,
                            //OrderNo = CommonHelper.RandomNumberOrder(),
                            OrderNo = _OrderNo,
                            CustomerID = model.Customer.Id,
                            TotalBill = model.TotalPrice,
                            SubTotal = model.SubTotalPrice,
                            TotalDiscount = model.TotalDiscount,
                            CreatedDate = DateTime.Now,
                            LastModified = DateTime.Now,
                            CreatedUser = string.IsNullOrEmpty(model.CreatedUser) ? model.Customer.Id : model.CreatedUser,
                            ModifiedUser = string.IsNullOrEmpty(model.ModifiedUser) ? model.Customer.Id : model.ModifiedUser,
                            Status = (byte)CMS_Common.Commons.EStatus.Actived,
                            IsTemp = model.IsTemp,
                        };
                        db.CMS_Order.Add(eOrder);
                        // create order detail
                        if(model.ListItem != null && model.ListItem.Any())
                        {
                            var lstOrderDetail = new List<CMS_OrderDetail>();
                            foreach(var item in model.ListItem)
                            {
                                lstOrderDetail.Add(new CMS_OrderDetail
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    OrderID = _OrderId,
                                    ProductID = item.ProductID,
                                    Price = item.Price,
                                    Quantity = (decimal)item.Quantity,
                                    Description = model.Customer.Description,
                                    CreatedDate = DateTime.Now,
                                    CreatedUser = string.IsNullOrEmpty(model.CreatedUser) ? model.Customer.Id : model.CreatedUser,
                                    ModifiedUser = string.IsNullOrEmpty(model.ModifiedUser) ? model.Customer.Id : model.ModifiedUser,
                                    LastModified = DateTime.Now,
                                    DiscountID = item.DiscountID,
                                    DiscountValue = item.DiscountValue,
                                    DiscountType = item.DiscountType,
                                });
                            }
                            db.CMS_OrderDetail.AddRange(lstOrderDetail);
                        }
                        db.SaveChanges();
                        trans.Commit();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        NSLog.Logger.Error("CreateOrder_Error:", ex);
                        trans.Rollback();
                        return false;
                    }
                    finally
                    {
                        m_Semaphore.Release();
                        db.Dispose();
                    }
                }
            }
        }

        public List<CMS_OrderModels> GetListOrder()
        {
            try
            {
                using (var db = new CMS_Context())
                {
                    var data = db.CMS_Order.GroupJoin(db.CMS_Customer, o => o.CustomerID, c => c.ID, (o, c) => new { o, c })
                                           .Select(o => new CMS_OrderModels
                                           {
                                               CreatedDate = o.o.CreatedDate,
                                               CustomerId = o.o.CustomerID,
                                               CustomerName = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select( x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                               Id = o.o.ID,
                                               OrderNo = o.o.OrderNo,
                                               TotalBill = o.o.TotalBill,
                                               Phone = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select(x => x.Phone).FirstOrDefault(),
                                               Address = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select(x => x.HomeStreet).FirstOrDefault()
                                           }).ToList();
                    return data;
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("GetListOrder :", ex);
            }
            return null;
        }

        public CMS_OrderModels GetDetailOrder(string OrderId)
        {
            var data = new CMS_OrderModels();
            try
            {
                NSLog.Logger.Info("GetDetailOrder_Request : ", OrderId);
                using (var db = new CMS_Context())
                {
                   data = db.CMS_Order.GroupJoin(db.CMS_OrderDetail, o => o.ID, d => d.OrderID, (o, d) => new { o, d })
                                           .GroupJoin(db.CMS_Customer, o => o.o.CustomerID, c => c.ID, (o, c) => new { o = o.o, d = o.d, c })
                                           .Where(o => o.o.ID.Equals(OrderId))
                                           .Select(r => new CMS_OrderModels
                                           {
                                                CreatedDate  = r.o.CreatedDate,
                                                CustomerId = r.o.CustomerID,
                                                CustomerName = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                                OrderNo = r.o.OrderNo,
                                                Id = r.o.ID,
                                                Phone = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.Phone).FirstOrDefault(),
                                                TotalBill = r.o.TotalBill,
                                                City = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeCity).FirstOrDefault(),
                                                Country = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeCountry).FirstOrDefault(),
                                                Email = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.Email).FirstOrDefault(),
                                                PostCode  = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeZipCode).FirstOrDefault(),
                                                Description = r.d.Select( x => x.Description).FirstOrDefault(),
                                                Address = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeStreet).FirstOrDefault(),
                                                Items = r.d.Select(x => new CMS_ItemModels
                                                {
                                                    Price = x.Price.HasValue ? x.Price.Value : 0,
                                                    ProductID = x.ProductID,
                                                    ProductName = x.CMS_Products.Name,
                                                    Quantity = x.Quantity.HasValue ? (double)x.Quantity.Value : 0,
                                                    TotalPrice = (x.Price.Value * (double) x.Quantity.Value)
                                                }).ToList()
                                           }).FirstOrDefault();
                    if (string.IsNullOrEmpty(data.Description))
                        data.Description = "Không có ghi chú.";
                    NSLog.Logger.Info("GetDetailOrder_Response : ", data);
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("GetDetailOrder :", ex);
            }
            return data;
        }

        public bool Delete(string Id)
        {
            var result = true;
            using (var db = new CMS_Context())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _detail = db.CMS_OrderDetail.Where(o => o.OrderID.Equals(Id)).ToList();
                        if (_detail != null)
                            db.CMS_OrderDetail.RemoveRange(_detail);
                        var _master = db.CMS_Order.Where(o => o.ID.Equals(Id)).FirstOrDefault();
                        if (_master != null)
                            db.CMS_Order.Remove(_master);
                        db.SaveChanges();
                        trans.Commit();
                    }
                    catch(Exception ex)
                    {
                        result = false;
                        NSLog.Logger.Error("Delete_Order:", ex);
                        trans.Rollback();
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            return result;
        }

        public bool Discount(string coupon_code, ref CMS_DiscountModels model)
        {
            var result = true;
            try
            {
                using (var db = new CMS_Context())
                {
                    var data = db.CMS_Discount.Where(o => o.DiscountCode.Equals(coupon_code))
                                              .Select(o => new CMS_DiscountModels
                                              {
                                                  DiscountCode = o.DiscountCode,
                                                  Id = o.ID,
                                                  Value = o.Value.HasValue ? o.Value.Value : 0,
                                                  IsPercent = o.ValueType == (byte)Commons.EValueType.Percent ? true : false,
                                                  ValueType = o.ValueType
                                              }).FirstOrDefault();
                    if (data == null)
                        result = false;
                    model = data;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Discount:", ex);
                result = false;
            }
            return result;
        }
    }
}
