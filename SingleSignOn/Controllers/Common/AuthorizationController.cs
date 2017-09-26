using SingleSignOn.Repositories;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using MvcSiteMapProvider;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SingleSignOn.Controllers
{
    public class AuthorizationController : Controller
    {
        AuthorizationRepository res = new AuthorizationRepository();
        [MvcSiteMapNode(Title = "Set Authorization", Key = "setAuthorization", ParentKey = "user", PreservedRouteParameters = "id")]        
        public ActionResult Create(string id)
        {
            ViewBag.CreateUser = User.GetClaimValue(ClaimTypes.Sid);
            ViewBag.Owner = id;
            return View();
        }

        #region AllowAnonymous
        [AllowAnonymous]
        public JsonResult GetController()
        {
            var listControler = res.GetControler("SingleSignOn.Controllers");
            return listControler.Any() ? Json(listControler, JsonRequestBehavior.AllowGet) : null;
        }

        //[AllowAnonymous]
        //public string GetRights(string controllerId, string roleId )
        //{
        //    return (new AuthorizationRepository()).GetRights(controllerId, roleId);
        //}

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SaveAuthorization(AuthorizationModel model)
        {
            try
            {
                if (model.ListAuthorization.Count != 0)
                {
                    string id = "";
                    foreach (var item in model.ListAuthorization)
                    {
                        
                            item.IsAllowed = true;
                            item.CreateDate = DateTime.Now;
                            item.CreateUid = item.ModifyUid;
                            item.ModifyDate = DateTime.Now;
                        id = item.Owner.ToString();
                    }
                    //xóa hết mấy cái cũ trong data
                    var delete = res.DeleteAuthorization(id);
                    if (delete)
                    {
                        //thêm mới
                        var result = res.InsertAuthorization(model.ListAuthorization);
                        if (result)
                        {
                            return Json(new { result = "OK" });
                        }
                        else
                        {
                            return Json(new { result = "Error" });
                        }
                    }
                    else
                    {
                        LogHelper.Error("PUT: InsertAuthorization...Delete DeleteAuthorization");
                        return Json(new { result = "Error" });
                    }
                }
                else
                {
                    var delete = res.DeleteAuthorization(model.Owner);
                    if (delete)
                    {
                        return Json(new { result = "OK" });
                    }
                    else
                    {
                        LogHelper.Error("PUT: InsertAuthorization...Delete DeleteAuthorization");
                        return Json(new { result = "Error" });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PUT: InsertAuthorization " + ex.Message);
                return Json(new { result = "Error" });
            }
        }
        #endregion
    }
}