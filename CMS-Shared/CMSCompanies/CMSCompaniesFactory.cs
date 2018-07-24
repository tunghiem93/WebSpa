using CMS_DataModel.Models;
using CMS_DTO.CMSCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSCompanies
{
    public class CMSCompaniesFactory
    {
        public bool CreateOrUpdate(CMS_CompanyModels model, ref string msg)
        {
            NSLog.Logger.Info("CompCreateOrUpdate", model);
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.ID)) /* insert */
                        {
                            var e = new CMS_Companies
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = model.Name,
                                Description = model.Description,
                                Email = model.Email,
                                Phone = model.Phone,
                                Address = model.Address,
                                LinkBlog = model.LinkBlog,
                                LinkTwiter = model.LinkTwiter,
                                LinkInstagram = model.LinkInstagram,
                                LinkFB = model.LinkFB,
                                ImageURL = model.ImageURL,
                                IsActive = true,
                                BusinessHour = model.Businesshour,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now
                            };
                            cxt.CMS_Companies.Add(e);
                        }
                        else /* update */
                        {
                            var e = cxt.CMS_Companies.Find(model.ID);
                            if (e != null)
                            {
                                e.Name = model.Name;
                                e.Description = model.Description;
                                e.Email = model.Email;
                                e.Phone = model.Phone;
                                e.Address = model.Address;
                                e.LinkBlog = model.LinkBlog;
                                e.LinkTwiter = model.LinkTwiter;
                                e.LinkInstagram = model.LinkInstagram;
                                e.LinkFB = model.LinkFB;
                                //e.ImageURL = model.ImageURL;
                                e.IsActive = true;
                                e.BusinessHour = model.Businesshour;
                                e.UpdatedBy = model.UpdatedBy;
                                e.UpdatedDate = DateTime.Now;
                                e.ImageURL = string.IsNullOrEmpty(model.ImageURL) ? e.ImageURL : model.ImageURL;
                            }
                            else
                            {
                                msg = "Unable to find company.";
                                result = false;
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();

                        NSLog.Logger.Info("ResponseCompCreateOrUpdate", new { result, msg });

                    }
                    catch (Exception ex)
                    {
                        result = false;
                        trans.Rollback();
                        msg = "Lỗi đường truyền mạng";
                        NSLog.Logger.Error("ErrorCompCreateOrUpdate", ex);
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return result;
        }

        public List<CMS_CompanyModels> GetList()
        {
            NSLog.Logger.Info("CompGetList");

            List<CMS_CompanyModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Companies.Select(x => new CMS_CompanyModels
                    {
                        ID = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Email = x.Email,
                        Phone = x.Phone,
                        Address = x.Address,
                        LinkBlog = x.LinkBlog,
                        LinkTwiter = x.LinkTwiter,
                        LinkInstagram = x.LinkInstagram,
                        LinkFB = x.LinkFB,
                        ImageURL = string.IsNullOrEmpty(x.ImageURL) ? "" : Commons.HostImage + x.ImageURL,
                        IsActive = true,
                        Businesshour = x.BusinessHour,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = DateTime.Now
                    }).ToList();
                    result = data; 

                    NSLog.Logger.Info("ResponseCompGetList", new { result });
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCompGetList", ex);
            }
            return result;
        }

        public CMS_CompanyModels GetInfor()
        {
            NSLog.Logger.Info("CompGetInfor");

            CMS_CompanyModels result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Companies.Select(x => new CMS_CompanyModels
                    {
                        ID = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Email = x.Email,
                        Phone = x.Phone,
                        Address = x.Address,
                        LinkBlog = x.LinkBlog,
                        LinkTwiter = x.LinkTwiter,
                        LinkInstagram = x.LinkInstagram,
                        LinkFB = x.LinkFB,
                        ImageURL = string.IsNullOrEmpty(x.ImageURL) ? "" : Commons.HostImage + x.ImageURL,
                        IsActive = true,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = DateTime.Now
                    }).FirstOrDefault();
                    
                    result = data;

                    NSLog.Logger.Info("ResponseCompGetInfor", new { result });
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCompGetInfor", ex);
            }
            return result;
        }
    }
}
