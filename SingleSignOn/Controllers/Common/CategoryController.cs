using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SingleSignOn.Repositories;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;

namespace SingleSignOn.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryRepository res = new CategoryRepository();
        //
        // GET: /Category/
        [MvcSiteMapNode(Title = "Category", ParentKey = "home", Key = "category", PreservedRouteParameters = "id")]
        public ActionResult Index(string id)
        {
            System.Web.HttpContext.Current.Session["sessionLeftMenuId"] = id;
            return View();
        }

        [MvcSiteMapNode(Title = "Insert Category", ParentKey = "category")]
        public ActionResult Create()
        {
            return View();
        }

        [MvcSiteMapNode(Title = "Edit Category", ParentKey = "category", PreservedRouteParameters = "id")]
        public ActionResult EditCate(string id)
        {
            var model = res.GetCategory(id);
            return View(model);
        }


        [HttpPost]
        public ActionResult InsertCate(CategoryModel model)
        {
            model.ID = Guid.NewGuid();
            model.CreateDate = DateTime.Now;
            model.CreateUID = User.GetClaimValue(ClaimTypes.Sid);

            if (res.CheckExist(model.Name)) return Json(new {result = "Exist"});

            var response = res.InsertCategory(model);
            return Json(response ? new { result = model.ID.ToString() } : new { result = "ERROR" });

            //ModelState.AddModelError("InsertError", "Add new category fail! Pls contact to admin!");
            //return View(); MessageHelper.ShowMessage(this, "Add new category successfull!");
            //    return RedirectToAction("Edit/" + model.ID);
        }

        [HttpPost]
        public ActionResult EditCate(CategoryModel model)
        {
            if (!ModelState.IsValid) return View();
            model.ModifyDate = DateTime.Now;
            model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
            var response = res.UpdateCategory(model);
            if (response)
            {
                MessageHelper.ShowMessage(this, "Update category successfull!");
                if (model.ISCONTINUE)
                    return View(model);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("UpdatetError", "Update category fail! Pls contact to admin!");
            return View();
        }

        public ActionResult DeleteCate(string id)
        {
            var model = new CategoryModel
            {
                ID = new Guid(id),
                DeleteDate = DateTime.Now,
                DeleteUID = User.GetClaimValue(ClaimTypes.Sid),
                Deleted = true
            };
            var response = res.DeleteCategory(model);
            return Json(response ? new { result = "OK" } : new { result = "ERROR" });
            //if (response)
            //{
            //    MessageHelper.ShowMessage(this, "Delete category successfull!");
            //    return RedirectToAction("Index");
            //}
            //ModelState.AddModelError("DeleteError", "Delete category fail! Pls contact to admin!");
            //return View();
        }

        [AllowAnonymous]
        public ActionResult GetListCategory(string name, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var list = res.GetListCategory(name);
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

        #region CategoryValue
        [AllowAnonymous]
        public ActionResult GetListCategoryValue(string category, string name, string status, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var list = res.GetListValues(category, name, status);
            if (list == null) return Json("", JsonRequestBehavior.AllowGet);
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
        public ActionResult GetListValueViaCate(string category, bool isFilter)
        {
            var list = res.GetListValues(category, "", "1").ToList();
            if (isFilter) list.Insert(0, new CategoryValueModel() { ID = Guid.Empty, Name = "All" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetListTreeView(string category)
        {
            var list = res.GetListTreeView(category, User.GetClaimValue(ClaimTypes.Role)).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowModalCreateCategoryValue(string id)
        {
            var model = new CategoryValueModel() { Category = new Guid(id) };
            return PartialView("CreateCategoryValue", model);
        }
        public ActionResult ShowModalEditCategoryValue(string id)
        {
            var model = res.GetCategoryValue(id);
            return PartialView("EditCategoryValue", model);
        }

        [AllowAnonymous]
        public string CheckExistCateValue(CategoryValueModel model)
        {
            if (model == null) return "Model is null";
            return res.CheckExistCateValue(model.Category.ToString(), model.Name) ? "Name is exists!" : "OK";
        }

        [AllowAnonymous]
        public JsonResult GetListViaParent(string id, string parent = "")
        {
            var result = res.GetListViaParent(id, parent);//
            return result.Any() ? Json(result, JsonRequestBehavior.AllowGet) : null;
        }

        [AllowAnonymous]
        public JsonResult InsertCategoryValue(CategoryValueModel model)
        {
            model.CreateDate = DateTime.Now;
            model.CreateUID = User.GetClaimValue(ClaimTypes.Sid);
            var result = res.InsertCategoryValue(model);
            return Json(result ? new { result = "OK" } : new { result = "Error" });
        }

        [AllowAnonymous]
        public JsonResult UpdateCategoryValue(CategoryValueModel model)
        {
            model.ModifyDate = DateTime.Now;
            model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
            var result = res.UpdateCategoryValue(model);
            return Json(result ? new { result = "OK" } : new { result = "Error" });
        }

        [AllowAnonymous]
        public JsonResult DeleteCategoryValue(string id)
        {
            var model = new CategoryValueModel
            {
                ID = new Guid(id),
                DeleteDate = DateTime.Now,
                DeleteUID = User.GetClaimValue(ClaimTypes.Sid)
            };
            var result = res.DeleteCategoryValue(model);
            return Json(result ? new { result = "OK" } : new { result = "Error" });
        }
        #endregion
    }
}