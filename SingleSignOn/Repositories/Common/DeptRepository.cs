using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.Repositories.Common
{
    public class DeptRepository
    {
        public List<DeptModel> GetDeptTreeView()
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.HrDeptMasters
                                where u.Parent == null
                                from mm in db.HrDeptMasters.Where(m => m.Id == u.Parent && m.IsDelete == false).DefaultIfEmpty()
                                select new DeptModel
                                {
                                    Id = u.Id.ToString(),
                                    EnName = u.EnName,
                                    HasChildren = db.HrDeptMasters.Any(m => m.Parent == u.Id),
                                    DeptCode = u.Code
                                }).ToList();
                    return list.OrderBy(c => c.Code).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeptRepository GetDeptTreeView: " + ex.Message + " Inner Exception: " +
                                ex.InnerException.Message);
                return null;
            }
        }

        public string GetId(int id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    string str = "";
                    var check = string.Join(",", db.HrDeptMasters.Where(c => c.Parent == null).Select(c => c.Id));
                    if (check.Split(',').Any(c => c == id.ToString()))
                        return "";
                    var lst = db.HrDeptMasters.Where(c => c.Parent == id);
                    if (!lst.Any())
                    {
                        return string.Join(",", db.HrDeptMasters.Where(c => c.Id == id).Select(c => c.Id));
                    }
                    if (!lst.Any())
                        return null;
                    str += string.Join(",", lst.Select(c => c.Id));
                    foreach (var s in str.Split(','))
                        str += "," + GetId(Convert.ToInt32(s)) + ",";
                    return string.Join(",", str.Split(',').Where(c => c != ""));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeptRepository GetId: " + ex.Message + " Inner Exception: " +
                                ex.InnerException.Message);
                return null;
            }
        }
    }
}