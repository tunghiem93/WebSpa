using CMS_Common;
using CMS_DTO.CMSBase;
using CMS_DTO.CMSSession;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSCustomers;
using CMS_Shared.CMSEmployees;
using CMS_Shared.CMSRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class HQController : Controller
    {
        public UserSession CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["User"] != null)
                    return (UserSession)System.Web.HttpContext.Current.Session["User"];
                else
                    return new UserSession();
            }
        }

        public List<SelectListItem> GetListCategorySelectItem(int productType = 0)
        {
            var _factory = new CMSCategoriesFactory();
            List<SelectListItem> data = null;

            var listCate = _factory.GetList(productType);
            if (listCate != null)
            {
                data = listCate.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.CategoryName,
                }).ToList();
            }
            return data;
        }

        public List<SelectListItem> GetListCateSelectItem()
        {
            var _factory = new CMSCategoriesFactory();
            List<SelectListItem> data = null;

            var listCate = _factory.GetList();
            if (listCate != null)
            {
                data = listCate.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.CategoryName,
                }).ToList();
            }
            return data;
        }

        public List<SelectListItem> GetListEmployeeSelectItem()
        {
            var _factory = new CMSEmployeeFactory();
            List<SelectListItem> data = null;

            var listEmp = _factory.GetList();
            if (listEmp != null)
            {
                data = listEmp.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name,
                }).ToList();
            }
            return data;
        }

        public List<SelectListItem> GetListRoleSelectItem()
        {
            var _factory = new CMSRoleFactory();
            List<SelectListItem> data = null;

            var listCate = _factory.GetList();
            if (listCate != null)
            {
                data = listCate.Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name,
                }).ToList();
            }
            return data;
        }

        public List<CategoryByCategory> GetListCategory()
        {
            var _factory = new CMSCategoriesFactory();
            //var data = _factory.GetList().Where(x => !string.IsNullOrEmpty(x.ParentId)).Select(x => new SelectListItem
            //{
            //    Value = x.Id,
            //    Text = x.CategoryName,
            //}).ToList();
            //return data;
            var models = new List<CategoryByCategory>();
            var data = _factory.GetList();
            if (data != null)
            {
                var groupCate = data.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
                if (groupCate != null)
                {
                    groupCate.ForEach(x =>
                    {
                        var model = new CategoryByCategory();
                        model.id = x.Id;
                        model.text = x.CategoryName.ToUpper();
                        model.children = data.Where(y => !string.IsNullOrEmpty(y.ParentId) && y.ParentId.Equals(x.Id))
                                                .Select(z => new CategoryChildren
                                                {
                                                    id = z.Id,
                                                    text = z.CategoryName
                                                }).ToList();
                        models.Add(model);
                    });
                }
            }

            return models;
        }

        public List<SelectListItem> GetListBlogType()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                 new SelectListItem() {Text = "Hình ảnh", Value= Commons.EBlogType.Image.ToString("d") },
                 new SelectListItem() {Text = "Iframe(Video-Mp3)", Value= Commons.EBlogType.Iframe.ToString("d") },
                 //new SelectListItem() {Text = "Video", Value= Commons.EBlogType.Video.ToString("d") },
                 //new SelectListItem() {Text = "Audio", Value= Commons.EBlogType.Audio.ToString("d") },
            };
            return data;
        }

        public List<SelectListItem> GetListExpenseType()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                 new SelectListItem() {Text = "Dịch vụ hệ thống", Value= Commons.EExpenseType.System.ToString("d") },
                 new SelectListItem() {Text = "Dịch vụ ngoài", Value= Commons.EExpenseType.Service.ToString("d") },
            };
            return data;
        }

        public List<SelectListItem> GetListCustomer()
        {
            var _factory = new CMSCustomersFactory();
            List<SelectListItem> data = null;
            var listCus = _factory.GetList();
            if (listCus != null)
            {
                data = listCus.Select(x => new SelectListItem
                {
                    Value = x.ID,
                    Text = x.Name,
                }).ToList();
            }
            return data;
        }

        public List<SelectListItem> GetListFromTime()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                 new SelectListItem() {Text = "9 am", Value= "9" },
                 new SelectListItem() {Text = "10 am", Value= "10" },
                 new SelectListItem() {Text = "11 am", Value= "11" },
                 new SelectListItem() {Text = "12 pm", Value= "12" },
                 new SelectListItem() {Text = "1 pm", Value= "13" },
                 new SelectListItem() {Text = "2 pm", Value= "14" },
                 new SelectListItem() {Text = "3 pm", Value= "15" },
                 new SelectListItem() {Text = "4 pm", Value= "16" },
                 new SelectListItem() {Text = "5 pm", Value= "17" },
                 new SelectListItem() {Text = "6 pm", Value= "18" },
                 new SelectListItem() {Text = "7 pm", Value= "19" },
            };
            return data;
        }

        public List<SelectListItem> GetListToTime()
        {
            List<SelectListItem> data = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "9 am", Value= "9" },
                 new SelectListItem() {Text = "10 am", Value= "10" },
                 new SelectListItem() {Text = "11 am", Value= "11" },
                 new SelectListItem() {Text = "12 pm", Value= "12" },
                 new SelectListItem() {Text = "1 pm", Value= "13" },
                 new SelectListItem() {Text = "2 pm", Value= "14" },
                 new SelectListItem() {Text = "3 pm", Value= "15" },
                 new SelectListItem() {Text = "4 pm", Value= "16" },
                 new SelectListItem() {Text = "5 pm", Value= "17" },
                 new SelectListItem() {Text = "6 pm", Value= "18" },
                 new SelectListItem() {Text = "7 pm", Value= "19" },
            };
            return data;
        }
    }
}