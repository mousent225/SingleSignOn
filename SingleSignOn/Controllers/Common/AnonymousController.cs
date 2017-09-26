﻿using SingleSignOn.Repositories;
using SingleSignOn.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SingleSignOn.Utilities;
using System.Net;

namespace SingleSignOn
{
    public class AnonymousController : Controller
    {
        //
        // GET: /Anonymous/
        public ActionResult Login()
        {
            try
            {
                LogHelper.Info("Begin login: User.Identity.IsAuthenticated: " + User.Identity.IsAuthenticated.ToString());
                
                if (User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");

                return View(new LoginModel());
            }
            catch (Exception ex)
            {
                LogHelper.Error("AnonymousController: Login " + " Exception: " + ex.InnerException.Message);
                return null;
            }
        }
        [AllowAnonymous]
        public ActionResult UpdatePassword()
        {
            ViewBag.CountEmp = (new UserRepository()).UpdatePassword();
            return View();
        }
        public string GetIpAddress()
        {
            var stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
            return stringIpAddress;
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
//                LogHelper.Info("Login model: " + model.LoginID);
                if (string.IsNullOrEmpty(model.LoginID) || string.IsNullOrEmpty(model.Password))
                {
                    ViewBag.ErrorMessage = "Please input all field!!";
                    return View(model);
                }
                var user = ValidateModel(model);
                if (user != null)
                {
                    
                    var logHistoryRepo = new LogHistoryRepository();

                    var logHistory = new LogHistoryModel()
                    {
                        UserId = model.LoginID,
                        PcBrowser = Request.Browser.Browser,
                        IpAddress = Util.IP2INT(GetIpAddress())
                    };
                    logHistoryRepo.InsertLog(logHistory);

                    var status = AppDictionary.UserStatus.FirstOrDefault(a => a.Value == user.Status.ToString()).Key;
                    if (status == "New" || status == "Reset")
                    {
                        ViewBag.Status = "ChangePassword";
                        return View(model);
                    }
                    SetAuthorized(user, model.Remember);
                    if (Session["CurUrl"] != null)
                        return Redirect(Session["CurUrl"].ToString());
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = "Login ID & Password is incorrect!";
            }
            catch (Exception ex)
            {
                LogHelper.Error("AnonymousController: UserId: " + model.LoginID + " Exception: " + ex.InnerException.Message);
                return null;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePass(UserModel model)
        {
            var repository = new UserRepository();
            var uStatus = repository.GetStatus(model.LoginID);
            var status = AppDictionary.UserStatus.FirstOrDefault(a => a.Value == uStatus).Key;

            model.PasswordOld = (status == "New" || status == "Reset" ? model.PasswordOld : ED5Helper.Encrypt(model.PasswordOld));
            model.Password = repository.GetPassword(model.LoginID);
            if (model.Password != model.PasswordOld)
                return Json(new { result = "Error", mess = "The old password is entered incorrectly" });
            model.PasswordNew = (status == "New" || status == "Reset" ? model.PasswordNew : ED5Helper.Encrypt(model.PasswordNew));

            if (model.PasswordNew == model.PasswordOld)
                return Json(new { result = "Error", mess = "The new password must have different with old password" });

            model.PasswordRepeat = (status == "New" || status == "Reset" ? model.PasswordRepeat : ED5Helper.Encrypt(model.PasswordRepeat));
            if (model.PasswordNew != model.PasswordRepeat)
                return Json(new { result = "Error", mess = "The password entered does not match" });
            model.PasswordNew = ED5Helper.Encrypt(model.PasswordNew);
            var result = repository.ChangePassword(model.LoginID, model.PasswordNew);
            return result ? Json(new { result = "OK" }) : Json(new { result = "Error", mess = "Update fail" });

        }

        [HttpGet]
        public ActionResult ChangePass()
        {
            return PartialView("_ChangePassword");
        }

        public ActionResult Logout()
        {
            try
            {
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignOut();

            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " :" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                if (Request.UrlReferrer != null) return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("Login");
        }

        #region Method - NoAction

        [NonAction]
        public UserModel ValidateModel(LoginModel model)
        {
            var rep = new UserRepository();
            var uStatus = rep.GetStatus(model.LoginID);
            var status = AppDictionary.UserStatus.FirstOrDefault(a => a.Value == uStatus).Key;
            try
            {
                model.Password = (status == "New" || status == "Reset" ? model.Password : ED5Helper.Encrypt(model.Password));
                var res = new UserRepository();
                return res.Login(model);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " Method ValidateModel:" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        [NonAction]
        public void SetAuthorized(UserModel user, bool isRemember)
        {
            try
            {
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignOut();
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.RoleId),
                    new Claim(ClaimTypes.PrimarySid, user.EmpId),
                    new Claim(ClaimTypes.Sid,user.LoginID.ToString()),
                    new Claim("DeptId",user.DeptId.ToString()),
                    new Claim("DeptName",user.DeptName)
                }, DefaultAuthenticationTypes.ApplicationCookie);

                authManager.SignIn(new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = isRemember,
                    ExpiresUtc = DateTime.UtcNow.AddHours(8)
                }, identity);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " Method:SetAuthorized:" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return;
            }
        }
        #endregion
    }
}