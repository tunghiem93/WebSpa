using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS_Web.Controllers
{
    public class LoginController : Controller
    {
        private CMSCustomersFactory _factory = null;
        public LoginController()
        {
            _factory = new CMSCustomersFactory();
        }
        // GET: ClientSite/Login
        public ActionResult SignIn()
        {
            ClientLoginModel model = new ClientLoginModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            try
            {
                HttpCookie cookie = new HttpCookie("UserClientCookie");
                HttpContext.Response.Cookies.Remove("UserClientCookie");
                HttpContext.Response.SetCookie(cookie);

                if (Session["UserClient"] == null)
                    return RedirectToAction("Index", "Home");

                FormsAuthentication.SignOut();
                Session.Remove("UserClient");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Logout ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SignIn(ClientLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Password = CommonHelper.Encrypt(model.Password);
            var result = _factory.Login(model);
            if (result != null)
            {
                UserSession userSession = new UserSession();
                userSession.Email = result.Email;
                userSession.UserName = result.DisplayName;
                userSession.IsAdminClient = result.IsAdmin;
                userSession.FirstName = result.FirstName;
                userSession.LastName = result.LastName;
                userSession.Phone = result.Phone;
                userSession.Address = result.Address;
                userSession.UserId = result.Id;
                userSession.PostCode = result.PostCode;
                userSession.Country = result.Country;
                userSession.City = result.City;
                Session.Add("UserClient", userSession);
                string myObjectJson = JsonConvert.SerializeObject(userSession);  //new JavaScriptSerializer().Serialize(userSession);
                HttpCookie cookie = new HttpCookie("UserClientCookie");
                cookie.Expires = DateTime.Now.AddMonths(1);
                cookie.Value = Server.UrlEncode(myObjectJson);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("Email", "Thông tin tài khoản không chính xác");
                return View(model);
            }
        }

        public ActionResult SignUp()
        {
            CustomerModels model = new CustomerModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp(CustomerModels model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ConfirmPassword) && !model.Password.Equals(model.ConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Xác nhận mật khẩu không chính xác !");

                if (!ModelState.IsValid)
                    return View(model);
                model.Password = CommonHelper.Encrypt(model.Password);
                string msg = "";
                string cusId = "";
                var result = _factory.CreateOrUpdate(model, ref cusId, ref msg);
                if (result)
                {
                    var data = _factory.GetDetail(cusId);
                    UserSession userSession = new UserSession();
                    userSession.Email = data.Email;
                    userSession.UserName = data.FirstName + " " + data.LastName;
                    userSession.FirstName = data.FirstName;
                    userSession.LastName = data.LastName;
                    userSession.Phone = data.Phone;
                    userSession.Address = data.Address;
                    userSession.UserId = data.ID;
                    userSession.PostCode = data.Postcode;
                    userSession.Country = data.Country;
                    userSession.City = data.City;
                    Session.Add("UserClient", userSession);
                    string myObjectJson = JsonConvert.SerializeObject(userSession);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("UserClientCookie");
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", msg);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("SignUp", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        public ActionResult Profile(string q)
        {
            var model = new CustomerModels();
            try
            {
                NSLog.Logger.Info("Profile_Request:", q);
                model = _factory.GetDetail(q);
                if(model != null)
                {
                    model.Password = CommonHelper.Decrypt(model.Password);
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Profile_Error:", ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(CustomerModels model)
        {
            try
            {
                NSLog.Logger.Info("Profile_Request:", model);
                var CusId = string.Empty;
                var msg = string.Empty;
                model.Password = CommonHelper.Encrypt(model.Password);
                var result = _factory.CreateOrUpdate(model,ref CusId,ref msg);
                if(result)
                {
                    model.IsShow = true;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Profile_Error:", ex);
            }
            return View(model);
        }
    }
}