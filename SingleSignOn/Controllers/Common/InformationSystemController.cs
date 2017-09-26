using SingleSignOn.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.Controllers
{
    [DisplayName("Information System")]
    public class InformationSystemController : BaseController
    {
        //
        // GET: /InformationSystem/
        public ActionResult Index()
        {
            return View();
        }
	}
}