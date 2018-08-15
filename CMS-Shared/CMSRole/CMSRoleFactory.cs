using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSRole;

namespace CMS_Shared.CMSRole
{
    public class CMSRoleFactory
    {
        public bool CreateOrUpdate(CMS_RoleModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("RoleCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    model.Name = model.Name.Trim();
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        model.Id = Id;
                        var checkDup = cxt.CMS_Role.Where(o => o.Name.Trim() == model.Name.Trim()).FirstOrDefault();
                        if (checkDup == null)
                        {
                            var e = new CMS_Role
                            {
                                ID = Id,
                                StoreID = model.StoreID,
                                Name = model.Name,
                                IsActive = model.IsActive,
                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                            };
                            cxt.CMS_Role.Add(e);
                        }
                        else
                        {
                            msg = "Duplicate Name";
                            Result = false;
                        }

                    }
                    else /* updated */
                    {
                        var checkDupCode = cxt.CMS_Role.Where(o => o.Name.Trim() == model.Name.Trim() && o.ID != model.Id).FirstOrDefault();
                        if (checkDupCode == null)
                        {
                            var e = cxt.CMS_Role.Find(model.Id);
                            if (e != null)
                            {
                                e.Name = model.Name;
                                e.IsActive = model.IsActive;
                                e.Status = (byte)Commons.EStatus.Actived;
                                e.ModifiedUser = model.CreatedBy;
                                e.LastModified = DateTime.Now;
                            }
                            else
                            {
                                Result = false;
                                msg = "Unable to find role.";
                            }
                        }
                        else
                        {
                            msg = "Duplicate name";
                            Result = false;
                        }
                    }

                    if (cxt.SaveChanges() > 0 && Result == true)
                    {
                        /* insert list module permission */
                        CreateOrUpdatePermission(model);
                    }

                    NSLog.Logger.Info("ResponseRoleCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "System Error.";
                    NSLog.Logger.Error("ErrorRoleCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool CreateOrUpdatePermission(CMS_RoleModels model)
        {
            NSLog.Logger.Info("RoleCreateOrUpdatePermission", model);

            var result = false;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var listInsertPermission = model.ListPermission.Where(o => string.IsNullOrEmpty(o.Id)).ToList();
                    if (listInsertPermission.Count > 0) /* insert */
                    {
                        var listInrtPerDB = new List<CMS_ModulePermission>();
                        listInsertPermission.ForEach(o =>
                        {
                            listInrtPerDB.Add(new CMS_ModulePermission()
                            {
                                ID = Guid.NewGuid().ToString(),
                                ModuleID = o.ModuleID,
                                RoleID = model.Id,
                                StoreID = model.StoreID,
                                IsView = o.IsView,
                                IsAction = o.IsAction,
                                IsActive = true,
                                Status = (byte)Commons.EStatus.Actived,
                                CreatedDate = DateTime.Now,
                                CreatedUser = model.CreatedBy,
                                ModifiedUser = model.CreatedBy,
                                LastModified = DateTime.Now,
                            });
                        });
                        cxt.CMS_ModulePermission.AddRange(listInrtPerDB);
                    }

                    var listUpdatePermission = model.ListPermission.Where(o => !string.IsNullOrEmpty(o.Id)).ToList();
                    if (listUpdatePermission.Count > 0) /* update permission */
                    {
                        var listModulePerID = listUpdatePermission.Select(o => o.Id).ToList();
                        var listPerDB = cxt.CMS_ModulePermission.Where(o => listModulePerID.Contains(o.ID)).ToList();
                        foreach (var per in listPerDB)
                        {
                            var updatePer = listUpdatePermission.Where(o => o.Id == per.ID).FirstOrDefault();
                            per.IsAction = updatePer.IsAction;
                            per.IsView = updatePer.IsView;
                            per.ModifiedUser = model.CreatedBy;
                            per.LastModified = DateTime.Now;
                        }

                    }

                    if (cxt.SaveChanges() > 0)
                        result = true;
                    NSLog.Logger.Info("ResponseRolemodelCreateOrUpdatePermission", new { result });
                }
            }
            catch (Exception ex)
            {
                result = false;
                NSLog.Logger.Error("ErrorRoleCreateOrUpdatePermission", ex);
            }
            return result;
        }


        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("RoleDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Role.Find(Id);
                    if (e != null)
                    {
                        e.Status = (byte)Commons.EStatus.Deleted;
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Unable to find Role.";
                        result = false;
                    }

                    NSLog.Logger.Info("ResponseRoleDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "System Error.";
                result = false;
                NSLog.Logger.Error("ErrorRoleDelete", ex);
            }
            return result;
        }

        public CMS_RoleModels GetDetail(string Id)
        {
            NSLog.Logger.Info("RoleGetDetail", Id);
            CMS_RoleModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Role.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_RoleModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            IsActive = o.IsActive ?? false,
                        }).FirstOrDefault();

                    /* get list module */
                    var listModule = cxt.CMS_Module.OrderBy(o => o.Code)
                        .Select(o => new CMS_PermissionModels()
                        {
                            ModuleID = o.ID,
                            ModuleName = o.Name,
                            ModuleCode = o.Code,
                        }).ToList();

                    /* get list permission*/
                    var listPermission = cxt.CMS_ModulePermission.Where(o => o.RoleID == Id).ToList();
                    listModule.ForEach(o =>
                    {
                        o.Id = listPermission.Where(p => p.ModuleID == o.ModuleID).Select(p => p.ID).FirstOrDefault();
                        o.IsAction = listPermission.Where(p => p.ModuleID == o.ModuleID).Select(p => p.IsAction).FirstOrDefault() ?? false;
                        o.IsView = listPermission.Where(p => p.ModuleID == o.ModuleID).Select(p => p.IsView).FirstOrDefault() ?? false;
                    });

                    data.ListPermission = listModule;
                    result = data;

                    NSLog.Logger.Info("ResponseRoleGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorRoleGetDetail", ex);
            }
            return result;
        }

        public List<CMS_RoleModels> GetList()
        {
            NSLog.Logger.Info("RoleGetList");

            List<CMS_RoleModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Role.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(o => new CMS_RoleModels
                        {
                            Id = o.ID,
                            StoreID = o.StoreID,
                            Name = o.Name,
                            IsActive = o.IsActive ?? false,
                        }).ToList();

                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseRoleGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorRoleGetList", ex);
            }
            return result;
        }

        public List<CMS_PermissionModels> GetListModule()
        {
            NSLog.Logger.Info("RoleGetListModule");
            List<CMS_PermissionModels> result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    /* get list module */
                    result = cxt.CMS_Module.OrderBy(o => o.Code)
                        .Select(o => new CMS_PermissionModels()
                        {
                            ModuleID = o.ID,
                            ModuleName = o.Name,
                            ModuleCode = o.Code,
                        }).ToList();
                    NSLog.Logger.Info("ResponseRoleGetListModule", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorRoleGetListModule", ex);
            }
            return result;
        }
    }
}
