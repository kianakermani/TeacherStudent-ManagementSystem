using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        public ActionResult AdminPanel(AdminViewModel ad)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM Admin";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Connection = cnn;
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            sqlcomm.Connection = cnn;
            List<AdminViewModel> adminList = new List<AdminViewModel>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var adminInfo = new AdminViewModel();
                    adminInfo.Name = sdr["Name"].ToString();
                    adminInfo.Email = sdr["Email"].ToString();
                    adminInfo.Address = sdr["Address"].ToString();
                    adminInfo.Phone = sdr["Phone"].ToString();
                    adminList.Add(adminInfo);

                }
                ad.admin = adminList;
                cnn.Close();
            }

            return View(ad);
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