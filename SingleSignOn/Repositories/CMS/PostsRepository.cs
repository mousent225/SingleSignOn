using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.Repositories
{
    public class PostsRepository
    {
        public IEnumerable<PostsModel> GetAll(string subject, string category, string kind, string creator, int id, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.SP_CMS_POSTS_GET(subject, creator, kind, category, id, dateFrom, dateTo)
                                select new PostsModel()
                                {
                                    PostId = l.PostId,
                                    Subject = l.Subject,
                                    Content = l.Content,
                                    Category = l.Category,
                                    CategoryName = l.CATENAME,
                                    IsAttachFile = l.IsAttachFile,
                                    CreateUid = l.CreateUid,
                                    CreateDate = l.CreateDate,
                                    CreateName = l.CREATERNM,
                                    ApplyToPlant = l.ApplyToPlant,
                                    ApplyToPlantName = l.ApplyToPlantName,
                                    Kind = l.Kind,
                                    KindName = l.KindName,
                                    IsActive = l.IsActive ?? false,
                                    NumRead = l.NUMREAD ?? 0,
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PostsRepository GetAllPost: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public string GetContent(int id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from l in db.CmsPosts
                                where l.PostId == id
                                select l).FirstOrDefault();
                    return list == null ? "" : list.Content;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PostsRepository GetContent: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool InsertFaq(PostsModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_CMS_POSTS_INSERT(model.Subject, model.Content, model.IsActive,
                                                                        model.Category.ToString(), model.Kind.ToString(), model.ApplyToPlant, model.CreateUid)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PostsRepository Insert Faq: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateFaq(PostsModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var result = (from l in db.SP_CMS_POSTS_UPDATE(model.PostId, model.Subject, model.Content, model.IsActive,
                                                                        model.Category.ToString(), model.Kind.ToString(), model.ApplyToPlant, model.CreateUid)
                                  select l).FirstOrDefault();
                    return result == "OK";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PostsRepository Update Faq: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public IEnumerable<TreeViewModel> GetPlant()
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_CMS_POSTS_GET_PLANT()
                                select new TreeViewModel
                                {
                                    id = u.Id ?? 0,
                                    Name = u.NAME,
                                    parentid = u.PARENTID
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("PostsRepository GetPlant: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public bool Delete(PostsModel model)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.CmsPosts
                                where c.PostId == model.PostId
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
                LogHelper.Error("PostsRepository Delete: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }
    }
}