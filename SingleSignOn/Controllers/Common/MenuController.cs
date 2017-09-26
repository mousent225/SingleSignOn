using SingleSignOn.Repositories;
using SingleSignOn.ViewModels;
using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using MvcSiteMapProvider;

namespace SingleSignOn.Controllers
{
    public class MenuController : BaseController
    {
        MenuRepository res = new MenuRepository();
        // GET: /Menu/
        [MvcSiteMapNode(Title = "Menu", ParentKey = "home", Key = "menu")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                var result = false;
                model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
                if (model.ID == null)
                {
                     result = res.InsertMenu(model);
                }
                else
                {
                    result = res.UpdateMenu(model);
                }
                if (result)
                {
                    MessageHelper.ShowMessage(this, "Process successful!");
                    return RedirectToAction("Index");
                }
                else
                {
                    MessageHelper.ShowMessage(this, "Process faile! Pls contact to admin!");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult GetMenuTreeView(string id)
        {
            var result = res.GetMenuTreeView(id);
            if (result.Any())
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [AllowAnonymous]
        public JsonResult GetListValues(string id)
        {
            var caRes = new CategoryRepository();
            var result = caRes.GetListValues(id, "", "");
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }

        [AllowAnonymous]
        public JsonResult GetMenu(string id)
        {
            var result = res.GetMenu(id);
            return result.ID != null ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }

        [AllowAnonymous]
        [HttpPost]
        public string InsertMenu(MenuModel model)
        {
            var result = res.InsertMenu(model);
            if (result)
            {
                return "OK";
            }
            else
            {
                return "Error";
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public string UpdateMenu(MenuModel model)
        {
            var result = res.UpdateMenu(model);
            if (result)
            {
                return "OK";
            }
            else
            {
                return "Error";
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult DeleteMenu(string id)
        {
            var result = res.DeleteMenu(id);
            if (result)
            {
                return Json(new { result = "OK" });
            }
            else
            {
                return Json(new { result = "Error" });
            }
        }
	}
}