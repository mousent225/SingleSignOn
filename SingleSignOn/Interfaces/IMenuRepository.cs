using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface IMenuRepository
    {
        IEnumerable<MenuModel> GetActiveMenuViaMasterMenu(string id);
        IEnumerable<MenuModel> GetActiveMenuViaMasterMenuUser(string id, string user);
        IEnumerable<MenuModel> GetAllMenuViaMasterMenu(string id);
        MenuModel GetMenu(string id);
        bool InsertMenu(MenuModel model);
        bool UpdateMenu(MenuModel model);
        bool DeleteMenu(string id);
        IEnumerable<MenuTreeViewModel> GetMenuTreeView(string id);
    }
}
