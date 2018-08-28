using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS_Web.Controllers
{
    public class LoginController : Controller
    {
        private CMSCustomersFactory _factory = null;
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
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

        [AllowAnonymous]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }        

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            string first_name = me.first_name;
            string last_name = me.last_name;
            string picture = me.picture.data.url;
            string fb_id = me.id;
            FormsAuthentication.SetAuthCookie(email, false);
            ClientLoginModel model = new ClientLoginModel();
            model.Email = email;
            model.FirstName = first_name;
            model.LastName = last_name;
            model.Picture = picture;
            model.Fb_ID = fb_id;

            bool IsCheck = _factory.CheckExistLoginSosial(model.Fb_ID);
            if (IsCheck)
            {
                var resultLogin = _factory.Login(model);
                if (resultLogin != null)
                {
                    UserSession userSession = new UserSession();
                    userSession.Email = resultLogin.Email;
                    userSession.UserName = resultLogin.DisplayName;
                    userSession.IsAdminClient = resultLogin.IsAdmin;
                    userSession.FirstName = resultLogin.FirstName;
                    userSession.LastName = resultLogin.LastName;
                    userSession.Phone = resultLogin.Phone;
                    userSession.Address = resultLogin.Address;
                    userSession.UserId = resultLogin.Id;
                    userSession.PostCode = resultLogin.PostCode;
                    userSession.Country = resultLogin.Country;
                    userSession.City = resultLogin.City;
                    Session.Add("UserClient", userSession);
                    string myObjectJson = JsonConvert.SerializeObject(userSession);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("UserClientCookie");
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    ModelState.AddModelError("Email", "Thông tin tài khoản không chính xác");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                CustomerModels modelFB = new CustomerModels();
                modelFB.FbID = fb_id;
                modelFB.FirstName = first_name;
                modelFB.LastName = last_name;
                modelFB.Email = email;
                modelFB.ImageURL = picture;
                string msg = "";
                string cusId = "";
                var resultSignUp = _factory.CreateOrUpdate(modelFB, ref cusId, ref msg);
                if (resultSignUp)
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
                }
                else
                {
                    ModelState.AddModelError("Email", "");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public JsonResult ForgetPassword(string Email)
        {
            var msg = "";
            try
            {
                var result = _factory.ForgotPassword(Email, ref msg);
                if (result)
                    msg = "Mật khẩu của bạn đã được cấp mới . Vui lòng kiểm tra E-mail.";

            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("ForgetPassword:", ex);
            }
            var obj = new
            {
                message = msg
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GoogleLogin(string id, string firstname, string lastname, string fullname, string email, string picture)
        {            
            FormsAuthentication.SetAuthCookie(email, false);
            ClientLoginModel model = new ClientLoginModel();
            model.Email = email;
            model.Picture = picture;
            model.Fb_ID = id;

            var obj = new
            {
                message = 1
            };
            bool IsCheck = _factory.CheckExistLoginSosial(id);
            if (IsCheck)
            {
                var resultLogin = _factory.Login(model);
                if (resultLogin != null)
                {
                    UserSession userSession = new UserSession();
                    userSession.Email = resultLogin.Email;
                    userSession.UserName = resultLogin.DisplayName;
                    userSession.IsAdminClient = resultLogin.IsAdmin;
                    userSession.FirstName = resultLogin.FirstName;
                    userSession.LastName = resultLogin.LastName;
                    userSession.Phone = resultLogin.Phone;
                    userSession.Address = resultLogin.Address;
                    userSession.UserId = resultLogin.Id;
                    userSession.PostCode = resultLogin.PostCode;
                    userSession.Country = resultLogin.Country;
                    userSession.City = resultLogin.City;
                    Session.Add("UserClient", userSession);
                    string myObjectJson = JsonConvert.SerializeObject(userSession);  //new JavaScriptSerializer().Serialize(userSession);
                    HttpCookie cookie = new HttpCookie("UserClientCookie");
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    cookie.Value = Server.UrlEncode(myObjectJson);
                    HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    obj = new { message = 2 };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                CustomerModels modelFB = new CustomerModels();
                modelFB.FbID = id;
                modelFB.FirstName = firstname;
                modelFB.LastName = lastname;
                modelFB.Email = email;
                modelFB.ImageURL = picture;
                string msg = "";
                string cusId = "";
                var resultSignUp = _factory.CreateOrUpdate(modelFB, ref cusId, ref msg);
                if (resultSignUp)
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
                }
                else
                {
                    obj = new { message = 3 };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }            
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}