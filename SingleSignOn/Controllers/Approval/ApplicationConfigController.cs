using MvcSiteMapProvider;
using SingleSignOn.Approval.ViewModels;
using SingleSignOn.Repositories;
using SingleSignOn.Repositories.Approval;
using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SingleSignOn.Controllers.Approval
{
    [KSAuthorized]
    [DisplayName("Application Config")]
    public class ApplicationConfigController : Controller
    {
        // GET: ApprovalLineConfig
        #region Application Config
        [MvcSiteMapNode(Title = "Application Configuration", ParentKey = "home", Key = "applicationconfig")]
        public ActionResult Index()
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetAll(string name, int deptId, string kind, string dateFrom, string dateTo, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            //string name, int deptId, string kind, DateTime? dateFrom, DateTime? dateTo
            var fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : (new DateTime(int.Parse(dateFrom.Split('.')[0]), int.Parse(dateFrom.Split('.')[1]), int.Parse(dateFrom.Split('.')[2])));
            var toDate = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : ((new DateTime(int.Parse(dateTo.Split('.')[0]), int.Parse(dateTo.Split('.')[1]), int.Parse(dateTo.Split('.')[2]))));

            string empId = User.GetClaimValue(ClaimTypes.PrimarySid);
            var repository = new ApplicationConfigRepository();
            var list = repository.GetAll(string.IsNullOrEmpty(name) ? null : name, deptId, string.IsNullOrEmpty(kind) ? null : kind, fromDate, toDate);
            var total = list.Count();
            if (!string.IsNullOrEmpty(sortorder))
            {
                list = sortorder == "asc" ? list.OrderBy(o => o.GetType().GetProperty(sortdatafield).GetValue(o, null)) :
                                            list.OrderByDescending(o => o.GetType().GetProperty(sortdatafield).GetValue(o, null));
            }
            list = list.Skip(pagesize * pagenum).Take(pagesize);
            var result = new
            {
                TotalRows = total,
                Rows = list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetListApplication()
        {
            var res = new ApplicationConfigRepository();
            var list = res.GetListApplication().ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }        
        [HttpPost]
        public ActionResult Insert(ApplicationConfigModel model)
        {
            model.CreateUid = User.GetClaimValue(ClaimTypes.PrimarySid);
            var rep = new ApplicationConfigRepository();
            var response = rep.InsertApplicationConfig(model);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Update(ApplicationConfigModel model)
        {
            model.CreateUid = User.GetClaimValue(ClaimTypes.PrimarySid);
            var rep = new ApplicationConfigRepository();
            var response = rep.UpdateApplicationConfig(model);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Delete(ApplicationConfigModel model)
        {
            var rep = new ApplicationConfigRepository();
            var response = rep.DeleteApplicationConfig(model.Id, User.GetClaimValue(ClaimTypes.PrimarySid));
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

        #region Common
        [AllowAnonymous]
        public ActionResult GetDefaultApprovalLine(int applicationId)
        {
            var response = (new ApplicationConfigRepository()).GetDefaultApproval(applicationId);
            return Json(new { data = response });
        }

        //[OutputCache(Duration = 1800, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        [AllowAnonymous]
        public ActionResult ShowModalConfig(string id)
        {
            return PartialView("ApprovalLine");
        }
        #endregion
    }
}