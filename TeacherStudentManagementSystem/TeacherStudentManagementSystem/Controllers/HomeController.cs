using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeacherStudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();

        public ActionResult HomePage()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            var Teacher = db.Teachers.SingleOrDefault(t => t.UserName == login.UserName && t.Password == login.Password);
            if (Teacher != null)
            {

                FormsAuthentication.SetAuthCookie(Teacher.UserName, login.RememberMe);
                return RedirectToAction("Homepage");

            }
            else
            {
                ModelState.AddModelError("UserName", "UserName not found !");
                ModelState.AddModelError("Password", "Password is incorrect !");

                return View();
            }

        }
    }
}