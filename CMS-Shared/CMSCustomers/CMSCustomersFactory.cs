﻿using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSCustomer;
using CMS_Shared.Utilities;
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
                var _isExits = cxt.CMS_Customer.Where(x => x.Email.Equals(model.Email) && x.IsActive.HasValue && x.Status != (byte)Commons.EStatus.Deleted && x.ID != model.ID).FirstOrDefault();
                try
                {
                    if (string.IsNullOrEmpty(model.ID)) /* insert */
                    {
                        if (_isExits == null)
                        {
                            Id = Guid.NewGuid().ToString();
                            var e = new CMS_Customer
                            {
                                ID = Id,
                                FbID = model.FbID,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                IsActive = model.IsActive,
                                Email = model.Email,
                                Phone = model.Phone,
                                Password = model.Password,
                                Gender = model.Gender,
                                Marital = model.MaritalStatus,
                                JoinedDate = DateTime.Now,
                                BirthDate = model.BirthDate,
                                HomeStreet = model.Address,
                                HomeCity = model.City,
                                HomeCountry = model.Country,
                                HomeZipCode = model.Postcode,
                                ImageUrl = model.ImageURL,
                                /* other info */


                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                                Anniversary = Commons.MinDate,
                                ValidTo = Commons.MinDate,
                            };
                            cxt.CMS_Customer.Add(e);

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(model.FbID))
                            {
                                if (string.IsNullOrEmpty(_isExits.FbID)) /* update fb ID */
                                {
                                    Id = _isExits.ID;
                                    _isExits.FbID = model.FbID;
                                }
                                else
                                {
                                    msg = "Địa chỉ email đã tồn tại";
                                    Result = false;
                                }
                            }
                            else if (!string.IsNullOrEmpty(model.GoogleID))
                            {
                                if (string.IsNullOrEmpty(_isExits.GoogleID)) /* update google id */
                                {
                                    Id = _isExits.ID;
                                    _isExits.GoogleID = model.GoogleID;
                                }
                                else
                                {
                                    msg = "Địa chỉ email đã tồn tại";
                                    Result = false;
                                }
                            }
                            else
                            {
                                msg = "Địa chỉ email đã tồn tại";
                                Result = false;
                            }
                        }
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Customer.Find(model.ID);
                        if (e != null)
                        {
                            if (e.Email.Equals(model.Email) || _isExits == null)
                            {
                                e.FirstName = model.FirstName;
                                e.LastName = model.LastName;
                                e.IsActive = model.IsActive;
                                e.Email = model.Email;
                                e.Phone = model.Phone;
                                e.Password = model.Password;
                                e.Gender = model.Gender;
                                e.Marital = model.MaritalStatus;
                                e.BirthDate = model.BirthDate;
                                e.HomeStreet = model.Address;
                                e.HomeCity = model.City;
                                e.ImageUrl = model.ImageURL;
                                e.HomeCountry = model.Country;
                                e.HomeZipCode = model.Postcode;

                                /* other info */

                                e.ModifiedUser = model.CreatedBy;
                                e.LastModified = DateTime.Now;
                            }
                            else
                            {
                                msg = "Địa chỉ email đã tồn tại";
                                Result = false;
                            }
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


        public bool CheckExistLoginSosial(string Id)
        {
            bool result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Where(o => (o.FbID == Id || o.GoogleID == Id) && o.Status == (byte)Commons.EStatus.Actived).FirstOrDefault();
                    if (data == null)
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                NSLog.Logger.Error("ErrorCheckExistLoginSosial", ex);
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
                                                    Phone = o.Phone,
                                                    Password = o.Password,
                                                    Gender = o.Gender ?? true,
                                                    MaritalStatus = o.Marital ?? false,
                                                    BirthDate = o.BirthDate ?? Commons.MinDate,
                                                    Address = o.HomeStreet,
                                                    City = o.HomeCity,
                                                    Country = o.HomeCountry,
                                                    Postcode = o.HomeZipCode,
                                                    ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : o.ImageUrl,
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
                                                    Phone = o.Phone,
                                                    Gender = o.Gender ?? true,
                                                    MaritalStatus = o.Marital ?? false,
                                                    BirthDate = o.BirthDate ?? Commons.MinDate,
                                                    Address = o.HomeStreet,
                                                    City = o.HomeCity,
                                                    Country = o.HomeCountry,
                                                    ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : o.ImageUrl,
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
                    var data = cxt.CMS_Customer.Where(x => ((x.Email.Equals(model.Email) && x.Password == model.Password)
                                                                || (!string.IsNullOrEmpty(model.Fb_ID) && (x.FbID == model.Fb_ID || x.GoogleID == model.Fb_ID)))
                                                        && x.IsActive.Value
                                                        && x.Status == (byte)Commons.EStatus.Actived
                                                        )
                                              .Select(x => new ClientLoginModel
                                              {
                                                  Email = x.Email,
                                                  DisplayName = x.FirstName + " " + x.LastName,
                                                  Password = x.Password,
                                                  IsAdmin = false,
                                                  FirstName = x.FirstName,
                                                  LastName = x.LastName,
                                                  Phone = x.Phone,
                                                  Id = x.ID,
                                                  PostCode = x.HomeZipCode,
                                                  Address = x.HomeStreet,
                                                  Country = x.HomeCountry,
                                                  City = x.HomeCity,
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

        public bool ForgotPassword(string email, ref string msg)
        {
            NSLog.Logger.Info("CustomerForgotPassword", email);

            var result = false;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var emp = cxt.CMS_Customer.Where(o => o.Email.ToLower().Trim() == email.ToLower().Trim() && o.Status == (byte)Commons.EStatus.Actived).FirstOrDefault();
                    if (emp != null)
                    {
                        if (emp.IsActive ?? true)
                        {
                            string newPass = CommonHelper.GenerateCode(1, new List<string>(), 8).FirstOrDefault();

                            CommonHelper.SendContentMail(email, "New password: " + newPass, "", "[Lamode Beauté Home Spa] Forgot password");

                            emp.Password = CommonHelper.Encrypt(newPass);
                            emp.LastModified = DateTime.Now;

                            if (cxt.SaveChanges() > 0)
                                result = true;
                            else
                                msg = "Unable to change password.";
                        }
                        else
                            msg = "This customer is inactive. Please contact your administrator for more support.";
                    }
                    else
                        msg = "Email is not exist.";

                    NSLog.Logger.Info("ResponseCustomerForgotPassword", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorCustomerForgotPassword", ex);
            }
            return result;
        }
    }
}
