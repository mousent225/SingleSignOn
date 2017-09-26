using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class TreeViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int? parentid { get; set; }
    }
}