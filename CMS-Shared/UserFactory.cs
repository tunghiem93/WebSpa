using CMS_DataModel.Models;
using CMS_DTO.Models;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CMS_Shared.Factory
{
    public class UserFactory
    {
        protected static UserFactory _instance;
        public static UserFactory Instance
        {
            get
            {
                _instance = _instance != null ? _instance : new UserFactory();
                return _instance;
            }
        }

        public LoginResponseModel Login(LoginRequestModel info)
        {
            NSLog.Logger.Info("Employee Login Start", info);
            LoginResponseModel user = null;
            try
            {
                using (CMS_Context _db = new CMS_Context())
                {
                    info.Password = CommonHelper.Encrypt(info.Password);
                    string serverImage = ConfigurationManager.AppSettings["PublicImages"];

                    var emp = _db.CMS_Employee.Where(o => o.Email == info.Email.ToLower().Trim() && o.Password == info.Password).FirstOrDefault();
                    if (emp != null)
                    {
                        user = new LoginResponseModel()
                        {
                            EmployeeID = emp.ID,
                            EmployeeName = emp.Name,
                            EmployeeEmail = emp.Email,
                            EmployeeImageURL = string.IsNullOrEmpty(emp.ImageUrl) ? "" : serverImage + "Employees/" + emp.ImageUrl,
                            IsSupperAdmin = emp.IsSupperAdmin,
                        };

                        if (!user.IsSupperAdmin) /* not supper admin */
                        {
                            user.ListPermision = _db.CMS_ModulePermission.Where(o => o.RoleID == emp.RoleID)
                                                                        .Join(_db.CMS_Module, p => p.ModuleID, m => m.ID, (p, m) => new CMS_DTO.CMSRole.CMS_PermissionModels
                                                                        {
                                                                            Id = p.ID,
                                                                            ModuleID = m.ID,
                                                                            ModuleName = m.Name,
                                                                            ModuleCode = m.Code,
                                                                            IsAction = p.IsAction ?? true,
                                                                            IsView = p.IsView ?? true,
                                                                        }).ToList();
                        }
                        else /* is supper admin */
                        {
                            user.ListPermision = _db.CMS_Module.Select(o => new CMS_DTO.CMSRole.CMS_PermissionModels
                                                    {
                                                        Id = o.ID,
                                                        ModuleID = o.ID,
                                                        ModuleName = o.Name,
                                                        ModuleCode = o.Code,
                                                        IsAction = true,
                                                        IsView = true,
                                                    }).ToList();
                        }
                    }
                    NSLog.Logger.Info("Employee Login Done", user);
                }
            }
            catch (Exception ex) { NSLog.Logger.Error("Employee Login Error", ex); }
            return user;
        }
    }
}
