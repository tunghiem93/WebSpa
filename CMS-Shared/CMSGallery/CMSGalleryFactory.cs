using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSGallery;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSGallery
{
    public class CMSGalleryFactory
    {
        public List<CMS_GalleryModels> getList()
        {
            try
            {
                using (var _db = new CMS_Context())
                {
                    var data = _db.CMS_Products.Join(_db.CMS_ImagesLink,
                                                        p => p.ID,
                                                        i => i.ProductId,
                                                        (p, i) => new { ProductName = p.Name, Status = p.Status, i })
                                                .Where(o => !string.IsNullOrEmpty(o.i.ImageURL) && o.Status == (byte)Commons.EStatus.Actived)
                                                .Select(o => new CMS_GalleryModels
                                                {
                                                    ImageUrl = Commons._PublicImages + "Products/" + o.i.ImageURL,
                                                    ProductName = o.ProductName
                                                }).ToList();
                    NSLog.Logger.Info("getList Gallery Response :", JsonConvert.SerializeObject(data));
                    return data;
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("getList Gallery", ex);
            }
            return null;
        }
    }
}
