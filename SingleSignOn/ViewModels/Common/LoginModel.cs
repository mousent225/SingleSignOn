using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SingleSignOn.Utilities;

namespace SingleSignOn.ViewModels
{
    public class LoginModel
    {

        [Required(ErrorMessage = "required")]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool Remember { get; set; }

        
       
    }
}