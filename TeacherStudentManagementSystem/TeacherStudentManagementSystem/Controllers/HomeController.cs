using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        //Admin Functions 
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
        //Admin Panel , Teacher Section
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


        public ActionResult AddProf()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProf([Bind(Include = "Name,FName,CodeMeli,Phone,Email,Address")] Teachers teacher)
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

        [HttpGet]
        public ActionResult EditProf(int id)
        {
            var obj = db.Teachers.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditProf(Teachers te)
        {
            db.Entry(te).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllProf", "Home");

        }

        [HttpGet]
        public ActionResult DeleteProf(int id)
        {
            var obj = db.Teachers.Find(id);
            db.Teachers.Attach(obj);
            db.Teachers.Remove(obj);
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

            return RedirectToAction("AllProf", "Home");
        }

        public ActionResult DetailProf(int id)
        {
            var obj = db.Teachers.Find(id);
            return View(obj);
        }

        //Admin Panel , Student Section
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

        public ActionResult AddStu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStu([Bind(Include = "Name,Fname,CodeMeli,Reshte,Phone,Email,Address")] Students student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Students st = new Students();
                    st.Name = student.Name;
                    st.Fname = student.Fname;
                    st.CodeMeli = student.CodeMeli;
                    st.Reshte = student.Reshte;
                    st.Phone = student.Phone;
                    st.Email = student.Email;
                    st.Address = student.Address;
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("AllStu");
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

            return View(student);
        }


        [HttpGet]
        public ActionResult EditStu(int id)
        {
            var obj = db.Students.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditStu(Students st)
        {
            db.Entry(st).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("AllStu", "Home");

        }

        [HttpGet]
        public ActionResult DeleteStu(int id)
        {
            var obj = db.Students.Find(id);
            db.Students.Attach(obj);
            db.Students.Remove(obj);
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

            return RedirectToAction("AllStu", "Home");
        }

        public ActionResult DetailStu(int id)
        {
            var obj = db.Students.Find(id);
            return View(obj);
        }

        //Admin Panel , Course Section
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
                    courseInfo.CourseID = Convert.ToInt32(sdr["CID"]);
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


        public ActionResult AddCou()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCou([Bind(Include = "Title,Teacher,Days,Time")] Courses course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Courses co = new Courses();
                    co.Title = course.Title;
                    co.Teacher = course.Teacher;
                    co.Days = course.Days;
                    co.Time= course.Time;
                    
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("AllCou");
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

            return View(course);
        }

        [HttpGet]
        public ActionResult EditCou(int id)
        {
            var obj = db.Courses.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditCou(Courses co)
        {
            db.Entry(co).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("AllCou", "Home");

        }

        [HttpGet]
        public ActionResult DeleteCou(int id)
        {
            var obj = db.Courses.Find(id);
            db.Courses.Attach(obj);
            db.Courses.Remove(obj);
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

            return RedirectToAction("AllCou", "Home");
        }



        //Login
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