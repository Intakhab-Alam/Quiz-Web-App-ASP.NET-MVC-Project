using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebApp.ViewModel
{
    public class ResultViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerByUser { get; set; }
        public string Status { get; set; }
    }
}