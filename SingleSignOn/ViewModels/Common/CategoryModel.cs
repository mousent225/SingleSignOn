using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//add library
using System.ComponentModel.DataAnnotations;

namespace SingleSignOn.ViewModels
{
    public class CategoryModel
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Required")]
        
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]

        [Display(Name = "Remark")]
        public string Remark { get; set; }
        
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Created UID")]
        public string CreateUID { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modified UID")]
        public string ModifyUID { get; set; }
        public string ModifyUser { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string DeleteUID { get; set; }

        public bool ISCONTINUE { get; set; }
    }
}