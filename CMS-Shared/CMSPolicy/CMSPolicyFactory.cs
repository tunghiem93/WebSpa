using CMS_DataModel.Models;
using CMS_DTO.CMSPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSPolicy
{
    public class CMSPolicyFactory
    {
        public CMS_PolicyModels GetData()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = new CMS_PolicyModels();
                    
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }

        public bool InsertOrUpdate(CMS_PolicyModels model, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.Id))
                        {
                            
                        }
                        else
                        {
                           
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
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
    }
}
