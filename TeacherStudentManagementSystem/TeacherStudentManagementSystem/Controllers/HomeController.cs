using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
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
                    adminInfo.AdminID = Convert.ToInt32(sdr["AdminID"]);
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

        public ActionResult AllProf(TeacherViewModel pr)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM Teachers";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Connection = cnn;
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            sqlcomm.Connection = cnn;
            List<TeacherViewModel> teacherList = new List<TeacherViewModel>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var tacherInfo = new TeacherViewModel();
                    tacherInfo.TeacherID = Convert.ToInt32(sdr["TID"]);
                    tacherInfo.Name = sdr["Name"].ToString();
                    tacherInfo.Family = sdr["FName"].ToString();
                    tacherInfo.Email = sdr["Email"].ToString();
                    teacherList.Add(tacherInfo);

                }
                pr.teacher = teacherList;
                cnn.Close();
            }
            return View(pr)
;
        }

        public ActionResult AllStu(StudentViewModel st)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM Students";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Connection = cnn;
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            sqlcomm.Connection = cnn;
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var studentInfo = new StudentViewModel();
                    studentInfo.StudentID = Convert.ToInt32(sdr["SID"]);
                    studentInfo.Name = sdr["Name"].ToString();
                    studentInfo.Family = sdr["FName"].ToString();
                    studentInfo.Email = sdr["Email"].ToString();
                    studentList.Add(studentInfo);

                }
                st.student = studentList;
                cnn.Close();
            }
            return View(st)
;
        }

        public ActionResult AllCou(CourseViewModel co)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM Courses";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Connection = cnn;
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            sqlcomm.Connection = cnn;
            List<CourseViewModel> courseList = new List<CourseViewModel>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var courseInfo = new CourseViewModel();
                    courseInfo.Title = sdr["Title"].ToString();
                    courseInfo.Teacher = sdr["Teacher"].ToString();
                    courseInfo.Days = sdr["Days"].ToString();
                    courseInfo.Time = sdr["Time"].ToString();
                    courseList.Add(courseInfo);

                }
                co.course = courseList;
                cnn.Close();
            }
            return View(co)
;
        }


        public ActionResult AddProf()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProf([Bind(Include = "Name,FName,CodeMeli,Phone,Email,Address")] Teachers teacher )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Teachers te = new Teachers();
                    te.Name = teacher.Name;
                    te.FName = teacher.FName;
                    te.CodeMeli = teacher.CodeMeli;
                    te.Phone = teacher.Phone;
                    te.Email = teacher.Email;
                    te.Address = teacher.Address;
                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                    return RedirectToAction("AllProf");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }

            return View(teacher);
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