using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Minimum length should be 8 character long")]
        public string Password { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords doesn't match")]
        public string ConfirmPassword { get; set; }


        [Required (AllowEmptyStrings =false,ErrorMessage ="This field is required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:dd/MM/yyyy}")]
        public string DOB { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Display(Name ="Gender")]
        public string Gender { get; set; }



    }
}