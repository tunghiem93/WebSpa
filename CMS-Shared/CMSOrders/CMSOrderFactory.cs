using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSOrder;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSOrders
{
    public class CMSOrderFactory
    {
        public bool CreateOrder(CMS_CheckOutModels model)
        {
            NSLog.Logger.Info("CreateOrder_Request:", model);
            using (var db = new CMS_Context())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if(string.IsNullOrEmpty(model.Customer.Id))
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
                                Status = (byte)CMS_Common.Commons.EStatus.Actived,
                                Anniversary = Commons.MinDate,
                                ValidTo = Commons.MinDate,

                            };
                            db.CMS_Customer.Add(eCus);
                        }
                        // create order
                        var _OrderId = Guid.NewGuid().ToString();
                        var eOrder = new CMS_Order
                        {
                            ID = _OrderId,
                            StoreID = model.StoreID,
                            OrderNo = CommonHelper.RandomNumberOrder(),
                            CustomerID = model.Customer.Id,
                            TotalBill = model.TotalPrice,
                            SubTotal = model.SubTotalPrice,
                            CreatedDate = DateTime.Now,
                            LastModified = DateTime.Now,
                            CreatedUser = string.IsNullOrEmpty(model.CreatedUser) ? model.Customer.Id : model.CreatedUser,
                            ModifiedUser = string.IsNullOrEmpty(model.ModifiedUser) ? model.Customer.Id : model.ModifiedUser,
                            Status = (byte)CMS_Common.Commons.EStatus.Actived
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
                        db.Dispose();
                    }
                }
            }
        }
    }
}
