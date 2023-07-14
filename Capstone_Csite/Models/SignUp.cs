using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Capstone_Csite.Models
{
    public class SignUp
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display (Name ="First Name")]
        [StringLength(20,ErrorMessage ="First Name not be exceed 20 Letters")]
        public string fName { get; set; }


      
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Last Name not be exceed 20 Letters")]
        public string lName { get; set; }


       
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$", ErrorMessage = "Entered phone format is not valid.")]
        public string phone { get; set; }

        [Required]

        [Range(18,60, ErrorMessage ="Please Enter Between 18 to 60")]
        public int age { get; set; }

        [Display (Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid ")]
        public string email { get; set; }

       
        [Required(ErrorMessage = "Must Enter Username"), MinLength(5)]
        [StringLength(20, ErrorMessage = "Username not be exceed 20 Letters")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required Password"), MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("password"), DataType(DataType.Password)]
        public string con_password { get; set; }
        public string usertype { get; set; }

    }
}