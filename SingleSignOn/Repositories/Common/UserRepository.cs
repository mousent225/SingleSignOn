using SingleSignOn.Interfaces;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleSignOn.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserModel Login(LoginModel model)
        {
            //var active = new Guid(AppDictionary.UserStatus.FirstOrDefault(a => a.Key == "Active").Value);
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from u in db.HrEmpMasters
                                join r in db.SysUserMappings on u.EmpId equals r.UserId
                                join d in db.HrDeptMasterFulls on u.DeptCode equals d.Id
                                where u.Code.ToLower() == model.LoginID.ToLower() && string.Compare(model.Password, u.Password,false) == 0 && u.Status == true
                                select new UserModel()
                                {
                                    LoginID = u.Code,
                                    Name = u.LocalName,
                                    Email = u.Email ?? "",
                                    EmpId = u.EmpId,
                                    //IMG = Util.ByteArrayToImage(item.Img),
                                    DeptId = u.DeptCode,
                                    DeptName = (string.IsNullOrEmpty(d.OrganizationName) ? "" : d.OrganizationName) + (string.IsNullOrEmpty(d.PlantName) ? "" : " > " + d.PlantName) +
                                                (string.IsNullOrEmpty(d.DeptName) ? "" : " > " + d.DeptName) + (string.IsNullOrEmpty(d.SectName) ? "" : " > " + d.SectName),
                                    Status = u.uStatus,
                                    RoleId = r.RoleId,
                                    Password = u.Password
                                }).FirstOrDefault();
                    
                    if (item == null)
                        return null;
                    if (string.Compare(model.Password, item.Password) == 0)
                        return item;
                    else
                        return null;

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository: Login: " + ex.Message + " Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<UserModel> GetUsers(int searchType, string userId, string userName, string status, int deptCode)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from u in db.SP_SYS_USER_LIST(searchType, userId, userName, deptCode)
                                select new UserModel
                                {
                                    //ID = u.,
                                    LoginID = u.Code,
                                    Name = u.LocalName,
                                    Email = u.Email,
                                    OrganizeName = u.OrganizationName,
                                    PlantName = u.PlantName,
                                    DeptName = u.DeptName,
                                    SectionName = u.SectName,
                                    StatusName = u.NAME,
                                    DeptFullName = u.DeptFullName,
                                    DeptId = u.DeptId,
                                    CostCenter = u.Costcenter
                                }).ToList();

                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository: GetUsers: " + ex.Message + " Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public UserModel GetUser(string id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from u in db.SysUsers
                                from u1 in db.SysUsers.Where(u1 => u1.LoginID == u.CreateUid || u1.LoginID == u.UpdateUid).DefaultIfEmpty()
                                where u.Deleted == false && u.LoginID == id
                                select new UserModel
                                {
                                    //ID = u.ID,
                                    LoginID = u.LoginID,
                                    Password = u.Password,
                                    //IMG = u.IMG,
                                    Name = u.Name,
                                    Email = u.Email,
                                    Mobile = u.Mobile,
                                    Actived = u.Actived,
                                    ModifyDate = u.UpdateDate ?? u.CreateDate,
                                    ModifyUID = u.UpdateUid ?? u.CreateUid,
                                    ModifyName = u1.Name
                                }).FirstOrDefault();

                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository: GetUser: " + ex.Message + " Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<EmployeeInfor> GetEmployeeInfor(string empId, string empName)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from u in db.SP_EMPLOYEE_INFOR(empName, empId)
                                select new EmployeeInfor
                                {
                                    //ID = u.,
                                    EmpId = u.Code,
                                    EmpName = u.LocalName,
                                    DeptId = u.DeptId,
                                    DeptName = u.DeptName,
                                    CostCenter = u.Costcenter
                                }).ToList();

                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository: GetEmployeeInfor: " + ex.Message + " Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool CheckExists(UserModel model)
        {
            throw new NotImplementedException();
        }

        public bool InsertUser(UserModel model)
        {
            if (model == null)
                return false;
            try
            {
                var user = new SysUser();
                using (var db = new PORTALEntities())
                {
                    //user.ID = Guid.NewGuid();
                    user.LoginID = model.LoginID;
                    user.Password = ED5Helper.Encrypt(model.Password);
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Mobile = model.Mobile;
                    user.CreateUid = model.ModifyUID;
                    user.CreateDate = DateTime.Now;
                    user.Deleted = false;
                    user.Actived = true;
                    db.SysUsers.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository:InsertUser: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateUser(UserModel model)
        {
            if (model == null)
                return false;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var user = (from u in db.SysUsers where u.LoginID == model.LoginID select u).FirstOrDefault();
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Mobile = model.Mobile;
                    user.UpdateUid = model.ModifyUID;
                    user.UpdateDate = DateTime.Now;
                    user.Actived = model.Actived;
                    if (model.PasswordNew != null)
                        user.Password = ED5Helper.Encrypt(model.PasswordNew); ;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository:UpdateUser: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool ResetPassword(string loginId)
        {
            var list = loginId.Split(';');
            try
            {
                using (var db = new PORTALEntities())
                {
                    foreach (var login in list)
                    {
                        if (string.IsNullOrEmpty(login))
                            continue;
                        var user = (from u in db.HrEmpMasters where u.Code == login select u).FirstOrDefault();
                        user.Password = loginId;
                        user.uStatus = new Guid(AppDictionary.UserStatus.FirstOrDefault(a => a.Key == "Reset").Value);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository:UpdateUser: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteUser(UserModel model)
        {
            if (model == null)
                return false;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var user = (from u in db.SysUsers where u.LoginID == model.LoginID select u).FirstOrDefault();
                    user.DeleteUid = model.ModifyUID;
                    user.DeleteDate = DateTime.Now;
                    user.Deleted = true;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository:UpdateUser: " + ex.Message);
                return false;
            }
        }

        public string GetPassword(string empId)
        {
            using (var db = new PORTALEntities())
            {
                var user = (from u in db.HrEmpMasters where u.Code.ToLower() == empId.ToLower() select u).FirstOrDefault();
                if (user == null)
                    return "";
                return user.Password;
            }
        }

        public string GetStatus(string empId)
        {
            using (var db = new PORTALEntities())
            {
                var user = (from u in db.HrEmpMasters where u.Code.ToLower() == empId.ToLower() select u).FirstOrDefault();
                if (user == null)
                    return "";
                return user.uStatus.ToString();
            }
        }

        public bool ChangePassword(string empId, string pass)
        {
            var active = new Guid(AppDictionary.UserStatus.FirstOrDefault(d => d.Key == "Active").Value);
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = db.HrEmpMasters.FirstOrDefault(i => i.Code.ToLower() == empId.ToLower());
                    if (item == null)
                        return false;
                    item.Password = pass;
                    item.uStatus = active;
                    item.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository:UpdatePassword: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return false;
            }
        }
        //cập nhật password cho nhân viên mới
        public int UpdatePassword()
        {
            var resetPass = new Guid(AppDictionary.UserStatus.FirstOrDefault(d => d.Key == "Reset").Value);
            var newUser = new Guid(AppDictionary.UserStatus.FirstOrDefault(d => d.Key == "New").Value);
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = db.HrEmpMasters.Where(e => e.uStatus == newUser).ToList();
                    list.ForEach(l =>
                    {
                        l.uStatus = resetPass;
                        l.Password = ED5Helper.Encrypt(l.Code);
                    });
                    db.SaveChanges();
                    return list.Count;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository: UpdatePassword: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return -1;
            }
        }

        public IEnumerable<DepartmentModel> GetListDept(int deptId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_SYS_DEPT_GET_TREE(deptId)
                                select new DepartmentModel
                                {
                                    id = u.ID ?? 0,
                                    Name = u.NAME,
                                    parentid = u.PARENTID,
                                    ParentName = u.NAME
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository GetListDept: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<DepartmentModel> GetListMailGroup(int deptId, string deptName)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_SYS_GET_MAIL_GROUP(deptId, deptName)
                                select new DepartmentModel
                                {
                                    OrganizeName = u.OrganizationName,
                                    PlantName = u.PlantName,
                                    DeptName = u.DeptName,
                                    SectionName = u.SectName,
                                    Name = u.NAME,
                                    MailGroup = u.MAILGROUP,
                                    KoName = u.KONAME
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository GetListMailGroup: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<ListMail> GetListMail(int searchType, string empId, string empName, string sex, string nation, int deptId, bool hasEmail)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_SYS_MAIL_LIST(searchType, empId, empName, sex, nation, deptId, hasEmail)
                                select new ListMail
                                {
                                    OrganizeName = u.OrganizationName,
                                    PlantName = u.PlantName,
                                    DeptName = u.DeptName,
                                    SectionName = u.SectName,
                                    EmpId = u.Code,
                                    EmpName = u.LocalName,
                                    Email = u.Email
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("UserRepository GetListMailGroup: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

    }
}