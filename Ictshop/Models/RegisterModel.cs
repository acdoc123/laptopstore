using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ictshop.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID {set; get;}

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Require Name!")]
        public string Name { set; get; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Ex:customer@email.com")]
        [Required(ErrorMessage = "Require Email!")]
        public string Email { set; get; }

        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length is at least 6 characters!")]
        [Required(ErrorMessage = "Require Password!")]
        public string Password { set; get; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match!")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Require Address!")]
        public string Address { set; get; }

        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "Wrong Format! ex: 0123456789")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Require 10 or 11 numbers!")]
        [Required(ErrorMessage = "Require Phone number!")]
        public string Phone { set; get; }
    }
}