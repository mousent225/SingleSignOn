using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;

namespace SingleSignOn.Repositories
{
    public class RoleRepository
    {
        public List<RoleModel> GetList()
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SysRoles
                                select new RoleModel()
                                {
                                    RoleId = l.RoleId,
                                    RoleName = l.RoleName,
                                    Description = l.Description,
                                    Status = l.Status,
                                    CreateDate = l.CreateDate,
                                    CreateUser = l.CreateUser,
                                    UpdateDate = l.UpdateDate,
                                    UpdateUser = l.UpdateUser,
                                    DeleteDate = l.DeleteDate,
                                    DeleteUser = l.UpdateUser,
                                    IsDelete = l.IsDelete
                                }).OrderBy(a => a.RoleId).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("RoleRepository GetList: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public List<ActionModel> GetListAction(string roleId, string controlId, string namespaces)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(controlId) || string.IsNullOrEmpty(namespaces))
                return null;
            var res = new AuthorizationRepository();
            var actions = res.GetActions("SingleSignOn.Controllers", controlId);

            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SysRoleMappings
                                where l.ControllerId == controlId && l.RoleId == roleId
                                select new ActionModel()
                                {
                                    RoleId = l.RoleId,
                                    ControllerId = l.ControllerId,
                                    ActionId = l.ActionId,
                                    ActionName = l.ActionName,
                                    IsAllow = l.IsAllow,
                                    CreateDate = l.CreateDate,
                                    CreateUid = l.CreateUid
                                }).OrderBy(a => a.RoleId).ToList();
                    actions.ForEach(i => { if (!list.Any(j => j.ActionId == i.ActionId)) { list.Add(i); } });
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("RoleRepository GetList: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool InsertRoleMapping(ActionModel model)
        {
            
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    //xóa các role cũ
                    db.SysRoleMappings.RemoveRange(db.SysRoleMappings.Where(x => x.RoleId == model.RoleId && x.ControllerId == model.ControllerId));
                    db.SaveChanges();
                    if (string.IsNullOrEmpty(model.ActionId))
                        return true;
                    var listActions = model.ActionId.Split(';');
                    //thêm lại role mới
                    foreach (var action in listActions)
                    {
                        if (string.IsNullOrEmpty(action) || (action.IndexOf('_') == -1))
                            continue;
                        var actionId = action.Split('_')[0];
                        if (db.SysRoleMappings.FirstOrDefault(a => a.RoleId == model.RoleId && a.ControllerId == model.ControllerId && a.ActionId == actionId) != null)
                            continue;
                        var item = new SysRoleMapping()
                        {
                            RoleId = model.RoleId,
                            ControllerId = model.ControllerId,
                            ActionId = action.Split('_')[0],
                            ActionName = action.Split('_')[1],
                            IsAllow = true,
                            CreateUid = model.CreateUid,
                            CreateDate = DateTime.Now
                        };
                        db.SysRoleMappings.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormTemplateRepository InsertRoleMapping: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
    }
}