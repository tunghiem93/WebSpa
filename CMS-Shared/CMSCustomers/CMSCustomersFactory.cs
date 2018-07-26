using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSCustomers
{
    public class CMSCustomersFactory
    {
        public bool CreateOrUpdate(CustomerModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("CustomersCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.ID)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        var e = new CMS_Customer
                        {
                            ID = Id,
                            FirstName = model.Name,
                            LastName = model.LastName,
                            IsActive = model.IsActive,
                            Email = model.Email,
                            Gender = model.Gender,
                            Marital = model.MaritalStatus,
                            JoinedDate = DateTime.Now,
                            BirthDate = model.BirthDate,
                            HomeStreet = model.Address,
                            HomeCity = model.City,
                            ImageUrl = model.ImageURL,
                            /* other info */


                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                            Anniversary = Commons.MinDate,
                            ValidTo = DateTime.MinDate,
                        };
                        cxt.CMS_Customer.Add(e);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Customer.Find(model.ID);
                        if (e != null)
                        {
                            e.FirstName = model.FirstName;
                            e.LastName = model.LastName;
                            e.IsActive = model.IsActive;
                            e.Email = model.Email;
                            e.Gender = model.Gender;
                            e.Marital = model.MaritalStatus;
                            e.BirthDate = model.BirthDate;
                            e.HomeStreet = model.Address;
                            e.HomeCity = model.City;
                            e.ImageUrl = model.ImageURL;

                            /* other info */

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
                    NSLog.Logger.Info("ResponseCustomersCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorCustomersCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("CustomersDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Customer.Find(Id);
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

                    NSLog.Logger.Info("ResponseCustomersDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorCustomersDelete", ex);
            }
            return result;
        }

        public CustomerModels GetDetail(string Id)
        {
            NSLog.Logger.Info("CustomersGetDetail", Id);
            CustomerModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                                                .Select(o => new CustomerModels
                                                {
                                                    ID = o.ID,
                                                    FirstName = o.FirstName,
                                                    LastName = o.LastName,
                                                    IsActive = o.IsActive ?? true,
                                                    Email = o.Email,
                                                    Gender = o.Gender ?? true,
                                                    MaritalStatus = o.Marital ?? false,
                                                    BirthDate = o.BirthDate ?? Commons.MinDate,
                                                    Address = o.HomeStreet,
                                                    City = o.HomeCity,
                                                    Country = o.HomeCountry,
                                                    ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + o.ImageUrl,
                                                    /* other info */

                                                }).FirstOrDefault();

                    result = data;

                    NSLog.Logger.Info("ResponseCustomersGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCustomerGetDetail", ex);
            }
            return result;
        }

        public List<CustomerModels> GetList()
        {
            NSLog.Logger.Info("CustomerGetList");

            List<CustomerModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                                                .Select(o => new CustomerModels
                                                {
                                                    ID = o.ID,
                                                    FirstName = o.FirstName,
                                                    LastName = o.LastName,
                                                    IsActive = o.IsActive ?? true,
                                                    Email = o.Email,
                                                    Gender = o.Gender ?? true,
                                                    MaritalStatus = o.Marital ?? false,
                                                    BirthDate = o.BirthDate ?? Commons.MinDate,
                                                    Address = o.HomeStreet,
                                                    City = o.HomeCity,
                                                    Country = o.HomeCountry,
                                                    ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : Commons._PublicImages + o.ImageUrl,
                                                    /* other info */
                                                }).ToList();

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseCustomerGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCustomerGetList", ex);
            }
            return result;
        }

        public ClientLoginModel Login(ClientLoginModel model)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Where(x => x.Email.Equals(model.Email) &&
                                                         x.Password.Equals(model.Password) &&
                                                         x.IsActive.Value) 
                                              .Select(x => new ClientLoginModel
                                              {
                                                  Email = x.Email,
                                                  DisplayName = x.FirstName + " " + x.LastName,
                                                  Password = x.Password,
                                                  IsAdmin = false,
                                              })
                                              .FirstOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Login", ex);
            }
            return null;
        }
    }
}
