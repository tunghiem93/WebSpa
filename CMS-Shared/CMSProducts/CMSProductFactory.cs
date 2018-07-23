using CMS_DataModel.Models;
using CMS_DTO.CMSImage;
using CMS_DTO.CMSProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSProducts
{
    public class CMSProductFactory
    {
        public bool CreateOrUpdate(CMS_ProductsModels model,ref string msg)
        {
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex) {
                        Result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {

                    }catch(Exception)
                    {
                        Result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return Result;
        }

        public CMS_ProductsModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMS_ProductsModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Select(x=> new CMS_ProductsModels
                                               {
                                               }).ToList();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }

        public List<CMS_ImagesModels> GetListImage()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ImagesLink
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMS_ImagesModels> GetListImageOfProduct(string ProductId)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_ImagesLink.Where(x => x.ProductId.Equals(ProductId))
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool DeleteImage(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_ImagesLink.Find(Id);
                    if(e != null)
                    {
                        msg = e.ImageURL;
                        cxt.CMS_ImagesLink.Remove(e);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                    }
                }
            }
            catch(Exception ex)
            {
                result = false;
                msg = "Vui lòng kiểm tra đường truyền";
            }
            return result;
        }
    }
}
