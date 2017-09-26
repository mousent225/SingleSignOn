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
    public class MenuRepository : IMenuRepository
    {
        public IEnumerable<MenuModel> GetActiveMenuViaMasterMenu(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_GetMenu_Via_MasterMenu(id)
                                select new MenuModel()
                                {
                                    ID = u.ID.ToString(),
                                    Name = u.Name,
                                    Action = u.Action,
                                    Controller = u.Controller,
                                    Param = u.Param,
                                    Icon = u.Icon,
                                    ParentID = u.ParentID.ToString(),
                                    Sequence = u.Sequence
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository GetActiveMenuViaMasterMenu: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<MenuModel> GetAllMenuViaMasterMenu(string id)
        {
            throw new NotImplementedException();
        }

        public MenuModel GetMenu(string id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var model = (from u in db.SysMenus
                                 where u.ID.ToString() == id
                                 from mm in db.SysMenus.Where(m => m.ID == u.ParentID).DefaultIfEmpty()
                                 orderby u.Sequence
                                 select new MenuModel
                                 {
                                     ID = u.ID.ToString(),
                                     Name = u.Name,
                                     Controller = u.Controller,
                                     Action = u.Action,
                                     Sequence = u.Sequence,
                                     ParentID = u.ParentID.ToString(),
                                     Param = u.Param,
                                     ParentName = mm.Name,
                                     Icon = u.Icon,
                                     Actived = u.Actived,
                                     MasterMenu = u.MasterMenu.ToString()
                                 }).FirstOrDefault();
                    return model;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository GetMenu: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool InsertMenu(MenuModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var mn = new SysMenu
                    {
                        ID = Guid.NewGuid(),
                        Name = model.Name,
                        MasterMenu = new Guid(model.MasterMenu)
                    };
                    if (model.ParentID != null)
                        mn.ParentID = new Guid(model.ParentID);
                    mn.Sequence = model.Sequence;
                    mn.Controller = model.Controller;
                    mn.Action = model.Action;
                    mn.Actived = true;
                    mn.CreateUID = new Guid(model.ModifyUID);
                    mn.CreateDate = DateTime.Now;
                    mn.Icon = model.Icon;
                    mn.Param = model.Param;
                    db.SysMenus.Add(mn);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository insert: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateMenu(MenuModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var mn = db.SysMenus.FirstOrDefault(m => m.ID.ToString() == model.ID);
                    mn.Name = model.Name;
                    mn.MasterMenu = new Guid(model.MasterMenu);
                    if (model.ParentID != null)
                        mn.ParentID = new Guid(model.ParentID);
                    mn.Sequence = model.Sequence;
                    mn.Controller = model.Controller;
                    mn.Action = model.Action;
                    mn.Actived = true;
                    mn.ModifyUID = new Guid(model.ModifyUID);
                    mn.ModifyDate = DateTime.Now;
                    mn.Icon = model.Icon;
                    mn.Param = model.Param;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository insert: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteMenu(string id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var mn = db.SysMenus.FirstOrDefault(m => m.ID.ToString() == id);
                    db.SysMenus.Remove(mn);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository insert: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public IEnumerable<MenuTreeViewModel> GetMenuTreeView(string id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SysMenus
                                where u.ParentID == null && u.MasterMenu.ToString() == id
                                from mm in db.SysMenus.Where(m => m.ID == u.ParentID).DefaultIfEmpty()
                                orderby u.Sequence
                                select new MenuTreeViewModel
                                {
                                    Id = u.ID.ToString(),
                                    Name = u.Name,
                                    HasChildren = db.SysMenus.Any(m => m.ParentID == u.ID),
                                    Controller = u.Controller,
                                    Action = u.Action,
                                    Sequence = u.Sequence,
                                    ParentID = u.ParentID.ToString(),
                                    Param = u.Param,
                                    ParentName = mm.Name
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository GetMenu: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }


        public IEnumerable<MenuModel> GetActiveMenuViaMasterMenuUser(string id, string role)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_GetMenu_Via_MasterMenu_User(id, role)
                                select new MenuModel
                                {
                                    ID = u.ID.ToString(),
                                    Name = u.Name,
                                    Action = u.Action,
                                    Controller = u.Controller,
                                    Param = u.Param,
                                    Icon = u.Icon,
                                    ParentID = u.ParentID.ToString(),
                                    Sequence = u.Sequence
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("MenuRepository GetActiveMenuViaMasterMenuUser: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
    }
}