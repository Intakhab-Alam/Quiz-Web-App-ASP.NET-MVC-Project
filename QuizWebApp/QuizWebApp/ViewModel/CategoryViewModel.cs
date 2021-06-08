using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApp.ViewModel
{
    public class CategoryViewModel
    {
        [Display(Name ="Category")]
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question is required.")]
        public string QuestionName { get; set; }

        [Display(Name = "Option")]
        [Required(ErrorMessage = "Option is required.")]
        public string OptionName { get; set; }

        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> ListOfCategory { get; set; }
    }
}