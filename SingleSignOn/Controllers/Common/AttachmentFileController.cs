using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.Repositories
{
    public class AttachmentFileController : BaseController
    {
        [HttpGet]
        public ActionResult GetListAttachment(string moduleId, string masterId)
        {
            var result = (new AttachmentRepository()).GetList(masterId == "" ? 0 : int.Parse(masterId), new Guid(moduleId));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}