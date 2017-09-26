using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingleSignOn.Interfaces;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;


namespace SingleSignOn.Repositories
{
    public class NoticesRepository
    {
        public IEnumerable<NoticesModel> GetAll(string subject, string userId, string creator, int? id, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_SYS_NOTICES_GET(subject, userId, creator, id, dateFrom, dateTo)
                                select new NoticesModel()
                                {
                                    Id = l.Id,
                                    Subject = l.Subject,
                                    Descript = l.Descript,
                                    AttachFile = l.IsAttachFile,
                                    UpdateUid = l.CreateUid,
                                    UpdateDate = l.CreateDate,
                                    UpdateName = l.LocalName,
                                    Active = l.IsAcive,
                                    Writer = l.CreateUid
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticesRepository GetNotices: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<NoticesModel> GetTop10()
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_SYS_NOTICES_GET_TOP_10()
                                select new NoticesModel()
                                {
                                    Id = l.Id,
                                    Subject = l.Subject,
                                    AttachFile = l.IsAttachFile,
                                    UpdateDate = l.UpdateDate ?? l.CreateDate,
                                    UpdateName = l.LocalName,
                                    IsSubmit = l.IsSubmit,
                                    Reason = l.Reason,
                                    SubmitDate = l.ConfirmDate,
                                    Active = l.IsAcive
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticesRepository GetTop10: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<AttachFileModel> GetAttachment(string moduleId, int masterId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.Attachments
                                where l.ModuleId == new Guid(moduleId) && l.MasterId == masterId
                                select new AttachFileModel()
                                {
                                    AttachId = l.AttachId,
                                    ModuleId = l.ModuleId,
                                    MasterId = l.MasterId,
                                    FileName = l.FileName,
                                    FilePath = l.FilePath
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticesRepository GetAttachmet: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool InsertNotices(NoticesModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_SYS_NOTICES_INSERT(model.Subject, model.Descript, model.Active, model.FilePath, model.FileName, model.Writer)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("NoticesRepository Insert Notices: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateNotices(NoticesModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_SYS_NOTICES_UPDATE(model.Id, model.Subject, model.Descript, model.Active, model.FilePath, model.FileName, model.Writer)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository Update Notices: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteNotices(NoticesModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysNotices
                                where c.Id == model.Id
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.IsDelete = true;
                    item.DeleteDate = model.UpdateDate;
                    item.DeleteUid = model.UpdateUid;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository Update Category: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool SubmitForm(int id, bool status)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysNotices
                                where c.Id == id
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.IsSubmit = status;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository Submit Notices: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
    }
}