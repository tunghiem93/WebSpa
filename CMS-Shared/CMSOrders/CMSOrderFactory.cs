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

        public bool CreateOrder(CMS_CheckOutModels model, ref string OrderId)
        {
            NSLog.Logger.Info("CreateOrder_Request:", model);
            var ret = true;
            using (var db = new CMS_Context())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    m_Semaphore.WaitOne();
                    try
                    {
                        var active = (byte)Commons.EStatus.Actived;
                        if (string.IsNullOrEmpty(model.Customer.Id) && !string.IsNullOrEmpty(model.Customer.LastName))
                        {
                            // create new customer 
                            model.Customer.Id = Guid.NewGuid().ToString();
                            var eCus = new CMS_Customer
                            {
                                ID = model.Customer.Id,
                                FirstName = model.Customer.FirstName,
                                LastName = model.Customer.LastName,
                                Email = model.Customer.Email,
                                Phone = model.Customer.Phone,
                                CreatedDate = DateTime.Now,
                                LastModified = DateTime.Now,
                                CustomerType = (int)CMS_Common.Commons.ECustomerType.Anonymous,
                                HomeCountry = model.Customer.Country,
                                OfficeZipCode = model.Customer.PostCode,
                                Status = active,
                                Anniversary = Commons.MinDate,
                                ValidTo = Commons.MinDate,
                                HomeStreet = model.Customer.Address,
                                OfficeStreet = model.Customer.Address,
                            };
                            db.CMS_Customer.Add(eCus);
                        }
                        // create order
                        OrderId = Guid.NewGuid().ToString();
                        var eOrder = new CMS_Order
                        {
                            ID = OrderId,
                            StoreID = model.StoreID,
                            //OrderNo = CommonHelper.RandomNumberOrder(),
                            OrderNo = CommonHelper.GenerateOrderNo(model.StoreID, active, model.OrderType),
                            ReceiptNo = model.IsTemp ? "" : CommonHelper.GenerateReceiptNo(model.StoreID, active, model.OrderType),
                            ReceiptCreatedDate = model.IsTemp ? Commons.MinDate : DateTime.Now,
                            CustomerID = model.Customer.Id,
                            TotalBill = model.TotalPrice,
                            SubTotal = model.SubTotalPrice,
                            TotalDiscount = model.TotalDiscount,
                            Cashier = model.IsTemp ? "" : model.CreatedUser,
                            CreatedDate = DateTime.Now,
                            LastModified = DateTime.Now,
                            CreatedUser = string.IsNullOrEmpty(model.CreatedUser) ? model.Customer.Id : model.CreatedUser,
                            ModifiedUser = string.IsNullOrEmpty(model.ModifiedUser) ? model.Customer.Id : model.ModifiedUser,
                            Status = active,
                            IsTemp = model.IsTemp,
                            OrderType = model.OrderType,
                        };
                        db.CMS_Order.Add(eOrder);
                        // create order detail
                        if (model.ListItem != null && model.ListItem.Any())
                        {
                            var lstOrderDetail = new List<CMS_OrderDetail>();
                            foreach (var item in model.ListItem)
                            {
                                lstOrderDetail.Add(new CMS_OrderDetail
                                {
                                    ID = Guid.NewGuid().ToString(),
                                    OrderID = OrderId,
                                    ProductID = item.ProductID,
                                    Remark = string.IsNullOrEmpty(item.ProductID) ? item.ProductName : "",
                                    Price = item.Price,
                                    Quantity = (decimal)item.Quantity,
                                    Description = string.IsNullOrEmpty(model.Customer.Description) ? item.Description : model.Customer.Description,
                                    CreatedDate = DateTime.Now,
                                    CreatedUser = string.IsNullOrEmpty(model.CreatedUser) ? model.Customer.Id : model.CreatedUser,
                                    ModifiedUser = string.IsNullOrEmpty(model.ModifiedUser) ? model.Customer.Id : model.ModifiedUser,
                                    //CreatedUser = model.IsTemp ? model.Customer.Id : (string.IsNullOrEmpty(item.EmployeeID) ? model.CreatedUser : item.EmployeeID),
                                    //ModifiedUser = model.IsTemp ? model.Customer.Id : (string.IsNullOrEmpty(item.EmployeeID) ? model.ModifiedUser : item.EmployeeID),
                                    EmployeeID = item.EmployeeID,
                                    LastModified = DateTime.Now,
                                    DiscountID = item.DiscountID,
                                    DiscountValue = item.DiscountValue,
                                    DiscountType = item.DiscountType,
                                    DiscountAmount = item.DiscountAmount,
                                    Status = active,
                                });
                            }
                            db.CMS_OrderDetail.AddRange(lstOrderDetail);

                            /* update quantity of product */
                            var listProductID = model.ListItem.Select(o => o.ProductID).ToList();
                            var listProductDB = db.CMS_Products.Where(o => listProductID.Contains(o.ID) && o.Status == (byte)Commons.EStatus.Actived && o.TypeCode == (byte)Commons.EProductType.Product).ToList();
                            foreach (var product in listProductDB)
                            {
                                var sign = -1;
                                if (model.OrderType == (byte)Commons.EOrderType.Expense)
                                {
                                    sign = +1;
                                }

                                product.Quantity = product.Quantity + ((sign) * (decimal)(model.ListItem.Where(o => o.ProductID == product.ID).Select(o => o.Quantity).FirstOrDefault()));

                                //if (product.Quantity < 0)
                                //{
                                //    ret = false;
                                //}
                            }

                        }

                        if (ret == true)
                        {
                            db.SaveChanges();
                            trans.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("CreateOrder_Error:", ex);
                        trans.Rollback();
                        ret = false;
                    }
                    finally
                    {
                        m_Semaphore.Release();
                        db.Dispose();
                    }
                }
            }
            return ret;
        }

        public List<CMS_OrderModels> GetListOrder(string cusID = "")
        {
            try
            {
                using (var db = new CMS_Context())
                {
                    var query = db.CMS_Order.Where(o => o.Status == (byte)Commons.EStatus.Actived);
                    if (!string.IsNullOrEmpty(cusID))
                        query = query.Where(o => o.CustomerID == cusID);

                    var data = query.GroupJoin(db.CMS_Customer, o => o.CustomerID, c => c.ID, (o, c) => new { o, c })
                                           .Select(o => new CMS_OrderModels
                                           {
                                               CreatedDate = o.o.CreatedDate,
                                               CustomerId = o.o.CustomerID,
                                               CustomerName = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select(x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                               Id = o.o.ID,
                                               OrderNo = o.o.OrderNo,
                                               TotalBill = o.o.TotalBill,
                                               Phone = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select(x => x.Phone).FirstOrDefault(),
                                               Address = string.IsNullOrEmpty(o.o.CustomerID) ? "" : o.c.Select(x => x.HomeStreet).FirstOrDefault(),
                                               TotalDiscount = o.o.TotalDiscount,
                                               OrderType = o.o.OrderType,
                                               EmployeeName = o.o.CreatedUser,
                                               IsTemp = o.o.IsTemp,
                                               ReceiptNo = o.o.ReceiptNo,
                                           }).ToList();
                    return data;
                }
            }
            catch (Exception ex)
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
                                                CreatedDate = r.o.CreatedDate,
                                                CustomerId = r.o.CustomerID,
                                                CustomerName = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.FirstName + " " + x.LastName).FirstOrDefault(),
                                                OrderNo = r.o.OrderNo,
                                                Id = r.o.ID,
                                                Phone = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.Phone).FirstOrDefault(),
                                                TotalBill = r.o.TotalBill,
                                                SubTotal = r.o.SubTotal,
                                                TotalDiscount = r.o.TotalDiscount,
                                                City = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeCity).FirstOrDefault(),
                                                Country = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeCountry).FirstOrDefault(),
                                                Email = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.Email).FirstOrDefault(),
                                                PostCode = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeZipCode).FirstOrDefault(),
                                                Description = r.d.Select(x => x.Description).FirstOrDefault(),
                                                Address = string.IsNullOrEmpty(r.o.CustomerID) ? "" : r.c.Select(x => x.HomeStreet).FirstOrDefault(),
                                                Items = r.d.Select(x => new CMS_ItemModels
                                                {
                                                    Price = x.Price.HasValue ? x.Price.Value : 0,
                                                    ProductID = x.ProductID,
                                                    ProductName = x.CMS_Products.Name,
                                                    Quantity = x.Quantity.HasValue ? (double)x.Quantity.Value : 0,
                                                    TotalPrice = (x.Price.Value * (double)x.Quantity.Value),
                                                    DiscountID = x.DiscountID,
                                                    DiscountType = x.DiscountType,
                                                    DiscountValue = (float)x.DiscountValue,
                                                    Description = x.Description,
                                                    EmployeeID = x.EmployeeID,
                                                    EmployeeName = x.CMS_Employees != null ? x.CMS_Employees.Name : "",
                                                }).ToList()
                                            }).FirstOrDefault();

                    //var listEmployeeID = data.Items.Select(o => o.EmployeeID).ToList();
                    //var listEmp = db.CMS_Employee.Where(o => listEmployeeID.Contains(o.ID)).ToList();
                    //data.Items.ForEach(o =>
                    //{
                    //    o.EmployeeName = listEmp.Where(e => e.ID == o.EmployeeID).Select(e => e.Name).FirstOrDefault();
                    //});

                    NSLog.Logger.Info("GetDetailOrder_Response : ", data);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailOrder :", ex);
            }
            return data;
        }

        public bool Delete(string Id, string Reason = "")
        {
            var result = true;
            using (var db = new CMS_Context())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _master = db.CMS_Order.Where(o => o.ID.Equals(Id)).FirstOrDefault();
                        if (_master != null)
                        {
                            _master.Status = (byte)CMS_Common.Commons.EStatus.Deleted;
                            _master.Reason = Reason;
                        }

                        // db.CMS_Order.Remove(_master);

                        var _detail = db.CMS_OrderDetail.Where(o => o.OrderID.Equals(Id)).ToList();
                        if (_detail != null)
                        {
                            foreach (var item in _detail)
                            {
                                item.Status = (byte)CMS_Common.Commons.EStatus.Deleted;
                            }
                        }
                        //db.CMS_OrderDetail.RemoveRange(_detail);

                        /* update quantity of product */
                        var listProductID = _detail.Select(o => o.ProductID).ToList();
                        var listProductDB = db.CMS_Products.Where(o => listProductID.Contains(o.ID) && o.Status == (byte)Commons.EStatus.Actived && o.TypeCode == (byte)Commons.EProductType.Product).ToList();
                        foreach (var product in listProductDB)
                        {
                            var sign = 1;
                            if (_master.OrderType == (byte)Commons.EOrderType.Expense)
                            {
                                sign = -1;
                            }

                            product.Quantity = product.Quantity + ((sign) * (decimal)(_detail.Where(o => o.ProductID == product.ID).Select(o => o.Quantity).FirstOrDefault()));

                            //if (product.Quantity < 0)
                            //{
                            //    result = false;
                            //}
                        }

                        if (result == true)
                        {
                            db.SaveChanges();
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
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

        public bool CheckOut(string OrderId, string createdUser)
        {
            var result = true;
            try
            {
                NSLog.Logger.Info("CheckOut_Request:", OrderId);
                using (var db = new CMS_Context())
                {
                    var Order = db.CMS_Order.Find(OrderId);
                    if (Order != null)
                    {
                        Order.ReceiptNo = CommonHelper.GenerateReceiptNo(Order.StoreID, (byte)Commons.EStatus.Actived, (byte)Commons.EOrderType.Normal);
                        Order.LastModified = DateTime.Now;
                        Order.ModifiedUser = createdUser;
                        Order.Cashier = createdUser;
                        Order.ReceiptCreatedDate = DateTime.Now;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                NSLog.Logger.Error("CheckOut_Order:", ex);
            }
            return result;
        }
    }
}
