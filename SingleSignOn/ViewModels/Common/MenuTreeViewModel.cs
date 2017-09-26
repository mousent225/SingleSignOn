using SingleSignOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class MenuTreeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public string Dictionary { get; set; }
        public string DictionaryName { get; set; }
        public int? Sequence { get; set; }
        public string Param { get; set; }
        public string MasterMenu { get; set; }

        private IEnumerable<MenuTreeViewModel> _Children;

        public IEnumerable<MenuTreeViewModel> Children
        {
            get
            {
                if (HasChildren)
                {
                    using (var db = new PORTALEntities ())
                    {
                        var list = (from u in db.SysMenus
                                    where u.ParentID.ToString() == Id
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
                return null;
            }
            set { _Children = value; }
        }

    }
}