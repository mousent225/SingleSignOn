
using System.Web.Mvc;
using System.Security.Claims;
using SingleSignOn.Utilities;
using SingleSignOn.Repositories;
using System.Linq;
using MvcSiteMapProvider;
using SingleSignOn.ViewModels;
using System;
using System.ComponentModel;

namespace SingleSignOn.Controllers
{
    [DisplayName("FAQ")]
    public class FaqController : BaseController
    {
        // GET: Faq
        [MvcSiteMapNode(Title = "FAQ", ParentKey = "home", Key = "faq")]
        public ActionResult Index()
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));
            return View();
        }

        [MvcSiteMapNode(Title = "Insert FAQ", ParentKey = "faq", Key = "insertfaq")]
        public ActionResult Insert()
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));
            var model = new PostsModel() { IsActive = true };
            return View(model);
        }

        [AllowAnonymous]
        [MvcSiteMapNode(Title = "View Detail FAQ", ParentKey = "faq", Key = "viewdetailfaq", PreservedRouteParameters = "id")]
        public ActionResult ViewDetail(int id)
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));

            var empId = User.GetClaimValue(ClaimTypes.PrimarySid);
           
            var repository = new PostsRepository();
            var item = repository.GetAll(null, null, null, empId, id, null, null).FirstOrDefault();
            return View(item);
            
        }

        [MvcSiteMapNode(Title = "Edit FAQ", ParentKey = "faq", Key = "editfaq")]
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var empId = User.GetClaimValue(ClaimTypes.PrimarySid);

            var repository = new PostsRepository();
            var item = repository.GetAll(null, null, null, empId, id, null, null).FirstOrDefault();
            return View(item);
        }

        [AllowAnonymous]
        public ActionResult GetAll(string subject, string category, string kind, int id, string dateFrom, string dateTo, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : (new DateTime(int.Parse(dateFrom.Split('.')[0]), int.Parse(dateFrom.Split('.')[1]), int.Parse(dateFrom.Split('.')[2])));
            var toDate = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : ((new DateTime(int.Parse(dateTo.Split('.')[0]), int.Parse(dateTo.Split('.')[1]), int.Parse(dateTo.Split('.')[2]))));

            string empId = User.GetClaimValue(ClaimTypes.PrimarySid);
            var repository = new PostsRepository();
            var list = repository.GetAll(string.IsNullOrEmpty(subject) ? null : subject, string.IsNullOrEmpty(category) ? null : category, string.IsNullOrEmpty(kind) ? null : kind, empId, id, fromDate, toDate);
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

        [HttpPost]
        public string Insert(PostsModel model)
        {
            //model.Id = Guid.NewGuid();
            model.CreateUid = User.GetClaimValue(ClaimTypes.PrimarySid);
            model.CreateDate = System.DateTime.Now;

            if ((new PostsRepository()).InsertFaq(model))
                return "OK";
            return "Error";
            //ModelState.AddModelError("InsertError", "Add new user faile! Pls contact to admin!");
            //MessageHelper.ShowMessage(this, "Error", "Insert fail! Pls contact to admin!", NotificationStyle.Error);
            //return View();
        }

        [AllowAnonymous]
        public string GetContent(int postId)
        {
            var rep = new PostsRepository();
            return rep.GetContent(postId);
        }

        [AllowAnonymous]
        public string Update(PostsModel model)
        {
            //model.Id = Guid.NewGuid();
            model.CreateUid = User.GetClaimValue(ClaimTypes.PrimarySid);
            model.CreateDate = System.DateTime.Now;

            if ((new PostsRepository()).UpdateFaq(model))
                return "OK";
            return "Error";
        }

        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            var model = new PostsModel()
            {
                PostId = id,
                UpdateUid = User.GetClaimValue(ClaimTypes.PrimarySid),
                UpdateDate = DateTime.Now
            };
            var repository = new PostsRepository();
            return Json(repository.Delete(model) ? new { result = "OK" } : new { result = "ERROR" });
        }

        [AllowAnonymous]
        public ActionResult GetPlant()
        {
            var list = (new PostsRepository()).GetPlant().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}