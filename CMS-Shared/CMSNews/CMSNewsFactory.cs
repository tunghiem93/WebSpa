using CMS_DataModel.Models;
using CMS_DTO.CMSNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSNews
{
    public class CMSNewsFactory
    {
        public bool CreateOrUpdate(CMS_NewsModels model, ref string msg)
        {
            NSLog.Logger.Info("NewsCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        var _Id = Guid.NewGuid().ToString();
                        var e = new CMS_News
                        {
                            Id = _Id,
                            Title = model.Title,
                            Short_Description = model.Short_Description,
                            ImageURL = model.ImageURL,
                            Description = model.Description,
                            IsActive = model.IsActive,
                            Author = model.Author,
                            Category = model.Category,
                            Source = model.Source,
                            CreatedUser = model.CreatedBy,
                            CreatedDate = DateTime.Now,
                            ModifiedUser = model.UpdatedBy,
                            LastModified = DateTime.Now,
                            Status = (byte)Commons.EStatus.Actived,
                        };
                        cxt.CMS_News.Add(e);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_News.Find(model.Id);
                        if (e != null)
                        {
                            e.Title = model.Title;
                            e.Short_Description = model.Short_Description;
                            e.ImageURL = model.ImageURL;
                            e.Description = model.Description;
                            e.Author = model.Author;
                            e.Source = model.Source;
                            e.Category = model.Category;
                            e.ModifiedUser = model.UpdatedBy;
                            e.LastModified = DateTime.Now;
                            e.IsActive = model.IsActive;
                        }
                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseNewsCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "Vui lòng kiểm tra đường truyền";
                    NSLog.Logger.Error("ErrorNewsCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("NewsDelete", Id);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var e = cxt.CMS_News.Find(Id);
                        if (e != null)
                        {
                            e.Status = (byte)Commons.EStatus.Deleted;
                            cxt.SaveChanges();
                            trans.Commit();
                        }
                        else
                        {
                            msg = "Vui lòng kiểm tra đường truyền";
                        }
                        NSLog.Logger.Info("ResponseNewsDelete", new { Result, msg });

                    }
                    catch (Exception ex)
                    {
                        Result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                        trans.Rollback();
                        NSLog.Logger.Info("ErrorNewsDelete", ex);
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return Result;
        }

        public CMS_NewsModels GetDetail(string Id)
        {
            NSLog.Logger.Info("NewsGetDetail", Id);
            CMS_NewsModels ret = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_News.Where(x => x.Id.Equals(Id) && x.Status == (byte)Commons.EStatus.Actived).FirstOrDefault();
                    if (e != null)
                    {
                        ret = new CMS_NewsModels
                        {
                            Id = e.Id,
                            CreatedBy = e.CreatedUser,
                            CreatedDate = e.CreatedDate,
                            Description = e.Description,
                            IsActive = e.IsActive,
                            Title = e.Title,
                            Short_Description = e.Short_Description,
                            ImageURL = e.ImageURL,
                            Author = e.Author,
                            Category = e.Category,
                            Source = e.Source,
                            UpdatedBy = e.ModifiedUser,
                            UpdatedDate = e.LastModified,
                        };
                    }
                }
                NSLog.Logger.Info("ResponseNewsGetDetail", ret);

            }
            catch (Exception ex)
            {
                NSLog.Logger.Info("ErrorNewsGetDetail", ex);
            }
            return ret;
        }

        public List<CMS_NewsModels> GetList()
        {
            NSLog.Logger.Info("NewsGetList");
            List<CMS_NewsModels> data = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    data = cxt.CMS_News.Where(x=> x.Status == (byte)Commons.EStatus.Actived)
                                    .Select(x => new CMS_NewsModels
                                    {
                                        Id = x.Id,
                                        Title = x.Title,
                                        Short_Description = x.Short_Description,
                                        ImageURL = x.ImageURL,
                                        Author = x.Author,
                                        Category = x.Category,
                                        Source = x.Source,
                                        Description = x.Description,
                                        IsActive = x.IsActive,
                                        CreatedDate = x.CreatedDate,
                                        CreatedBy = x.CreatedUser,
                                        UpdatedBy = x.ModifiedUser,
                                        UpdatedDate = x.LastModified,
                                    }).ToList();
                }
                NSLog.Logger.Info("ResponseNewsGetList", data);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Info("ErrorNewsGetList", ex);
            }
            return data;
        }
    }
}
