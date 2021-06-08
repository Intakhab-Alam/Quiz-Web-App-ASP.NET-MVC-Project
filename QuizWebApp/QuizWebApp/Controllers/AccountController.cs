using QuizWebApp.Models;
using QuizWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuizWebApp.Controllers
{
    public class AccountController : Controller
    {
        private QuizDBEntities objQuizDBEntities;

        public AccountController()
        {
            objQuizDBEntities = new QuizDBEntities();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminViewModel objAdminViewModel)
        {
            if(ModelState.IsValid)
            {
                Admin objAdmin = objQuizDBEntities.Admins.SingleOrDefault(model => model.UserName == objAdminViewModel.UserName);
                if (objAdmin == null)
                {
                    ModelState.AddModelError(string.Empty, "Email is not exist");
                }
                else if(objAdmin.UserPassword != objAdminViewModel.UserPassword)
                {
                    ModelState.AddModelError(string.Empty, "Password is not correct.");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(objAdminViewModel.UserName, false);
                    var authTicket = new FormsAuthenticationTicket(1, objAdmin.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Admin");
                    string encryptTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}