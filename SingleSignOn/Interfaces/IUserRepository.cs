using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Interfaces
{
    interface IUserRepository
    {
        UserModel Login(LoginModel model);
        IEnumerable<UserModel> GetUsers(int searchType, string userId, string userName, string status, int deptCode);
        UserModel GetUser(string id);
        bool CheckExists(UserModel model);
        bool InsertUser(UserModel model);
        bool UpdateUser(UserModel model);
        bool DeleteUser(UserModel model);

        bool ChangePassword(string empId, string pass);
        int UpdatePassword();
    }
}
