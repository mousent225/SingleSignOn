using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class ActionFilterModel
    {
        public string ID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}