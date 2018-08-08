using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSEmployees
{
    public class CMSEmployeeFactory
    {
        public bool CreateOrUpdate(CMS_EmployeeModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("EmployeeCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        var e = new CMS_Employee
                        {
                            ID = Id,
                            Name = model.Name,
                            Email = model.Employee_Email,
                            Password = model.Password,
                            IsActive = model.IsActive,
                            Phone = model.Employee_Phone,
                            PinCode = model.PinCode,
                            Gender = model.Gender,
                            Marital = model.Marital,
                            HiredDate = model.HiredDate,
                            BirthDate = model.BirthDate,
                            Street = model.Street,
                            City = model.City,
                            Country = model.Country,
                            ZipCode = model.ZipCode,
                            ImageUrl = model.ImageURL,
                            StoreID = model.StoreID,
                            IsSupperAdmin = model.IsSupperAdmin,
                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                            Quote = model.Quote,
                        };
                        cxt.CMS_Employee.Add(e);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Employee.Find(model.Id);
                        if (e != null)
                        {
                            e.Name = model.Name;
                            e.Email = model.Employee_Email;
                            //Password = model.Password,
                            e.IsActive = model.IsActive;
                            e.Phone = model.Employee_Phone;
                            e.PinCode = model.PinCode;
                            e.Gender = model.Gender;
                            e.Marital = model.Marital;
                            e.HiredDate = model.HiredDate;
                            e.BirthDate = model.BirthDate;
                            e.Street = model.Street;
                            e.City = model.City;
                            e.Country = model.Country;
                            e.ZipCode = model.ZipCode;
                            e.ImageUrl = model.ImageURL;
                            e.StoreID = model.StoreID;
                            e.IsSupperAdmin = model.IsSupperAdmin;
                            e.Status = (byte)Commons.EStatus.Actived;
                            e.ModifiedUser = model.CreatedBy;
                            e.LastModified = DateTime.Now;
                            e.Quote = model.Quote;
                        }
                        else
                        {
                            Result = false;
                            msg = "Unable to find Discount.";
                        }
                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseEmployeeCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorEmployeeCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("EmployeeDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Employee.Find(Id);
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

                    NSLog.Logger.Info("ResponseEmployeeDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorEmployeeDelete", ex);
            }
            return result;
        }

        public CMS_EmployeeModels GetDetail(string Id)
        {
            NSLog.Logger.Info("EmployeeGetDetail", Id);
            CMS_EmployeeModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Employee.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_EmployeeModels
                        {
                            Id = o.ID,
                            Name = o.Name,
                            Employee_Email = o.Email,
                            Password = o.Password,
                            IsActive = o.IsActive?? true,
                            Employee_Phone = o.Phone,
                            PinCode = o.PinCode,
                            Gender = o.Gender,
                            Marital = o.Marital,
                            HiredDate = o.HiredDate,
                            BirthDate = o.BirthDate,
                            Street = o.Street,
                            City = o.City,
                            Country = o.Country,
                            ZipCode = o.ZipCode,
                            ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : o.ImageUrl,
                            StoreID = o.StoreID,
                            IsSupperAdmin = o.IsSupperAdmin,
                            Quote = o.Quote,
                        }).FirstOrDefault();

                    result = data;

                    NSLog.Logger.Info("ResponseEmployeeGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorEmployeeGetDetail", ex);
            }
            return result;
        }

        public List<CMS_EmployeeModels> GetList()
        {
            NSLog.Logger.Info("EmployeeGetList");

            List<CMS_EmployeeModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Employee.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_EmployeeModels
                        {
                            Id = o.ID,
                            Name = o.Name,
                            Employee_Email = o.Email,
                            Password = o.Password,
                            IsActive = o.IsActive ?? true,
                            Employee_Phone = o.Phone,
                            PinCode = o.PinCode,
                            Gender = o.Gender,
                            Marital = o.Marital,
                            HiredDate = o.HiredDate,
                            BirthDate = o.BirthDate,
                            Street = o.Street,
                            City = o.City,
                            Country = o.Country,
                            ZipCode = o.ZipCode,
                            ImageURL = string.IsNullOrEmpty(o.ImageUrl) ? "" : o.ImageUrl,
                            StoreID = o.StoreID,
                            IsSupperAdmin = o.IsSupperAdmin,
                            Quote = o.Quote,
                        }).ToList();

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseEmployeeGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorEmployeeGetList", ex);
            }
            return result;
        }
    }
}
