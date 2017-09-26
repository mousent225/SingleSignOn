using System.Collections.Generic;
using System.Linq;
using SingleSignOn.Models;

namespace SingleSignOn.ViewModels
{
    public class MenuTreeViewAuthorizationModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool hasChildren { get; set; }
        public bool Checked { get; set; }
        public bool expanded { get; set; }
        public int? Sequence { get; set; }
        public string MasterMenu { get; set; }
        public string User { get; set; }
        public string StrMenu { get; set; }

        private IEnumerable<MenuTreeViewAuthorizationModel> _Children;

        public IEnumerable<MenuTreeViewAuthorizationModel> items
        {
            get
            {
                if (!hasChildren) return null;
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SysMenus
                        where u.ParentID.ToString() == id
                        orderby u.Sequence
                        select new MenuTreeViewAuthorizationModel
                        {
                            id = u.ID.ToString(),
                            text = u.Name,
                            hasChildren = db.SysMenus.Any(m => m.ParentID == u.ID),
                            Sequence = u.Sequence,
                            MasterMenu = u.MasterMenu.ToString(),
                            User = User,
                            //Checked = db.SysAuthorizations.Any(a => a.MasterMenu == u.MasterMenu && a.MenuID == u.ID && a.Owner.ToString() == User),
                        }).ToList();
                    return list;
                }
            }
            set { _Children = value; }
        }
    }
}