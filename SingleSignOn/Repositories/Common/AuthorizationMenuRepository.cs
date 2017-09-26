using SingleSignOn.Interfaces;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.Repositories
{
    public class AuthorizationMenuRepository : IAuthorizationMenuRepository
    {
        public IEnumerable<MenuTreeViewAuthorizationModel> GetMenuTreeViewAuthorization(string id, string user)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var listAuMenu = (from a in db.SysAuthorizations.Where(a => a.MasterMenu.ToString() == id && a.Owner.ToString() == user) select a).ToList();
                    string strmenu = "";
                    foreach (var item in listAuMenu)
                    {
                        var last = listAuMenu.Last();
                        if (item.MenuID != last.MenuID)
                            strmenu += item.MenuID + ",";
                        else
                            strmenu += item.MenuID;
                    }

                    var list = (from u in db.SysMenus
                                where u.ParentID == null && u.MasterMenu.ToString() == id
                                orderby u.Sequence
                                select new MenuTreeViewAuthorizationModel
                                {
                                    id = u.ID.ToString(),
                                    text = u.Name,
                                    hasChildren = db.SysMenus.Any(m => m.ParentID == u.ID),
                                    Sequence = u.Sequence,
                                    MasterMenu = u.MasterMenu.ToString(),
                                    User = user,
                                    expanded = true,
                                    StrMenu = strmenu,
                                    Checked = db.SysAuthorizations.Any(a => a.MasterMenu == u.MasterMenu && a.MenuID == u.ID && a.Owner.ToString() == user),
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("AuthorizationMenuRepository GetMenuTreeViewAuthorization: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }


        public bool InsertAuthorizationMenu(IEnumerable<SysAuthorization> list)
        {
            if (list == null || !list.Any())
                return false;
            try
            {
                using (var _db = new PORTALEntities())
                {
                    _db.SysAuthorization.AddRange(list);
                    _db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("AuthorizationMenuRepository:InsertAuthorizationMenu: " + ex.Message);
                return false;
            }
        }


        public bool DeleteAuthorizationMenu(string id, string user)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var deleteAuthor = from a in db.SysAuthorization where a.Owner.ToString() == user && a.MasterMenu.ToString() == id select a;
                    db.SysAuthorization.RemoveRange(deleteAuthor);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("AuthorizationMenuRepository DeleteAuthorizationMenu: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }

        }
    }
}