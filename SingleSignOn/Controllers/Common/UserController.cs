using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SingleSignOn.Repositories;
using SingleSignOn.ViewModels;
using SingleSignOn.Utilities;
using System.Web.UI;

namespace SingleSignOn.Controllers
{
    public class UserController : Controller
    {
        UserRepository res = new UserRepository();
        //
        // GET: /User/
        [MvcSiteMapNode(Title = "User", ParentKey = "home", Key = "user", PreservedRouteParameters = "id")]
        public ActionResult Index(string id)
        {
            System.Web.HttpContext.Current.Session["sessionLeftMenuId"] = id;
            return View();
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Create", ParentKey = "user")]
        public ActionResult Create()
        {
            return View();
        }

        //[AllowAnonymous]
        //public ActionResult DelCache()
        //{
        //    res.Del();
        //    return View("Index","Home");
        //}
        [HttpPost]
        public ActionResult Create(UserModel model, HttpPostedFileBase file)
        {
            model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
            //model.IMG = UploadPhoto(file);
            var isInsert = res.InsertUser(model);
            if (isInsert)
            {
                MessageHelper.ShowMessage(this, "Add new user successful!");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("InsertError", "Add new user faile! Pls contact to admin!");
                return View();
            }
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit User", ParentKey = "user", PreservedRouteParameters = "id")]
        public ActionResult Edit(string id)
        {
            try
            {
                //var gid = new Guid(id);
                var model = res.GetUser(id);
                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController:Edit: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public  ActionResult Edit(UserModel model, HttpPostedFileBase file)
        {
            model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
            //if (file != null)
            //    model.IMG = UploadPhoto(file);

            var edit = res.UpdateUser(model);
            if (edit)
            {
                MessageHelper.ShowMessage(this, "Update user successful!");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("InsertError", "Add new role faile! Pls contact to admin!");
                return View();
            }
        }
        //[OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult ShowModalEmployeeInfor(string id)
        {
            if (id.IndexOf('_') != -1)
            {
                if (id.Split('_').Length == 2)
                {
                    ViewBag.EmpId = id.Split('_')[0];
                    ViewBag.SelectionMode = id.Split('_')[1];
                }
                else {
                    ViewBag.EmpId = id.Split('_')[0];
                    ViewBag.SelectionMode = id.Split('_')[1];
                    ViewBag.Addition = id.Split('_')[2];
                }
            }
            else {
                ViewBag.SelectionMode = "singlerow";
                ViewBag.EmpId = id;
                ViewBag.Addition = "";
            }
            return PartialView("EmployeeInfor");
        }

        public ActionResult Delete(string id)
        {
            try
            {
                var model = new UserModel();
                //model.ID = new Guid(id);
                model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
                var delete = res.DeleteUser(model);
                if (delete)
                {
                    MessageHelper.ShowMessage(this, "Delete user successful!");
                    return RedirectToAction("Index");
                }
                else
                {
                    MessageHelper.ShowMessage(this, "Delete user faile!");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController:Delete: " + ex.Message);
                return HttpNotFound();
            }
        }
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        [AllowAnonymous]
        public ActionResult User_Read(int selectType, int deptCode, string userId, string userName, string status, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var list = res.GetUsers(selectType, userId == "" ? null : userId, userName == "" ? null : userName, status, deptCode);
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
        public string ResetPassword(string loginId)
        {
            var result = res.ResetPassword(loginId);
            return result ? "OK" : "Error";
        }

        // GetListMail(int searchType, string empId, string empName, string sex, string nation, int deptId)
        [AllowAnonymous]
        public ActionResult GetListMail(int searchType, string empId, string empName, string sex, string nation, int deptId, bool hasEmail)
        {
            var list = res.GetListMail(searchType, empId == "" ? null : empId, empName == "" ? null : empName, sex == "" ? null : sex, nation == "" ? null : nation, deptId, hasEmail);
            if (list == null) return Json("", JsonRequestBehavior.AllowGet);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #region nonaction
        [NonAction]
        public string UploadPhoto(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var rondom = Guid.NewGuid() + "_" + fileName;
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Resources/user"), rondom);
                var filePathToSave = "user/" + fileName;
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Resources/user")))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Resources/user"));
                }
                file.SaveAs(path);

                return "/Resources/user/" + rondom;
            }
            return "/Resources/user/avata.png";
        }
        #endregion

        #region editprofile
        [AllowAnonymous]
        [HttpGet]
        public ActionResult EditProfile(string id)
        {
            try
            {
                //var gid = new Guid(id);
                var model = res.GetUser(id);
                return PartialView("EditProfile", model);
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserController:Edit: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public string EditProfile(UserModel model, HttpPostedFileBase file)
        {
            //check khi change pass
            if (model.PasswordOld != null)
            {
                LoginModel lmodel = new LoginModel();
                lmodel.LoginID = model.LoginID;
                lmodel.Password = model.PasswordOld;
                var check = ValidateModel(lmodel);
                if (check != null)
                {
                    model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
                    //if (file != null)
                    //    model.IMG = UploadPhoto(file);
                    var edit = res.UpdateUser(model);
                    return edit ? "OK" : "Err";
                }
                else
                {
                    return "Old Pass is not correct";
                }
            }
            else
            {
                model.ModifyUID = User.GetClaimValue(ClaimTypes.Sid);
                //if (file != null)
                //    model.IMG = UploadPhoto(file);
                var edit = res.UpdateUser(model);
                return edit ? "OK" : "Err";
            }
        }

        [NonAction]
        public UserModel ValidateModel(LoginModel model)
        {
            try
            {
                model.Password = ED5Helper.Encrypt(model.Password);
                UserRepository res = new UserRepository();
                return res.Login(model);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " Method ValidateModel:" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
        #endregion

        #region department

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetListTreeView()
        {
            var res = new UserRepository();
            var list = res.GetListDept(0).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        [AllowAnonymous]
        public ActionResult GetListTreeView(int deptId)
        {
            var res = new UserRepository();
            var list = res.GetListDept(deptId).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult GetListMailGroup(int deptId, string deptName)
        {
            var repository = new UserRepository();
            var list = repository.GetListMailGroup(deptId, deptName == "" ? null : deptName);
            
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetEmployeeInfor(string empId, string empName)
        {
            var repository = new UserRepository();
            var list = repository.GetEmployeeInfor(empId, empName);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //GetEmployeeInfor
        #endregion
    }
}