using SingleSignOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class CategoryValueTreeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public Guid? ParentID { get; set; }
        public string ParentName { get; set; }
        public int? Sequence { get; set; }
        private IEnumerable<CategoryValueTreeViewModel> _Children;

        public IEnumerable<CategoryValueTreeViewModel> Children
        {
            get
            {
                if (!HasChildren) return null;
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SysCategoryValues
                                where u.ParentID == Id
                                from mm in db.SysCategoryValues.Where(m => m.ID == u.ParentID).DefaultIfEmpty()
                                orderby u.Sequence
                                select new CategoryValueTreeViewModel
                                {
                                    Id = u.ID,
                                    Name = u.Name,
                                    HasChildren = db.SysCategoryValues.Any(m => m.ParentID == u.ID),
                                    Sequence = u.Sequence,
                                    ParentID = u.ParentID,
                                    ParentName = mm.Name
                                }).ToList();
                    return list;
                }
            }
            set { _Children = value; }
        }
    }
}