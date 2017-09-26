using SingleSignOn.Models;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface IAuthorizationMenuRepository
    {
        IEnumerable<MenuTreeViewAuthorizationModel> GetMenuTreeViewAuthorization(string id, string user);
        bool InsertAuthorizationMenu(IEnumerable<SysAuthorization> list);
        bool DeleteAuthorizationMenu(string id, string user);
    }
}
