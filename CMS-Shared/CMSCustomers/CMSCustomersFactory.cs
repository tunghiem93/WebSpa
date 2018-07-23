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
        public bool InsertOrUpdate(CustomerModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if(string.IsNullOrEmpty(model.ID))
                        {
                            var e = new CMS_Customer
                            {
                            };
                            cxt.CMS_Customer.Add(e);
                        }
                        else
                        {
                            var e = cxt.CMS_Customer.Find(model.ID);
                            if(e != null)
                            {
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch(Exception ex) {
                        result = false;
                        trans.Rollback();
                        msg = "Lỗi đường truyền mạng";
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
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var e = cxt.CMS_Customer.Find(Id);
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex) {
                        result = false;
                        msg = "Không thể xóa thể loại này";
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

        public CustomerModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Where(x => x.ID.Equals(Id))
                                                .Select(x => new CustomerModels
                                                {
                                                    
                                                }).FirstOrDefault();
                    return data;
                }
            }
            catch(Exception ex) { }
            return null;
        }

        public List<CustomerModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Customer.Select(x => new CustomerModels
                    {
                    }).ToList();
                    return data;
                }
            }
            catch(Exception ex) { }
            return null;
        }
    }
}
