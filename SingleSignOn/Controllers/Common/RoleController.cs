using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SingleSignOn.Repositories;
using System.Threading.Tasks;
using SingleSignOn.ViewModels;
using SingleSignOn.Utilities;
using System.Security.Claims;

namespace SingleSignOn.Controllers
{
    [DisplayName("Role Management")]
    public class RoleController : BaseController
    {
        //
        // GET: /Role/
        [MvcSiteMapNode(Title = "Role Management", Key = "rolemanagement", ParentKey = "home", PreservedRouteParameters = "id")]    
        [DisplayName("Role Management")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetList()
        {
            var result = (new RoleRepository()).GetList();
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }

        [AllowAnonymous]
        public ActionResult GetListAction(string roleId, string controlId)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(controlId))
                return null;
            var result = (new RoleRepository()).GetListAction(roleId, controlId, "SingleSignOn.Controllers");
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }

        [HttpPost]
        public ActionResult InsertRole(ActionModel model)
        {
            var repo = new RoleRepository();
            //if(string.IsNullOrEmpty (model.ActionId))
            //    return Json(new { result = "ERROR" });
            model.CreateUid = User.GetClaimValue(ClaimTypes.Sid);
            var response = repo.InsertRoleMapping(model);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });            
        }
    }
}