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
    public class FormTemplateRepository : IFormTemplateRepository
    {
        public IEnumerable<FormTemplateModel> GetAll(string subject, string category, string userId, string creator, int? id, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_CMS_FORMTEMPLATE_GET(subject, userId,creator, category, id, dateFrom, dateTo)
                                select new FormTemplateModel()
                                {
                                    Id = l.Id,
                                    Subject = l.Subject,
                                    Descript = l.Descript,
                                    Category = l.Category,
                                    CategoryName = l.CategoryName,
                                    AttachFile = l.IsAttachFile,
                                    UpdateUid =  l.CreateUid,
                                    UpdateDate = l.CreateDate,
                                    UpdateName = l.LocalName,
                                    ApproveName = l.APRROVENAME,
                                    ApproveDate = l.ApproveDate,
                                    IsApprove = l.IsApprove,
                                    IsSubmit = l.IsSubmit,
                                    Reason = l.Reason,
                                    SubmitDate = l.ConfirmDate,
                                    Active = l.IsAcive,
                                    Writer = l.CreateUid,
                                    NumRead = l.NUM_READ,
                                    Status = l.STATUS
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormTemplateRepository GetFormTemplate: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
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
                LogHelper.Error("FormTemplateRepository GetAttachmet: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }
        
        public bool InsertFormTemplate(FormTemplateModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_CMS_FORMTEMPLATE_INSERT(model.Subject, model.Descript, model.Active,
                                                                        model.Category.ToString(), model.FilePath, model.FileName, model.Writer)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormTemplateRepository Insert Formtemplate: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateFormTemplate(FormTemplateModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_CMS_FORMTEMPLATE_UPDATE(model.Id, model.Subject, model.Descript, model.Active,
                                                                        model.Category.ToString(), model.FilePath, model.FileName, model.Writer)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository Update FormTemplate: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteFormTemplate(FormTemplateModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.CmsFormTemplates
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

        public bool ApproveFormTemplate(FormTemplateModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.CmsFormTemplates
                                where c.Id == model.Id
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.ApproveDate = model.UpdateDate;
                    item.ApproveUid = model.UpdateUid;
                    item.IsApprove = model.IsApprove;
                    item.Reason = model.Reason;
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
                    var item = (from c in db.CmsFormTemplates
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
                LogHelper.Error("CategoryRepository Submit Formtemplate: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdatePin(int id, bool pin)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.CmsFormTemplates
                                where c.Id == id
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.Pin = pin;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository UpdatePin: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

    }
}