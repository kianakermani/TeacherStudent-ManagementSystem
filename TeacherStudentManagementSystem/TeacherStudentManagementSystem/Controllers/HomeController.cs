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
    [Authorize]
    public class HomeController : Controller
    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();

        public ActionResult Login()
        {
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherPanel()
        {
            return View();
        }
        [Authorize(Roles = "Student")]
        public ActionResult StudentPanel()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            var User = db.Users.SingleOrDefault(t => t.UserName == login.UserName && t.Password == login.Password);
            if (User != null)
            {
                if (User.RoleID == 1)
                {
                    Session["Role"] = "Admin";
                    Session["ID"] = User.RoleID;
                    FormsAuthentication.SetAuthCookie(User.UserName, login.RememberMe);
                    return RedirectToAction("AdminPanel", "Home");
                }
                else if (User.RoleID == 2)
                {
                    Session["Role"] = "Teacher";
                    Session["ID"] = User.RoleID;
                    FormsAuthentication.SetAuthCookie(User.UserName, login.RememberMe);
                    return RedirectToAction("TeacherPanel", "Home");
                }
                else if (User.RoleID == 3)
                {
                    Session["Role"] = "Student";
                    Session["ID"] = User.RoleID;
                    FormsAuthentication.SetAuthCookie(User.UserName, login.RememberMe);
                    return RedirectToAction("StudentPanel", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("UserName", "UserName not found !");
                ModelState.AddModelError("Password", "Password is incorrect !");


            }
            return View(login);

        }
    }
}