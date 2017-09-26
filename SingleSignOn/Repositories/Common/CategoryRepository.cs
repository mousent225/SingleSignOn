using SingleSignOn.Interfaces;
using SingleSignOn.Models;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SingleSignOn.Repositories
{
    public class CategoryRepository : ICategoryRepository, ICategoryValueRepository
    {
        #region Category

        public IEnumerable<CategoryModel> GetListCategory(string name)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from c in db.SysCategories
                                from u in db.SysUsers.Where(u => u.LoginID == (c.UpdateUid ?? c.CreateUid)).DefaultIfEmpty()
                                where c.Deleted != true && c.Name.Contains(name)
                                orderby c.CreateDate descending
                                select new CategoryModel()
                                {
                                    ID = c.ID,
                                    Name = c.Name,
                                    Remark = c.Remark,
                                    ModifyDate = c.UpdateDate ?? c.CreateDate,
                                    ModifyUID = c.UpdateUid ?? c.CreateUid,
                                    ModifyUser = u.Name
                                }).ToList();
                    return list;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository GetListCategory: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public CategoryModel GetCategory(string id)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysCategories
                                where c.ID.ToString() == id
                                select new CategoryModel()
                                {
                                    ID = c.ID,
                                    Name = c.Name,
                                    Remark = c.Remark,
                                    CreateDate = c.CreateDate,
                                    CreateUID = c.CreateUid,
                                    ModifyDate = c.UpdateDate,
                                    ModifyUID = c.UpdateUid,
                                    Deleted = c.Deleted,
                                    DeleteDate = c.DeleteDate,
                                    DeleteUID = c.DeleteUid
                                }).FirstOrDefault();
                    return item;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository GetCategory: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }
        
        public bool CheckExist(string name)
        {
            using (var db = new PORTALEntities())
            {
                var isExists = (from c in db.SysCategories where c.Name == name && c.Deleted != true select c).Any();
                return isExists;
            }
        }

        public bool InsertCategory(CategoryModel model)
        {
            try
            {
                if (model == null) return false;
                
                using (var db = new PORTALEntities())
                {
                    var item = new SysCategory
                    {
                        ID = model.ID,
                        Name = model.Name,
                        Remark = model.Remark,
                        CreateDate = model.CreateDate,
                        CreateUid = model.CreateUID
                    };

                    db.SysCategories.Add(item);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository Insert Category: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateCategory(CategoryModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysCategories
                                where c.ID == model.ID
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.Name = model.Name;
                    item.UpdateDate = model.ModifyDate;
                    item.UpdateUid = model.ModifyUID;
                    item.Remark = model.Remark;
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

        public bool DeleteCategory(CategoryModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysCategories
                                where c.ID == model.ID
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.DeleteDate = model.DeleteDate;
                    item.DeleteUid = model.DeleteUID;
                    item.Deleted = model.Deleted;
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
        #endregion

        #region CategoryValue
        public CategoryValueModel GetCategoryValue(string id)
        {
            try
            {
                using (var _db = new PORTALEntities())
                {
                    var u = (from v in _db.SysCategoryValues
                             from m in _db.SysUsers
                                .Where(m => m.LoginID == (v.CreateUid ?? v.UpdateUid)).DefaultIfEmpty()
                             from c in _db.SysCategories
                                 .Where(c => c.ID == v.Category).DefaultIfEmpty()
                             from p in _db.SysCategoryValues
                                .Where(p => p.ID == v.ParentID).DefaultIfEmpty()
                             where v.ID.ToString() == id && (v.IsDelete != true)
                             orderby v.ParentID, v.Sequence
                             select new CategoryValueModel
                             {
                                 ID = v.ID,
                                 Name = v.Name,
                                 SubCode = v.SubCode ?? "",
                                 Sequence = v.Sequence ?? 0,
                                 Category = v.Category,
                                 CategoryName = c.Name,
                                 ParentID = v.ParentID,
                                 ParentName = p.Name ?? "",
                                 RemarkValue = v.Remark
                             }).FirstOrDefault();
                    return u;

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository GetCategoryValue: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<CategoryValueModel> GetListValues(string category, string name, string status)
        {
            var bStatus = false;
            if (status != "")
                bStatus = (status == "1");
            var cate = new Guid(category);
            if (category == null)
                return null;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var u = (from v in db.SysCategoryValues
                             from m in db.SysUsers
                                 .Where(m => m.LoginID == (v.CreateUid ?? v.UpdateUid)).DefaultIfEmpty()
                             from c in db.SysCategories
                                 .Where(c => c.ID == v.Category).DefaultIfEmpty()
                             from p in db.SysCategoryValues
                                 .Where(p => p.ID == v.ParentID).DefaultIfEmpty()
                             where v.Category == cate && v.IsDelete != true && v.Name.Contains(name) && (status == "" || v.Actived == bStatus)
                              orderby v.ParentID, v.Sequence
                             select new CategoryValueModel
                             {
                                 ID = v.ID,
                                 Name = v.Name,
                                 SubCode = v.SubCode ?? "",
                                 Sequence = v.Sequence ?? 0,
                                 Category = v.Category,
                                 CategoryName = c.Name,
                                 ParentID = v.ParentID,
                                 ParentName = p.Name ?? "",
                                 ModifyDate = v.UpdateDate ?? v.CreateDate,
                                 ModifyName = m.Name,
                                 Actived = v.Actived,
                                 HasChild = db.SysCategoryValues.Any(a => a.ParentID == v.ID && a.IsDelete != true)
                             }).ToList();
                    return u;

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository:GetListValue(" + category.ToString() + "):  " + ex.Message);
                return null;
            }
        }

        public IEnumerable<CategoryValueTreeViewModel> GetListViaParent(string category, string parent = "")//
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SysCategoryValues
                                where u.ParentID == null 
                                      && u.Category == new Guid(category) && u.IsDelete != true && (parent == "" || u.ID.ToString() != parent)
                                      //&& u.ID != new Guid("4BA7FBD4-276D-437B-861E-DA64D3722F3F")//không lấy lên thằng ALL
                                      && u.ID != new Guid("EAF964F0-F557-40C7-AD06-F0B4A6571A4F")//CMS - My post
                                      && u.Name != "All"
                                from mm in db.SysCategoryValues.Where(m => m.ID == u.ParentID).DefaultIfEmpty()
                                orderby u.Sequence 
                                select new CategoryValueTreeViewModel
                                {
                                    Id = u.ID,
                                    Name = u.Name,
                                    HasChildren = db.SysCategoryValues.Any(m => m.ParentID == u.ID),
                                    Sequence = u.Sequence,
                                    ParentID = u.ParentID,
                                    ParentName = mm.Name
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository GetListViaParent: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<CategoryTree> GetListTreeView(string category, string role)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var list = (from u in db.SP_CMS_GET_TREEVIEW_VIA_CATEGORY(category, role)
                                select new CategoryTree
                                {
                                    id = u.ID,
                                    Name = u.NAME,
                                    parentid = u.PARENTID,
                                    ParentName = u.PARENTNAME
                                }).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository GetListTreeView: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<CategoryValueModel> GetActivedValues(string category, bool isEmpty)
        {
            if (string.IsNullOrEmpty(category))
                return null;
            try
            {
                using (var db = new PORTALEntities())
                {
                    var u = (from v in db.SysCategoryValues
                             from m in db.SysUsers
                                 .Where(m => m.LoginID == (v.CreateUid ?? v.UpdateUid)).DefaultIfEmpty()
                             from c in db.SysCategories
                                 .Where(c => c.ID == v.Category).DefaultIfEmpty()
                             from p in db.SysCategoryValues
                                 .Where(p => p.ID == v.ParentID).DefaultIfEmpty()
                             where v.Category.ToString() == category && (v.IsDelete != true) && v.Actived
                             orderby v.Sequence
                             select new CategoryValueModel
                             {
                                 ID = v.ID,
                                 Name = v.Name,
                                 SubCode = v.SubCode ?? "",
                                 Sequence = v.Sequence ?? 0,
                                 Category = v.Category,
                                 CategoryName = c.Name,
                                 ParentID = v.ParentID,
                                 ParentName = p.Name ?? "",
                                 ModifyDate = v.UpdateDate ?? v.CreateDate,
                                 ModifyName = m.Name,
                                 Actived = v.Actived,
                                 HasChild = db.SysCategoryValues.Any(a => a.ParentID == v.ID && a.IsDelete != true)
                             }).ToList();
                    if (isEmpty)
                        u.Insert(0, new CategoryValueModel() { ID = new Guid(), Name = "", DictionaryName = "" });
                    return u;

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository:GetListValue(" + category.ToString() + "):  " + ex.Message);
                return null;
            }
        }

        public bool InsertCategoryValue(CategoryValueModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var item = new SysCategoryValue()
                    {
                        ID = Guid.NewGuid(),
                        Name = model.Name,
                        Dictionary = model.Dictionary,
                        Sequence = model.Sequence,
                        Actived = model.Actived,
                        Category = model.Category,
                        ParentID = model.ParentID,
                        Remark = model.RemarkValue,
                        CreateDate = model.CreateDate,
                        CreateUid = model.CreateUID,
                        SubCode = model.SubCode
                    };
                    db.SysCategoryValues.Add(item);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository InsertCategoryValue: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool UpdateCategoryValue(CategoryValueModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysCategoryValues
                                where c.ID == model.ID
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.Name = model.Name;
                    item.Dictionary = model.Dictionary;
                    item.Sequence = model.Sequence;
                    item.Actived = model.Actived;
                    item.Category = model.Category;
                    item.ParentID = model.ParentID;
                    item.Remark = model.RemarkValue;
                    item.UpdateDate = model.ModifyDate;
                    item.UpdateUid = model.ModifyUID;
                    item.SubCode = model.SubCode;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository UpdateCategoryValue: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool DeleteCategoryValue(CategoryValueModel model)
        {
            try
            {
                if (model == null) return false;
                using (var db = new PORTALEntities())
                {
                    var item = (from c in db.SysCategoryValues
                                where c.ID == model.ID
                                select c).FirstOrDefault();
                    if (item == null) return false;
                    item.IsDelete = true;
                    item.DeleteDate = model.DeleteDate;
                    item.DeleteUid = model.DeleteUID;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository DeleteCategoryValue: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        public bool CheckExistCateValue(string category, string name)
        {
            try
            {
                using (var db = new PORTALEntities())
                {
                    var u = (from v in db.SysCategoryValues

                             where v.Category.ToString() == category && v.Name == name && (v.IsDelete != true)
                             select v).FirstOrDefault();
                    return u != null;

                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("CategoryRepository CheckExistCateValue: " + ex.Message + " Inner exception: " + ex.InnerException.Message);
                return false;
            }
        }

        #endregion
    }
}