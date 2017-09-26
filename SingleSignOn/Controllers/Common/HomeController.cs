using SingleSignOn.Repositories;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Security.Claims;
using System.ComponentModel;

namespace SingleSignOn.Controllers
{
    [KSAuthorized]
    [DisplayName("Home")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        //[OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        //[OutputCache(Duration = 3600, VaryByParam = "none")]

        [AllowAnonymous]
        public ActionResult NavigationBar()
        {
            string menus = GenerateMenu();
            ViewBag.Navigation = menus;
            return PartialView("_NavigationBar");
        }

        #region NonAction

        [NonAction]
        private IEnumerable<MenuModel> GetMenus()
        {
            try
            {
                MenuRepository res = new MenuRepository();
                //return res.GetActiveMenuViaMasterMenu(AppDictionary.MasterMenu.First(x => x.Key == "Navigation").Value);
                return res.GetActiveMenuViaMasterMenuUser(AppDictionary.MasterMenu.First(x => x.Key == "Navigation").Value, User.GetClaimValue(ClaimTypes.Role));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " Method GetMenus:" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        [NonAction]
        private string GenerateMenu()
        {
            string result = null;
            try
            {
                var menus = GetMenus();
                if (menus != null)
                {
                    var listParent = menus.Where(m => m.ParentID == "").ToList();
                    var ul = new HtmlGenericControl("ul");
                    ul.Attributes["class"] = @"menu";
                    ul.ID = "menu1";
                    foreach (var item in listParent)
                    {
                        var li = new HtmlGenericControl("li");
                        var listChildren = GetChildrenMenu(item.ID, menus);
                        if (listChildren != null && listChildren.Count() > 0)
                        {
                            var a = new HtmlGenericControl("a");
                            a.Attributes["href"] = "#";
                            a.InnerHtml =  item.Name;
                            li.Controls.Add(a);
                            ul.Controls.Add(li);
                            li.Controls.Add(AddChildrenMenu(item.ID, menus, li));
                        }
                        else
                        {
                            var a = new HtmlGenericControl("a");
                            if (string.IsNullOrEmpty(item.Controller))
                                a.Attributes["href"] = "#";
                            else
                                a.Attributes["href"] = "/" + item.Controller + (string.IsNullOrEmpty(item.Action) ? "" : "/" + item.Action) + (string.IsNullOrEmpty(item.Param) ? "" : ("/?" + item.Param));
                            a.InnerHtml = item.Name;
                            li.Controls.Add(a);
                            ul.Controls.Add(li);
                        }
                    }
                    using (System.IO.StringWriter swriter = new System.IO.StringWriter())
                    {
                        HtmlTextWriter writer = new HtmlTextWriter(swriter);
                        ul.RenderControl(writer);
                        result = swriter.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("Controller: " + Request.RequestContext.RouteData.Values["Controller"].ToString() + " Action: " + Request.RequestContext.RouteData.Values["Action"].ToString() + " Method GenerateMenu:" + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
            return result;
        }

        [NonAction]
        private IEnumerable<MenuModel> GetChildrenMenu(string parent, IEnumerable<MenuModel> source)
        {
            if (string.IsNullOrEmpty(parent))
                return null;
            return source.Where(m => m.ParentID == parent).ToList();
        }

        [NonAction]
        private HtmlGenericControl AddChildrenMenu(string parent, IEnumerable<MenuModel> source, HtmlGenericControl pLi)
        {
            var ul = new HtmlGenericControl("ul");
            ul.Attributes["class"] = "submenu venitian";
            var listChildren = GetChildrenMenu(parent, source);
            if (listChildren != null && listChildren.Count() > 0)
            {
                foreach (var item in listChildren)
                {
                    var li = new HtmlGenericControl("li");
                    var a = new HtmlGenericControl("a");
                    if (string.IsNullOrEmpty(item.Controller))
                        a.Attributes["href"] = "#";
                    else
                        a.Attributes["href"] = "/" + item.Controller + (string.IsNullOrEmpty(item.Action) ? "" : "/" + item.Action) + (string.IsNullOrEmpty(item.Param) ? "" : ("/?" + item.Param));
                    a.InnerHtml = item.Name;
                    li.Controls.Add(a);
                    ul.Controls.Add(li);
                    HtmlGenericControl liChild = AddChildrenMenu(item.ID, source, li);
                    if (liChild != null)
                        li.Controls.Add(liChild);
                }
                pLi.Controls.Add(ul);
                return ul;
            }
            return null;
        }

        #endregion
    }
}