using MvcSiteMapProvider;
using SingleSignOn.Approval.ViewModels;
using SingleSignOn.Repositories.Approval;
using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SingleSignOn.Controllers.Approval
{
    [KSAuthorized]
    public class ApplicationController : Controller
    {
        // GET: Application
        [MvcSiteMapNode(Title = "New Application", ParentKey = "applicationmaster", Key = "newapplication", PreservedRouteParameters = "id")]
        public ActionResult Index(string id)
        {
            var idDecrypt = Util.Decrypt(id);
            ViewBag.ApplicationId = idDecrypt;
            return View();
        }

        [AllowAnonymous]
        [MvcSiteMapNode(Title = "View Detail", ParentKey = "applicationmaster", Key = "viewdetailapplication", PreservedRouteParameters = "id")]
        public ActionResult ViewDetail(string id)
        {
            var idDecrypt = Util.Decrypt(id);
            var userId = User.GetClaimValue(ClaimTypes.Sid);
            var applicationMaster = (new ApplicationMasterRepository().GetInfor(int.Parse(idDecrypt.Split('_')[0]), userId));
            ViewBag.MasterId = int.Parse(idDecrypt.Split('_')[0]);
            ViewBag.ApplicationId = int.Parse(idDecrypt.Split('_')[1]);
            ViewBag.ApplicationMasterName = applicationMaster.ApplicantName;
            return View(applicationMaster);
        }
        [AllowAnonymous]
        public ActionResult ShowApplicationDetail(string id)
        {
            var rep = new ApplicationConfigRepository();
            var item = rep.GetInfor(int.Parse(id));
            var partialView = "";
            ViewBag.ApplicationMasterName = item.Name;
            switch (id)
            {
                case "1"://application for system role
                    partialView = "SystemRole";
                    break;
                case "4"://application for email request
                    partialView = "EmailRequest";
                    break;
                case "5"://application for it equipment
                    partialView = "ItEquipment";
                    break;
                case "6"://application for information system
                    partialView = "InformationSystem";
                    break;
                case "7"://application for NetClient Policy
                    partialView = "NetClientPolicy";
                    break;
                default:
                    break;
            }
            return PartialView(partialView);
        }
        [AllowAnonymous]
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult ShowApplicationDetailForEdit(string id)
        {
            var rep = new ApplicationConfigRepository();
            var item = rep.GetInfor(int.Parse(id));
            var partialView = "";
            ViewBag.ApplicationMasterName = item.Name;
            switch (id)
            {
                case "1"://application for system role
                    partialView = "SystemRoleDetail";
                    break;
                case "4"://application for system role
                    partialView = "EmailRequestDetail";
                    break;
                case "5"://application for Email Request
                    partialView = "ItEquipmentDetail";
                    break;
                case "6"://application for information system
                    partialView = "InformationSystemDetail";
                    break;
                case "7"://application for NetClient Policy
                    partialView = "NetClientPolicyDetail";
                    break;
                default:
                    break;
            }
            return PartialView(partialView);
        }

        #region SystemRole
        [AllowAnonymous]
        public ActionResult SystemRoleDetail(int masterId)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.GetInforSystemRole(masterId);
            return Json(response);
        }
        [AllowAnonymous]
        public ActionResult SystemRoleInsert(List<SystemRoleModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.SystemRoleInsert(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult SystemRoleUpdate(List<SystemRoleModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.SystemRoleUpdate(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

        #region EmailRequest
        [AllowAnonymous]
        public ActionResult EmailRequestDetail(int masterId)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.GetInforEmailRequest(masterId);
            return Json(response);
        }
        [AllowAnonymous]
        public ActionResult EmailRequestInsert(List<EmailRequestModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.EmailRequestInsert(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult EmailRequestUpdate(List<EmailRequestModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.EmailRequestUpdate(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

        #region ItEquipment
        [AllowAnonymous]
        public ActionResult ItEquipmentDetail(int masterId)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.GetInforItEquipment(masterId);
            return Json(response);
        }
        [AllowAnonymous]
        public ActionResult ItEquipmentInsert(List<ItEquipmentModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.ItEquipmentInsert(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult ItEquipmentUpdate(List<ItEquipmentModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.ItEquipmentUpdate(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

        #region Information System
        [AllowAnonymous]
        public ActionResult InformationSystemDetail(int masterId)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.GetInforInformationSystem(masterId);
            return Json(response);
        }
        [AllowAnonymous]
        public ActionResult InformationSystemInsert(InformationSystemModel models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.InformationSystemInsert(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult InformationSystemUpdate(InformationSystemModel models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.InformationSystemUpdate(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult InformationSystemUpdateApprove(InformationSystemModel models)
        {
            models.EmpIp = User.GetClaimValue(ClaimTypes.Sid);
            var rep = new ApplicationMasterRepository();
            var response = rep.InformationSystemUpdateApprove(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

        #region NetClientPolicy
        [AllowAnonymous]
        public ActionResult NetClientPolicyDetail(int masterId)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.GetInforNetClientPolicy(masterId);
            return Json(response);
        }
        [AllowAnonymous]
        public ActionResult NetClientPolicyInsert(List<NetClientPolicyModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.NetClientPolicyInsert(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        [AllowAnonymous]
        public ActionResult NetClientPolicyUpdate(List<NetClientPolicyModel> models)
        {
            var rep = new ApplicationMasterRepository();
            var response = rep.NetClientPolicyUpdate(models);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
        }
        #endregion

    }
}