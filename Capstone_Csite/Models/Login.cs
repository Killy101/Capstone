using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone_Csite.Models
{
    public class Login
    {
        
        [Required(ErrorMessage = "Must Enter Username")]
        [StringLength(20, ErrorMessage = "Username not be exceed 20 Letters")]
        [Display(Name = "Username")]
        public string username { get; set; }


        [Required(ErrorMessage = "Required Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}