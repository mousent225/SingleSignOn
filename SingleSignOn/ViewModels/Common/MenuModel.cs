using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SingleSignOn.ViewModels
{
    public class MenuModel
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string Icon { get; set; }
        public byte? Sequence { get; set; }
        public string Dictionary { get; set; }
        public string DictionaryName { get; set; }
        public string ParentName { get; set; }
        public bool Actived { get; set; }
        public string MasterMenu { get; set; }
        public string ModifyUID { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}