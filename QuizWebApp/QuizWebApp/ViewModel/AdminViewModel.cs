using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApp.ViewModel
{
    public class AdminViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}