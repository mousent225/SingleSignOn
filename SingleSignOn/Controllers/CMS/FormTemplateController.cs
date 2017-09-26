﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SingleSignOn.Repositories;
using SingleSignOn.Utilities;
using SingleSignOn.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;

namespace SingleSignOn.Controllers
{
    [DisplayName("Category Management")]
    public class FormTemplateController : BaseController
    {
        //
        // GET: /FormTemplate/
        [MvcSiteMapNode(Title = "Category Management", ParentKey = "home", Key = "formtemplateconfig")]
        [AllowAnonymous]
        public ActionResult Config()
        {
            return View();
        }
        [MvcSiteMapNode(Title = "Category Management", ParentKey = "home", Key = "formtemplate")]
        public ActionResult Index()
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));
            return View();
        }

        [MvcSiteMapNode(Title = "New Category", ParentKey = "formtemplate", Key = "insertformtemplate")]
        public ActionResult Insert()        
        {
            var model = new FormTemplateModel() { Active = true };
            return View(model);
        }

        [AllowAnonymous]
        [MvcSiteMapNode(Title = "View Detail", ParentKey = "formtemplate", Key = "viewdetailformtemplate", PreservedRouteParameters = "id")]        
        public ActionResult ViewDetail(int id)
        {
            var controllerId = this.ControllerContext.RouteData.Values["controller"].ToString() + "Controller";
            ViewBag.Rights = (new AuthorizationRepository()).GetRights(controllerId, User.GetClaimValue(ClaimTypes.Role));

            var empId = User.GetClaimValue(ClaimTypes.PrimarySid);
            try
            {
                var repository = new FormTemplateRepository();
                var item = repository.GetAll(null, null, null, empId, id, null, null).FirstOrDefault();
                return View(item);
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormTemplateControler GetFormTemplate: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                ViewBag.ErrorMessage = "Has something wrong, please contact with admin";
                return RedirectToAction("Index");
            }
        }

        [MvcSiteMapNode(Title = "Edit Category", ParentKey = "formtemplate", Key = "editformtemplate")]
        [AllowAnonymous]
        public ActionResult Edit(int id)
        {
            var empId = User.GetClaimValue(ClaimTypes.PrimarySid);
            try
            {
                var repository = new FormTemplateRepository();
                var item = repository.GetAll(null, null, null, empId, id, null, null).FirstOrDefault();
                return View(item);
            }
            catch (Exception ex)
            {
                LogHelper.Error("FormTemplateControler GetFormTemplate: " + ex.Message + " Inner Exception: " + ex.InnerException.Message);
                ViewBag.ErrorMessage = "Has something wrong, please contact with admin";
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult GetAll(string subject, string category, string userId, int id, string dateFrom, string dateTo, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : (new DateTime(int.Parse(dateFrom.Split('.')[0]), int.Parse(dateFrom.Split('.')[1]), int.Parse(dateFrom.Split('.')[2])));
            var toDate = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : ((new DateTime(int.Parse(dateTo.Split('.')[0]), int.Parse(dateTo.Split('.')[1]), int.Parse(dateTo.Split('.')[2]))));

            string empId = User.GetClaimValue(ClaimTypes.PrimarySid);
            var repository = new FormTemplateRepository();
            var list = repository.GetAll(string.IsNullOrEmpty(subject) ? null : subject, string.IsNullOrEmpty(category) ? null : category, string.IsNullOrEmpty(userId) ? null : userId, empId, id, fromDate, toDate);
            var total = list.Count();
            if (!string.IsNullOrEmpty(sortorder))
            {
                list = sortorder == "asc" ? list.OrderBy(o => o.GetType().GetProperty(sortdatafield).GetValue(o, null)) :
                                            list.OrderByDescending(o => o.GetType().GetProperty(sortdatafield).GetValue(o, null));
            }
            list = list.Skip(pagesize * pagenum).Take(pagesize);
            var result = new
            {
                TotalRows = total,
                Rows = list
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetAttachment(string moduleId, int attachment)
        {
            var list = (new FormTemplateRepository()).GetAttachment(moduleId, attachment);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(FormTemplateModel model)
        {
            //model.Id = Guid.NewGuid();
            model.UpdateUid = User.GetClaimValue(ClaimTypes.Sid);
            model.UpdateDate = DateTime.Now;
            model.Writer = User.GetClaimValue(ClaimTypes.PrimarySid);
            model.WriteDate = DateTime.Now;
            model.WriterName = User.GetClaimValue(ClaimTypes.Name);
            model.UpdateName = User.GetClaimValue(ClaimTypes.Name);
            //if (!ModelState.IsValid)
            //    return View();

            if ((new FormTemplateRepository()).InsertFormTemplate(model))
            {
                //nếu insert thành công thì copy các file đã upload sang đúng thư mục category của nó
                //if(!string.IsNullOrEmpty(model.FileName) && !string.IsNullOrEmpty(model.CategoryName))
                //{
                //    var listFilePath = model.FilePath.Split('|');
                //    var listFileName = model.FileName.Split('|');                   
                //    for (var i = 0; i <listFileName.Length; i++)
                //    {
                //        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate/" + model.CategoryName)))
                //        {
                //            Directory.CreateDirectory(
                //                System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate/" + model.CategoryName));
                //        }
                //        var serverPath = Request.MapPath("~" + listFilePath[i]);
                //        if (System.IO.File.Exists(serverPath))
                //        {
                //            System.IO.File.Move(serverPath, Request.MapPath(listFilePath[i].Replace("/Resources/FormTemplate/", "/Resources/FormTemplate/" + model.CategoryName + "/")));
                //        }
                //    }
                //}
            }
            ModelState.AddModelError("InsertError", "Add new user faile! Pls contact to admin!");
            MessageHelper.ShowMessage(this, "Error", "Insert fail! Pls contact to admin!", NotificationStyle.Error);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public string Edit(FormTemplateModel model)
        {
            model.UpdateUid = User.GetClaimValue(ClaimTypes.Sid);
            model.UpdateDate = DateTime.Now;
            model.Writer = User.GetClaimValue(ClaimTypes.PrimarySid);
            model.WriteDate = DateTime.Now;
            model.WriterName = User.GetClaimValue(ClaimTypes.Name);
            //model.ATTACH_FILE = (fileUpload[0] != null);
            model.UpdateName = User.GetClaimValue(ClaimTypes.Name);

            if ((new FormTemplateRepository()).UpdateFormTemplate(model))
            {
                //nếu update thành công thì copy các file đã upload sang đúng thư mục category của nó
                //if (!string.IsNullOrEmpty(model.FileName) && !string.IsNullOrEmpty(model.CategoryName))
                //{
                //    var listFilePath = model.FilePath.Split('|');
                //    var listFileName = model.FileName.Split('|');
                //    for (var i = 0; i < listFileName.Length; i++)
                //    {
                //        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate/" + model.CategoryName)))
                //        {
                //            Directory.CreateDirectory(
                //                System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate/" + model.CategoryName));
                //        }
                //        var serverPath = Request.MapPath("~" + listFilePath[i]);
                //        if (System.IO.File.Exists(serverPath))
                //        {
                //            System.IO.File.Move(serverPath, Request.MapPath(listFilePath[i].Replace("/Resources/FormTemplate/", "/Resources/FormTemplate/" + model.CategoryName + "/")));
                //        }
                //    }
                //}
            }

            //MessageHelper.ShowMessage(this, "Error", "Update fail! Pls contact to admin!", NotificationStyle.Error);
            return "OK";
        }

        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            var model = new FormTemplateModel()
            {
                Id = id,
                UpdateUid = User.GetClaimValue(ClaimTypes.PrimarySid),
                UpdateDate = DateTime.Now
            };
            var repository = new FormTemplateRepository();
            return Json(repository.DeleteFormTemplate(model) ? new { result = "OK" } : new { result = "ERROR" });
        }

        public ActionResult ForceDelete(int id)
        {
            var model = new FormTemplateModel()
            {
                Id = id,
                UpdateUid = User.GetClaimValue(ClaimTypes.PrimarySid),
                UpdateDate = DateTime.Now
            };
            var repository = new FormTemplateRepository();
            return Json(repository.DeleteFormTemplate(model) ? new { result = "OK" } : new { result = "ERROR" });
        }

        public ActionResult Approve(int id, bool isApprove, string reason)
        {
            var model = new FormTemplateModel()
            {
                UpdateUid = User.GetClaimValue(ClaimTypes.PrimarySid),
                UpdateDate = DateTime.Now,
                IsApprove = isApprove,
                Reason = reason,
                Id = id
            };
            var repository = new FormTemplateRepository();
            return Json(repository.ApproveFormTemplate(model) ? new { result = "OK" } : new { result = "ERROR" });
        }

        [AllowAnonymous]
        public ActionResult Submit(int id, bool status)
        {
            var repository = new FormTemplateRepository();
            return Json(repository.SubmitForm(id, status) ? new { result = "OK" } : new { result = "ERROR" });
        }

        [AllowAnonymous]
        public ActionResult PinFormTemplate(int id, bool pin)
        {
            var repository = new FormTemplateRepository();
            return Json(repository.UpdatePin(id, pin) ? new { result = "OK" } : new { result = "ERROR" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteFile(AttachFileModel model)
        {
            if (model.AttachId != null)
                if (!(new AttachmentRepository()).DeleteFileAttachment(model)) return Json(new { result = "ERROR" });

            if (string.IsNullOrEmpty(model.FilePath)) return Json(new { result = "ERROR" });

            var serverPath = Request.MapPath("~" + model.FilePath);
            if (System.IO.File.Exists(serverPath))
            {
                System.IO.File.Delete(serverPath);
            }
            return Json(new { result = "OK" });
        }

        [AllowAnonymous]
        [HttpPost]
        public string UploadFile(List<HttpPostedFileBase> fileToUpload)
        {
            var maxRequestLength = int.Parse(System.Configuration.ConfigurationManager.AppSettings["maxRequestLength"]);
            
            var listFile = new List<AttachFileModel>();
            if (fileToUpload == null)
                return "Check again";

            foreach (var item in fileToUpload)
            {
                if (item == null) continue;
                var invalidChars = Path.GetInvalidFileNameChars();

                var fileName = (new string(item.FileName
                    .Where(x => !invalidChars.Contains(x))
                    .ToArray())).Replace("&", "");
                //var fileName = Path.GetFileName(item.FileName);

                var fileExtention = "*.gif,*.jpg,*.jpeg,*.png,*.bmp,*.doc,*.docx,*.xlsx,*xls,*.pdf,*.ppt,*.pptx";
                var ext = Path.GetExtension(item.FileName);

                if (fileExtention.IndexOf(ext) == -1)
                    return "Check again: No support file type: " + ext;
                if (item.ContentLength > maxRequestLength)
                    return "Check again: No support file size over: " + maxRequestLength.ToString();

                if (string.IsNullOrEmpty(ext))
                    ext = "";
                var random = fileName.Replace(ext, "_") + string.Format("{0:ddMMyyyHHmmssff}", DateTime.Now) + ext;
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate"),
                    random);
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate")))
                {
                    Directory.CreateDirectory(
                        System.Web.HttpContext.Current.Server.MapPath("~/Resources/FormTemplate"));
                }
                item.SaveAs(path);
                listFile.Add(new AttachFileModel()
                {
                    //AttachId = Guid.NewGuid(),
                    ModuleId = new Guid("3CEE36B0-6121-47FB-9FC6-D90A44004522"),
                    MasterId = 0,
                    FileName = fileName,
                    FilePath = "/Resources/FormTemplate/" + random
                });
            }
            return JsonConvert.SerializeObject(listFile);
        }
        [AllowAnonymous]
        public FileResult DownloadAll(List<string> files)
        {
            var archive = Server.MapPath("~/Resources/archive.zip");
            var temp = Server.MapPath("~/Resources/temp");
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            // clear any existing archive
            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            // empty the temp folder
            Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));

            // copy the selected files to the temp folder
            files.ForEach(f => System.IO.File.Copy(Server.MapPath("~" + f), Path.Combine(temp, Path.GetFileName(Server.MapPath("~" + f)))));

            // create a new archive
            ZipFile.CreateFromDirectory(temp, archive);

            return File(archive, "application/zip", "archive.zip"); ;
        }
        [AllowAnonymous]
        public string GetZipFile(List<string> files)
        {
            var archive = Server.MapPath("~/Resources/archive.zip");
            var temp = Server.MapPath("~/Resources/temp");
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            try
            {
                // clear any existing archive
                if (System.IO.File.Exists(archive))
                {
                    System.IO.File.Delete(archive);
                }
                // empty the temp folder
                Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));

                // copy the selected files to the temp folder
                files.ForEach(f => System.IO.File.Copy(Server.MapPath("~" + f), Path.Combine(temp, Path.GetFileName(Server.MapPath("~" + f)))));

                // create a new archive
                ZipFile.CreateFromDirectory(temp, archive);
            }
            catch (Exception)
            {
                return "";
            }
            return "~/Resources/archive.zip";
        }
        [AllowAnonymous]
        public FileResult DownloadFile(string fileName)
        {
            var archive = Server.MapPath(fileName);
            return File(archive, "application/zip", "archive.zip"); ;
        }

    }
}