using QuizWebApp.Models;
using QuizWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private QuizDBEntities objQuizDBEntities;

        public AdminController()
        {
            objQuizDBEntities = new QuizDBEntities();
        }
        // GET: Admin
        public ActionResult Index()
        {
            CategoryViewModel objCategoryViewModel = new CategoryViewModel();
            objCategoryViewModel.ListOfCategory = (from objCat in objQuizDBEntities.Categories
                                                   select new SelectListItem()
                                                   {
                                                       Text = objCat.CategoryName,
                                                       Value = objCat.CategoryId.ToString()
                                                   }).ToList();

            return View(objCategoryViewModel);  
        }
        [HttpPost]
        public ActionResult Index(QuestionOptionViewModel objQuestionOptionViewModel)
        {
            Question objQuestion = new Question();
            objQuestion.QuestionName = objQuestionOptionViewModel.QuestionName;
            objQuestion.CategoryId = objQuestionOptionViewModel.CategoryId;
            objQuestion.IsActive = true;
            objQuestion.IsMultiple = false;

            objQuizDBEntities.Questions.Add(objQuestion);
            objQuizDBEntities.SaveChanges();

            int questionId = objQuestion.QuestionId;

            foreach (var item in objQuestionOptionViewModel.ListOfOptions)
            {
                Option objOption = new Option();
                objOption.OptionName = item;
                objOption.QuestionId = questionId;

                objQuizDBEntities.Options.Add(objOption);
                objQuizDBEntities.SaveChanges();
            }

            Answer objAnswer = new Answer();
            objAnswer.AnswerText = objQuestionOptionViewModel.AnswerText;
            objAnswer.QuestionId = questionId;

            objQuizDBEntities.Answers.Add(objAnswer);
            objQuizDBEntities.SaveChanges();

            return Json( new { message = "Data is successfully added.", success = true}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadMathsQuestion()
        {
            var mathsQues = (from objQues in objQuizDBEntities.Questions
                             where objQues.CategoryId == 1
                             join objAns in objQuizDBEntities.Answers on objQues.QuestionId equals objAns.QuestionId                             
                             select new MathsQuesViewModel()
                             {
                                 QuestionId = objQues.QuestionId,
                                 Question = objQues.QuestionName,
                                 Answer = objAns.AnswerText
                             }
                             ).ToList();

            return PartialView("LoadMathsQuestion", mathsQues);
        }
        [HttpPost]
        public JsonResult DeleteMathsQues(int questionId)
        {
            var question = objQuizDBEntities.Questions.Single(model => model.QuestionId == questionId);
            var answer = objQuizDBEntities.Answers.Single(model => model.QuestionId == questionId);            

            objQuizDBEntities.Questions.Remove(question);
            objQuizDBEntities.Answers.Remove(answer);
            
            objQuizDBEntities.SaveChanges();

            return Json(new { message = "Question is successfully deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadScienceQuestion()
        {

            var scienceQues = (from obj in objQuizDBEntities.Questions
                               where obj.CategoryId == 2
                               join objAns in objQuizDBEntities.Answers on obj.QuestionId equals objAns.QuestionId
                               select new ScienceQuesViewModel()
                               {
                                   QuestionId = obj.QuestionId,
                                   Question = obj.QuestionName,
                                   Answer = objAns.AnswerText
                               }
                               ).ToList();

            return PartialView("LoadScienceQuestion", scienceQues);
        }
    }
}