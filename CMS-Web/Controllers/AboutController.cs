﻿using CMS_Common;
using CMS_DTO.CMSAbout;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSEmployees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class AboutController : Controller
    {
        private CMSCategoriesFactory _facCate;
        private CMSEmployeeFactory _facEmp;
        public AboutController()
        {
            _facCate = new CMSCategoriesFactory();
            _facEmp = new CMSEmployeeFactory();
        }
        // GET: Clients/About
        public ActionResult Index()
        {
            var model = new CMS_AboutViewModels();
            try
            {
                model.Categories = _facCate.GetList().Where(x => x.ProductTypeCode == (int)Commons.EProductType.Service).OrderByDescending(x => x.CreatedDate).Skip(0).Take(3).ToList();
                model.Employees = _facEmp.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(3).ToList();
                if(model.Employees != null)
                {
                    model.Employees.ForEach(o =>
                    {
                        o.ImageURL = Commons._PublicImages + "Employees/" + o.ImageURL;
                    });
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("About Index", ex);
            }
            return View(model);
        }
    }
}