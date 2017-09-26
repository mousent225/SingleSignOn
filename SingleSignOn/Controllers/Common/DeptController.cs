using SingleSignOn.Repositories.Common;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SingleSignOn.Controllers
{
    public class DeptController : Controller
    {
        // GET: Dept
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [OutputCache(Duration = 3600, VaryByParam = "none",Location = OutputCacheLocation.Client)]
        public JsonResult GetDeptTreeView()
        {
            var res = new DeptRepository();
            var result = res.GetDeptTreeView();
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }
        [AllowAnonymous]
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public JsonResult GetDeptTreeViewFilter()
        {
            var res = new DeptRepository();
            var result = res.GetDeptTreeView();
            result.Insert(0, new DeptModel()
            {
                Code = "",
                Children = null,
                DelFlag = false,
                DeptCode = "",
                EnName = "Select One",
                HasChildren = false,
                Id = ""
            });
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }
        [AllowAnonymous]
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public JsonResult GetId(int id)
        {
            var res = new DeptRepository();
            var result = res.GetId(id);
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : Json("", JsonRequestBehavior.AllowGet);
        }

    }
}