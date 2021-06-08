using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizWebApp.ViewModel
{
    public class CandidateViewModel
    {
        [Display(Name = "Candidate Name")]
        [Required(ErrorMessage = "Candidate Name is required.")]
        public string CandidateName { get; set; }
    }
}