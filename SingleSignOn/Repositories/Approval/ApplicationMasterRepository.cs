using SingleSignOn.Approval.ViewModels;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SingleSignOn.Repositories.Approval
{
    public class ApplicationMasterRepository
    {
        #region Application Master
        public IEnumerable<ApplicationMasterModel> GetAllMaster(string name, int deptId, string kind, DateTime? dateFrom, DateTime? dateTo, string empId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_APPLICATION_MASTER_GETALL(name, deptId, kind, dateFrom, dateTo, empId)
                                select new ApplicationMasterModel()
                                {
                                    Id = l.Id,
                                    Subject = l.Subject,
                                    ApplicationId = l.ApplicationId,
                                    ApplicationName = l.ApplicationName,
                                    Code = l.Code,
                                    DeptId = l.DeptId,
                                    DeptName = l.DeptName,
                                    Description = l.Description,
                                    ApprovalLine = l.ApprovalLine,
                                    ApprovalLineJson = l.ApprovalLineJson,
                                    ApprovalKind = l.ApprovalKind,
                                    KindName = l.KindName,
                                    CreateDate = l.CreateDate,
                                    CreateUid = l.CreateUid,
                                    CreateName = l.CreateName,
                                    ApprovalStatus = l.ApprovalStatus,
                                    ApprovalStatusName = l.ApprovalStatusName,
                                    NextApprover = l.NextApprover,
                                    NextApproverName = l.NextApproverName,
                                    RequestDate = l.RequestDate

                                }).ToList();
                    if (list == null)
                        return null;
                    foreach (var item in list)
                    {
                        item.IdEncrypt = Util.Encrypt(item.Id.ToString() + "_" + item.ApplicationId.ToString() + "_" + string.Format("hhss", DateTime.Now));
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository Application Master get all: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public ApplicationMasterModel GetInfor(int id, string userId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from l in db.SP_APPLICATION_MASTER_GETDETAIL(id, userId)
                                select new ApplicationMasterModel()
                                {
                                    Id = l.Id,
                                    Subject = l.Subject,
                                    ApplicationId = l.ApplicationId,
                                    ApplicationName = l.ApplicationName,
                                    Code = l.Code,
                                    DeptId = l.DeptId,
                                    DeptName = l.DeptName,
                                    Description = l.Description,
                                    ApprovalLine = l.ApprovalLine,
                                    ApprovalLineJson = l.ApprovalLineJson,
                                    ApprovalKind = l.ApprovalKind,
                                    KindName = l.KindName,
                                    CreateDate = l.CreateDate,
                                    CreateUid = l.CreateUid,
                                    CreateName = l.CreateName,
                                    ApprovalStatus = l.ApprovalStatus,
                                    ApprovalStatusName = l.ApprovalStatusName,
                                    NextApprover = l.NextApprover,
                                    NextApproverName = l.NextApproverName,
                                    //ENTITY FOR SYSTEMROLE
                                    System = l.System,
                                    RequestDate = l.RequestDate,
                                    IsRecall = l.ISRECALL,
                                    Applicant = l.Applicant,
                                    ApplicantName = l.ApplicantName,
                                    CostCenter = l.CostCenter
                                }).FirstOrDefault();
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository Get infor: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public int AppicationMasterInsert(ApplicationMasterModel model)
        {
            try
            {
                if (model == null)
                    return 0;
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_INSERT(model.ApplicationId, model.Subject, model.Applicant, model.RequestDate, model.CostCenter, model.DeptId
                                                                , model.System, model.ApprovalKind.ToString(), model.ApprovalLine, model.ApprovalLineJson, model.ApprovalStatus.ToString()
                                                                , model.NextApprover, null, model.Description, model.Opinion, model.ContactPerson, model.Content, model.CreateDate, model.CreateUid).FirstOrDefault();
                    return result ?? 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository AppicationMasterInsert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return 0;
            }
        }
        public bool AppicationMasterUpdate(ApplicationMasterModel model)
        {
            try
            {
                if (model == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_UPDATE(model.Id, model.ApplicationId, model.Subject, model.Applicant, model.RequestDate, model.CostCenter, model.DeptId
                                                                , model.System, model.ApprovalKind.ToString(), model.ApprovalLine, model.ApprovalLineJson, null
                                                                , null, null, model.Description, model.Opinion, model.ContactPerson, model.Content, model.UpdateDate, model.CreateUid);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository AppicationMasterUpdate: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool ApplicationConfirm(int id, string linkApplication)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_CONFIRM(id, true, linkApplication);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ApplicationConfirm: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool ApplicationRecallConfirm(int id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_CONFIRM(id, false, "");
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ApplicationConfirm: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool ApplicationDelete(int id, string userId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from d in db.ApplicationMasters
                                where d.Id == id
                                select d).FirstOrDefault();
                    if (item == null)
                        return false;
                    item.DeleteDate = DateTime.Now;
                    item.DeleteUid = userId;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ApplicationDelete: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

        #region ApprovalLine
        public IEnumerable<ApprovalHistoryModel> ApprovalHistoryGetList(string userId, int applicationId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from d in db.SP_APPROVAL_HISTORY(userId, applicationId)
                                select new ApprovalHistoryModel()
                                {
                                    Id = d.Id,
                                    ApprovalLine = d.ApprovalLine,
                                    ApprovalLineJson = d.ApprovalLineJson,
                                    CreateDate = d.CreateDate,
                                    MasterId = d.MasterId,
                                    ApplicationMasterName = d.ApplicationMasterName,
                                    ApplicationSubject = d.ApplicationSubject,
                                    ApplicationId = d.ApplicationId
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository SystemRole Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool ApproveApplication(ApprovalModel model)
        {
            try
            {
                if (model == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_APPROVE(model.MasterId, model.ApplicationId, model.IsApprove, model.Comment, model.UserId, model.LinkApplication);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ApproveApplication: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool RecallApplication(int masterId, int applicationId, string userId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var result = db.SP_APPLICATION_MASTER_RECALL(masterId, applicationId, userId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository RecallApplication: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public IEnumerable<ApprovalLineModel> ApprovalLineGetList(int masterId, int applicationId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_APPROVAL_GETLIST(masterId, applicationId)
                                select new ApprovalLineModel()
                                {
                                    EmpId = s.EmpId,
                                    ApproverType = s.ApproverType,
                                    ApproverTypeName = s.ApproverTypeName,
                                    IsApprove = s.IsApprove,
                                    DateApprove = s.DateApprove,
                                    Comment = s.Comment,
                                    DeptFullName = s.DeptFullName,
                                    EmpName = s.EmpName
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository GetInforSystemRole: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        #endregion

        #region Application for system role
        public List<SystemRoleModel> GetInforSystemRole(int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_SYSTEMROLE_GETDETAIL(masterId)
                                select new SystemRoleModel()
                                {
                                    Id = s.Id,
                                    MasterId = s.MasterId,
                                    EmpId = s.EmpId,
                                    EmpName = s.EmpName,
                                    DeptId = s.DeptId,
                                    DeptName = s.DeptName,
                                    EmpNameSameRole = s.EmpNameSameRole,
                                    EmpIdSameRole = s.EmpIdSameRole,
                                    Module = s.Module,
                                    ModuleName = s.ModuleName,
                                    Remark = s.Remark
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository GetInforSystemRole: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool SystemRoleInsert(List<SystemRoleModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    foreach (var model in models)
                    {
                        var item = new SystemRole
                        {
                            MasterId = model.MasterId,
                            EmpId = model.EmpId,
                            DeptId = model.DeptId,
                            Module = model.Module,
                            EmpIdSameRole = model.EmpIdSameRole,
                            Remark = model.Remark
                        };

                        db.SystemRoles.Add(item);
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository SystemRole Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool SystemRoleUpdate(List<SystemRoleModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models[0].MasterId;
                    var list = (from d in db.SystemRoles where d.MasterId == masterId select d).ToList();
                    db.SystemRoles.RemoveRange(list);
                    foreach (var model in models)
                    {
                        var item = new SystemRole
                        {
                            MasterId = model.MasterId,
                            EmpId = model.EmpId,
                            DeptId = model.DeptId,
                            Module = model.Module,
                            EmpIdSameRole = model.EmpIdSameRole,
                            Remark = model.Remark
                        };

                        db.SystemRoles.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository SystemRole Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

        #region Application for email request
        public List<EmailRequestModel> GetInforEmailRequest(int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_EMAILREQUEST_GETDETAIL(masterId)
                                select new EmailRequestModel()
                                {
                                    Id = s.Id,
                                    MasterId = s.MasterId,
                                    EmpId = s.EmpId,
                                    EmpName = s.EmpName,
                                    DeptId = s.DeptId,
                                    DeptName = s.DeptName,
                                    Request = s.Request,
                                    RequestName = s.RequestName,
                                    Reason = s.Reason,
                                    TimesPerMonth = s.TimesPerMonth
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository GetInforSystemRole: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool EmailRequestInsert(List<EmailRequestModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    foreach (var model in models)
                    {
                        var item = new EmailAddress
                        {
                            MasterId = model.MasterId,
                            EmpId = model.EmpId,
                            DeptId = model.DeptId,
                            Request = model.Request,
                            TimesPerMonth = model.TimesPerMonth,
                            Reason = model.Reason
                        };

                        db.EmailAddresses.Add(item);
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository SystemRole Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool EmailRequestUpdate(List<EmailRequestModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models[0].MasterId;
                    var list = (from d in db.EmailAddresses where d.MasterId == masterId select d).ToList();
                    db.EmailAddresses.RemoveRange(list);
                    foreach (var model in models)
                    {
                        var item = new EmailAddress
                        {
                            MasterId = model.MasterId,
                            EmpId = model.EmpId,
                            DeptId = model.DeptId,
                            Request = model.Request,
                            TimesPerMonth = model.TimesPerMonth,
                            Reason = model.Reason
                        };

                        db.EmailAddresses.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository SystemRole Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

        #region Application for it equipment
        public List<ItEquipmentModel> GetInforItEquipment(int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_ITEQUIPMENT_GETDETAIL(masterId)
                                select new ItEquipmentModel()
                                {
                                    Id = s.Id,
                                    MasterId = s.MasterId,
                                    ItemName = s.ItemName,
                                    Operator = s.OS,
                                    Version = s.Version,
                                    Quantity = s.Qty,
                                    Remark = s.Remark
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ItEquipment: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool ItEquipmentInsert(List<ItEquipmentModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    foreach (var model in models)
                    {
                        var item = new ItEquipment
                        {
                            MasterId = model.MasterId,
                            ItemName = model.ItemName,
                            OS = model.Operator,
                            Qty = model.Quantity,
                            Version = model.Version,
                            Remark = model.Remark
                        };

                        db.ItEquipments.Add(item);
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ItEquipment Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool ItEquipmentUpdate(List<ItEquipmentModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models[0].MasterId;
                    var list = (from d in db.ItEquipments where d.MasterId == masterId select d).ToList();
                    db.ItEquipments.RemoveRange(list);
                    foreach (var model in models)
                    {
                        var item = new ItEquipment
                        {
                            MasterId = model.MasterId,
                            ItemName = model.ItemName,
                            OS = model.Operator,
                            Qty = model.Quantity,
                            Version = model.Version,
                            Remark = model.Remark
                        };

                        db.ItEquipments.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository ItEquipment Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

        #region Application for information system
        public InformationSystemModel GetInforInformationSystem(int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_INFORMATIONSYSTEM_GETDETAIL(masterId)
                                select new InformationSystemModel()
                                {
                                    Id = s.ID,
                                    MasterId = s.MasterId,
                                    System = s.System,
                                    SystemName = s.SystemName,
                                    Seriousness = s.Seriousness,
                                    SeriousName = s.SeriousName,
                                    EmpIp = s.EmpId,
                                    EmpName = s.EmpName,
                                    NumDays = s.NumDays,
                                    Solution = s.Solution,
                                    Explanation = s.Explanation
                                }).FirstOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository GetInforInformationSystem: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool InformationSystemInsert(InformationSystemModel models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var item = new InformationSystem
                    {
                        MasterId = models.MasterId,
                        System = models.System,
                        Seriousness = models.Seriousness,
                        Explanation = models.Explanation
                        //EmpId = models.EmpIp
                    };

                    db.InformationSystems.Add(item);
                    db.SaveChanges();
                    

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository InformationSystemInsert Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool InformationSystemUpdate(InformationSystemModel models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models.Id;
                    var list = (from d in db.InformationSystems where d.Id == masterId select d).FirstOrDefault();
                    list.Explanation = models.Explanation;
                    list.System = models.System;
                    list.Seriousness = models.Seriousness;
                    //list.Solution = models.Solution;
                    //list.NumDays = models.NumDays;
                    
                    db.SaveChanges();
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository InformationSystemUpdate: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool InformationSystemUpdateApprove(InformationSystemModel models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models.Id;
                    var list = (from d in db.InformationSystems where d.Id == masterId select d).FirstOrDefault();
                    list.Solution = models.Solution;
                    list.NumDays = models.NumDays;
                    list.EmpId = models.EmpIp;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository InformationSystemUpdateApprove: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

        #region Application for net-client policy
        public List<NetClientPolicyModel> GetInforNetClientPolicy(int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from s in db.SP_APPLICATION_NETCLIENTPOLICY_GETDETAIL(masterId)
                                select new NetClientPolicyModel()
                                {
                                    Id = s.Id,
                                    MasterId = s.MasterId,
                                    IsAllowance = s.IsAllowance,
                                    IpAddress = s.Ip,
                                    AssetNo = s.AssetNo,
                                    FromDate = s.FromDate,
                                    ToDate = s.ToDate,
                                    Reason = s.Reason
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository NetClientPolicy: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool NetClientPolicyInsert(List<NetClientPolicyModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    foreach (var model in models)
                    {
                        var item = new NetClientPolicy
                        {
                            MasterId = model.MasterId,
                            IsAllowance = model.IsAllowance,
                            Ip = Util.IP2INT(model.IpAddress),
                            AssetNo = model.AssetNo,
                            FromDate = model.FromDate,
                            ToDate = model.ToDate,
                            Reason = model.Reason
                        };

                        db.NetClientPolicies.Add(item);
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository NetClientPolicy Insert: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        public bool NetClientPolicyUpdate(List<NetClientPolicyModel> models)
        {
            try
            {
                if (models == null)
                    return false;
                using (var db = new PORTALEntities())
                {
                    var masterId = models[0].MasterId;
                    var list = (from d in db.NetClientPolicies where d.MasterId == masterId select d).ToList();
                    db.NetClientPolicies.RemoveRange(list);
                    foreach (var model in models)
                    {
                        var item = new NetClientPolicy
                        {
                            MasterId = model.MasterId,
                            IsAllowance = model.IsAllowance,
                            Ip = Util.IP2INT(model.IpAddress),
                            AssetNo = model.AssetNo,
                            FromDate = model.FromDate,
                            ToDate = model.ToDate,
                            Reason = model.Reason
                        };

                        db.NetClientPolicies.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("ApplicationMasterRepository NetClientPolicy Update: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
        #endregion

    }
}