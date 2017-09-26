using SingleSignOn.Models;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface IAuthorizationRepository
    {
        IEnumerable<AuthorizationModel> GetViaUser(string id);
        bool InsertAuthorization(IEnumerable<SysAuthorization> list);
        IEnumerable<AuthorizationModel> GetControllerAction(string id);
        bool DeleteAuthorization(string id);
        bool CheckAuthorized(ActionFilterModel model);
    }
}
