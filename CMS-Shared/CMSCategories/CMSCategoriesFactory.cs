using CMS_DTO.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_DataModel.Models;

namespace CMS_Shared.CMSCategories
{
    public class CMSCategoriesFactory
    {
        public bool CreateOrUpdate(CMSCategoriesModels model,ref string Id, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        
                        beginTran.Commit();
                    }
                    catch (Exception ex) {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public bool Delete(string Id , ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Categories.Find(Id);
                    cxt.CMS_Categories.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                msg = "Không thể xóa thể loại này";
                result = false;
            }
            return result;
        }

        public CMSCategoriesModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Categories.Select(x => new CMSCategoriesModels
                    {
                    }).Where(x=>x.Id.Equals(Id)).FirstOrDefault();
                    
                    return data;
                }
            }
            catch(Exception ex) { }
            return null;
        }

        public List<CMSCategoriesModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Categories.Select(x => new CMSCategoriesModels
                    {
                    }).ToList();

                    /* count number of product */
                    
                    /* response data */
                    return data;
                }
            }catch(Exception ex) { }
            return null;
        }
    }
}
