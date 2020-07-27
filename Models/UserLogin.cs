using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="This field is required")]
        [Display(Name ="Email ID")]
        public string EmailID { get; set; }


        [Required(AllowEmptyStrings =false, ErrorMessage ="This field is required")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}