using SingleSignOn.Repositories;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security.Claims;
using SingleSignOn.Models;

namespace SingleSignOn.Controllers
{
    public class AuthorizationMenuController : BaseController
    {
        //AuthorizationMenuRepository res = new AuthorizationMenuRepository();
        [MvcSiteMapNode(Title = "Set Authorization Menu", Key = "setAuthorizationMenu", ParentKey = "user", PreservedRouteParameters = "id")]
        // GET: /AuthorizationMenu/
        public ActionResult Create(string id)
        {
            ViewBag.Owner = id;
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Create(AuthorizationMenuModel model)
        //{
        //    try
        //    {
        //        var arr = model.MenuID.ToString().Split(',');
        //        var listmodel = new List<SysAuthorization>();
        //        for (var i = 0; i < arr.Count(); i++)
        //        {
        //            listmodel.Add(new SysAuthorization()
        //            {
        //                ID = Guid.NewGuid(),
        //                MasterMenu = new Guid(model.MasterMenu),
        //                MenuID = new Guid(arr[i]),
        //                Owner = model.Owner,
        //                CreateDate = DateTime.Now,
        //                CreateUID = new Guid(User.GetClaimValue(ClaimTypes.Sid))
        //            });
        //        }
        //        var delete = res.DeleteAuthorizationMenu(model.MasterMenu, model.Owner.ToString());
        //        if (listmodel.Count > 0 && delete)
        //        {
        //            var result = res.InsertAuthorizationMenu(listmodel);
        //            if (result)
        //            {
        //                MessageHelper.ShowMessage(this, "Process successful!");
        //                return RedirectToAction("Index", "User");
        //            }
        //            else
        //            {
        //                MessageHelper.ShowMessage(this, "Process faile!");
        //                return RedirectToAction("Create", "AuthorizationMenu");
        //            }
        //        }
        //        else
        //        {
        //            MessageHelper.ShowMessage(this, "Process faile!");
        //            return RedirectToAction("Create", "AuthorizationMenu");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("SaveAuthorizationMenu " + ex.Message);
        //        MessageHelper.ShowMessage(this, "Process faile!");
        //        return RedirectToAction("Create", "AuthorizationMenu");
        //    }
        //}

        //[AllowAnonymous]
        //public JsonResult GetMenuTreeViewAuthorization(string id, string user)
        //{
        //    var result = res.GetMenuTreeViewAuthorization(id, user);
        //    return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        //}
    }
}