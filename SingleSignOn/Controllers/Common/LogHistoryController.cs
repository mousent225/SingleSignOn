using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingleSignOn.Repositories;
using Newtonsoft.Json;
using MvcSiteMapProvider;

namespace SingleSignOn.Controllers
{
    public class LogHistoryController : Controller
    {
        [MvcSiteMapNode(Title = "Login History", ParentKey = "home", Key = "loghistory")]
        // GET: LogHistory
        public ActionResult Index()
        {
           // var model = (new LogHistoryRepository()).GetAll();

            return View();
        }
        public ActionResult GetAll(int deptId, string subject,string dateFrom, string dateTo, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : (new DateTime(int.Parse(dateFrom.Split('.')[0]), int.Parse(dateFrom.Split('.')[1]), int.Parse(dateFrom.Split('.')[2])));
            var toDate = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : ((new DateTime(int.Parse(dateTo.Split('.')[0]), int.Parse(dateTo.Split('.')[1]), int.Parse(dateTo.Split('.')[2]))));

            var repo = new LogHistoryRepository();
            var list = repo.GetAll(string.IsNullOrEmpty(subject) ? null : subject,deptId, fromDate, toDate);
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

        public ActionResult GetByDeptId(int deptId, string subject, string dateFrom, string dateTo, string sortdatafield, string sortorder, int pagesize, int pagenum)
        {
            var fromDate = string.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : (new DateTime(int.Parse(dateFrom.Split('.')[0]), int.Parse(dateFrom.Split('.')[1]), int.Parse(dateFrom.Split('.')[2])));
            var toDate = string.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : ((new DateTime(int.Parse(dateTo.Split('.')[0]), int.Parse(dateTo.Split('.')[1]), int.Parse(dateTo.Split('.')[2]))));

            var repo = new LogHistoryRepository();
            var list = repo.GetAll(string.IsNullOrEmpty(subject) ? null : subject,deptId ,fromDate, toDate);
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
    }
}