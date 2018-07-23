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
        public bool CreateOrUpdate(CMS_EmployeeModels model , ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if(string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var e = new CMS_Employee
                            {
                            };
                            cxt.CMS_Employee.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_Employee.Find(model.Id);
                            if(e != null)
                            {
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        msg = "Vui lòng kiểm tra đường truyền";
                        result = false;
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return result;
        }

        public bool Delete(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa nhân viên này";
                result = false;
            }
            return result;
        }

        public CMS_EmployeeModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Employee.Where(x => x.ID.Equals(Id))
                                                .Select(x => new CMS_EmployeeModels
                                                {
                                                }).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }

        public List<CMS_EmployeeModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Employee.Select(x => new CMS_EmployeeModels
                                                {
                                                }).ToList();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }
    }
}
