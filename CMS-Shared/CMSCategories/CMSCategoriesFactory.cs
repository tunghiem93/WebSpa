using CMS_DTO.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_DataModel.Models;
using CMS_Common;

namespace CMS_Shared.CMSCategories
{
    public class CMSCategoriesFactory
    {
        public bool CreateOrUpdate(CMSCategoriesModels model, ref string Id, ref string msg)
        {
            NSLog.Logger.Info("CateCreateOrUpdate", model);
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                try
                {
                    if (string.IsNullOrEmpty(model.Id)) /* insert */
                    {
                        Id = Guid.NewGuid().ToString();
                        var e = new CMS_Categories
                        {
                            ID = Id,
                            StoreID = model.StoreID,
                            Name = model.CategoryName,
                            TotalProducts = 0,
                            Description = model.Description,
                            ImageURL = model.ImageURL,
                            Status = (byte)Commons.EStatus.Actived,
                            CreatedDate = DateTime.Now,
                            CreatedUser = model.CreatedBy,
                            ModifiedUser = model.CreatedBy,
                            LastModified = DateTime.Now,
                            ProductTypeCode = model.ProductTypeCode,
                            IsShowInReservation = model.IsShowInReservation,
                            Sequence = model.Sequence,
                            IsActive = model.IsActive,
                        };
                        cxt.CMS_Categories.Add(e);
                    }
                    else /* updated */
                    {
                        var e = cxt.CMS_Categories.Find(model.Id);
                        if (e != null)
                        {
                            e.Name = model.CategoryName;
                            e.TotalProducts = 0;
                            e.Description = model.Description;
                            e.ImageURL = model.ImageURL;
                            e.ModifiedUser = model.CreatedBy;
                            e.LastModified = DateTime.Now;
                            e.ProductTypeCode = model.ProductTypeCode;
                            e.IsShowInReservation = model.IsShowInReservation;
                            e.Sequence = model.Sequence;
                            e.IsActive = model.IsActive;
                        }
                        else
                        {
                            Result = false;
                            msg = "Unable to find Category.";
                        }
                    }

                    cxt.SaveChanges();
                    NSLog.Logger.Info("ResponseCateCreateOrUpdate", new { Result, msg });

                }
                catch (Exception ex)
                {
                    Result = false;
                    msg = "Vui lòng kiểm tra đường truyền";
                    NSLog.Logger.Error("ErrorCateCreateOrUpdate", ex);
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            NSLog.Logger.Info("CateDelete", Id);

            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Categories.Find(Id);
                    if (e != null)
                    {
                        e.Status = (byte)Commons.EStatus.Deleted;
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Unable to find cate.";
                        result = false;
                    }

                    NSLog.Logger.Info("ResponseCateDelete", new { result, msg });
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa thể loại này";
                result = false;
                NSLog.Logger.Error("ErrorCateDelete", ex);
            }
            return result;
        }

        public CMSCategoriesModels GetDetail(string Id)
        {
            NSLog.Logger.Info("CateGetDetail", Id);
            CMSCategoriesModels result = null;

            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Categories.Where(o => o.ID == Id && o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(x => new CMSCategoriesModels
                        {
                            Id = x.ID,
                            ParentId = x.ParentID,
                            CategoryName = x.Name,
                            StoreID = x.StoreID,
                            NumberOfProduct = x.TotalProducts ?? 0,
                            Description = x.Description,
                            ImageURL = string.IsNullOrEmpty(x.ImageURL) ? "" : Commons._PublicImages + "Categories/" + x.ImageURL,
                            ProductTypeCode = x.ProductTypeCode,
                            IsShowInReservation = x.IsShowInReservation,
                            IsActive = x.IsActive,
                            Sequence = x.Sequence,
                        }).FirstOrDefault();

                    result = data;

                    NSLog.Logger.Info("ResponseCateGetDetail", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCateGetDetail", ex);
            }
            return result;
        }

        public List<CMSCategoriesModels> GetList()
        {
            NSLog.Logger.Info("CateGetList");

            List<CMSCategoriesModels> result = null;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Categories.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .Select(x => new CMSCategoriesModels
                        {
                            Id = x.ID,
                            ParentId = x.ParentID,
                            CategoryName = x.Name,
                            StoreID = x.StoreID,
                            NumberOfProduct = x.TotalProducts ?? 0,
                            Description = x.Description,
                            ImageURL = string.IsNullOrEmpty(x.ImageURL) ? "" : Commons._PublicImages + "Categories/" + x.ImageURL,
                            ProductTypeCode = x.ProductTypeCode,
                            IsShowInReservation = x.IsShowInReservation,
                            IsActive = x.IsActive,
                            Sequence = x.Sequence,
                        }).ToList();

                    var listCountProductCate = cxt.CMS_Products.Where(o => o.Status != (byte)Commons.EStatus.Deleted)
                        .GroupBy(o => o.CategoryID).Select(o => new
                        {
                            CateID = o.Key,
                            Count = o.Count(),
                        }).ToList();
                    data.ForEach(o => o.NumberOfProduct = listCountProductCate.Where(c => c.CateID == o.Id).Select(c => c.Count).FirstOrDefault());
                    
                    /* response data */
                    result = data;
                    NSLog.Logger.Info("ResponseCateGetList", result);

                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorCateGetList", ex);
            }
            return result;
        }

        public List<CMS_CategoryViewModels> GetListProductCate()
        {
            NSLog.Logger.Info("GetListProductCate");
            var result = new List<CMS_CategoryViewModels>();
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Categories.GroupJoin(cxt.CMS_Products,
                                                            c => c.ID,
                                                            p => p.CategoryID,
                                                            (c, p) => new { c, p })
                                                  .Where(o => o.c.Status != (byte)Commons.EStatus.Deleted /*&& o.c.ProductTypeCode == (int)Commons.EProductType.Product*/)
                                                  .Select(o => new CMS_CategoryViewModels
                                                  {
                                                      Id = o.c.ID,
                                                      Name = o.c.Name,
                                                      ParentId = o.c.ParentID,
                                                      TotalProduct = o.c.CMS_Products.Count
                                                  }).ToList();
                    result = data;
                    NSLog.Logger.Info("ResponseGetListProductCate", result);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ErrorGetListProductCate", ex);
            }
            return result;
        }
    }
}
