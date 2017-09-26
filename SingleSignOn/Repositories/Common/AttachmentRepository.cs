using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;

namespace SingleSignOn.Repositories
{
    public class AttachmentRepository
    {
        public IEnumerable<AttachFileModel> GetList(int masterId, Guid moduleId)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from l in db.Attachments
                                where l.MasterId == masterId && l.ModuleId == moduleId
                                select new AttachFileModel()
                                {
                                    AttachId = l.AttachId,
                                    ModuleId = l.ModuleId,
                                    MasterId = l.MasterId,
                                    FileName = l.FilePath,
                                    FilePath = l.FilePath
                                }).ToList();
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Attachmentfilereportsitory GetList: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
        public bool InsertFileAttachment(List<AttachFileModel> model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    foreach (var item in model.Select(attachFileModel => new Models.Attachment()
                    {
                        //AttachId = attachFileModel.AttachId,
                        FileName = attachFileModel.FileName,
                        FilePath = attachFileModel.FilePath,
                        ModuleId = attachFileModel.ModuleId,
                        MasterId = attachFileModel.MasterId
                    }))
                    {
                        db.Attachments.Add(item);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("FileAttacmentRepository Insert AttachmentFile: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteFileAttachment(AttachFileModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.Attachments
                                where c.AttachId == model.AttachId
                                select c).FirstOrDefault();
                    db.Attachments.Remove(item);
                    db.SaveChanges();
                    //return true;
                }
                //kiem tra neu khong con file dinh kem nao nua thi cap nhat lai ben formtemplate
                if (GetList(model.MasterId, model.ModuleId).Any()) return true;

                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.CmsFormTemplates
                        where c.Id == model.MasterId
                        select c).FirstOrDefault();
                    if(item == null)
                        return false;
                    item.IsAttachFile = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("FileAttacmentRepository DeleteFileAttachment: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
    }
}