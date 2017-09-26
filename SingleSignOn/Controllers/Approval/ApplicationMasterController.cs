using MvcSiteMapProvider;
using SingleSignOn.Approval.ViewModels;
using SingleSignOn.Repositories;
using SingleSignOn.Repositories.Approval;
using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SingleSignOn.Controllers.Approval
{
    [KSAuthorized]
    public class ApplicationMasterController : Controller
    {
        #region Application Master
        // GET: ApplicationMaster
        [MvcSiteMapNode(Title = "Application Master", ParentKey = "home", Key = "applicationmaster")]
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
            var repository = new ApplicationMasterRepository();
            var list = repository.GetAllMaster(string.IsNullOrEmpty(name) ? null : name, deptId, string.IsNullOrEmpty(kind) ? null : kind, fromDate, toDate, empId);
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
        public int ApplicationMasterInsert(ApplicationMasterModel model)
        {
            model.CreateDate = DateTime.Now;
            model.CreateUid = User.GetClaimValue(ClaimTypes.Sid);
            var rep = new ApplicationMasterRepository();
            return rep.AppicationMasterInsert(model);             
        }

        [AllowAnonymous]
        public JsonResult ApplicationMasterUpdate(ApplicationMasterModel model)
        {
            model.UpdateDate = DateTime.Now;
            model.UpdateUId = User.GetClaimValue(ClaimTypes.Sid);
            var rep = new ApplicationMasterRepository();
            var result = rep.AppicationMasterUpdate(model);
            return Json(result ? "OK" : "Error");
        }

        [AllowAnonymous]
        public JsonResult ApplicationDelete(int id)
        {
            var userId = User.GetClaimValue(ClaimTypes.Sid);
            var rep = new ApplicationMasterRepository();
            var result = rep.ApplicationDelete(id, userId);
            return Json( result ? "OK" : "Error" );
        }

        [AllowAnonymous]
        public JsonResult ApplicationConfirm(int id, string linkApplication)
        {
            var rep = new ApplicationMasterRepository();
            var result = rep.ApplicationConfirm(id, linkApplication);
            return Json(result ? "OK" : "Error");
        }

        [AllowAnonymous]
        public JsonResult ApplicationRecallConfirm(int id)
        {
            var rep = new ApplicationMasterRepository();
            var result = rep.ApplicationRecallConfirm(id);
            return Json(result ? "OK" : "Error");
        }

        #endregion

        #region ApprovalLine
        [AllowAnonymous]
        public ActionResult ApprovalHistoryGetList(int applicationId, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var rep = new ApplicationMasterRepository();
            var list = rep.ApprovalHistoryGetList(User.GetClaimValue(ClaimTypes.Sid), applicationId);
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
        public ActionResult ApprovalLineGetList(int masterId, int applicationId, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var rep = new ApplicationMasterRepository();
            var list = rep.ApprovalLineGetList(masterId, applicationId);
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
        public JsonResult ApproveApplication(ApprovalModel model)
        {
            var rep = new ApplicationMasterRepository();
            var result = rep.ApproveApplication(model);
            return Json(result ? "OK" : "Error");
        }
        [AllowAnonymous]
        public JsonResult RecallApplication(int masterId, int applicationId)
        {
            var rep = new ApplicationMasterRepository();
            var result = rep.RecallApplication(masterId, applicationId, User.GetClaimValue(ClaimTypes.Sid));
            return Json(result ? "OK" : "Error");
        }

        #endregion

    }
}