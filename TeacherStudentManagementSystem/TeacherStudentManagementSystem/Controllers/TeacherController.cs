using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TeacherStudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();

        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherPanel(CourseViewModel co)
        {
            string userName = User.Identity.Name;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM dbo.Courses WHERE UserName=@UserName";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Parameters.AddWithValue("@Username", userName);
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
                    courseInfo.StartDate = sdr["StartDate"].ToString();
                    courseInfo.UserName = sdr["UserName"].ToString();
                    courseList.Add(courseInfo);

                }
                co.course = courseList;
                cnn.Close();
            }
            return View(co);
        }


        public ActionResult ActiveHomeWork(HomeWorkViewModel ho)
        {
            string userName = User.Identity.Name;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM dbo.HomeWork WHERE TeacherID=@UserName";
            SqlCommand sqlcomm = new SqlCommand(s1);
            cnn.Open();
            sqlcomm.Parameters.AddWithValue("@Username", userName);
            sqlcomm.Connection = cnn;
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            sqlcomm.Connection = cnn;
            List<HomeWorkViewModel> homeWorkViewModels = new List<HomeWorkViewModel>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var homeworkInfo = new HomeWorkViewModel();
                    homeworkInfo.ID = Convert.ToInt32(sdr["HID"]);
                    homeworkInfo.Title = sdr["TitleLesson"].ToString();
                    homeworkInfo.Subject = sdr["Subject"].ToString();
                    homeworkInfo.TeacherName = sdr["TeacherName"].ToString();
                    homeworkInfo.TeacherID = sdr["TeacherID"].ToString();
                    homeworkInfo.DeliveryDate = sdr["DeliveryDate"].ToString();
                    homeWorkViewModels.Add(homeworkInfo);

                }
                ho.homework = homeWorkViewModels;

                cnn.Close();
            }
            return View(ho);

        }

        public ActionResult AddHomeWork()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHomeWork([Bind(Include = "TitleLesson,Subject,TeacherName,TeacherID,DeliveryDate")] HomeWork homework)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HomeWork ho = new HomeWork();
                    ho.TitleLesson = homework.TitleLesson;
                    ho.Subject = homework.Subject;
                    ho.TeacherName = homework.TeacherName;
                    ho.TeacherID = homework.TeacherID;
                    ho.DeliveryDate = homework.DeliveryDate;

                    db.HomeWork.Add(homework);
                    db.SaveChanges();
                    return RedirectToAction("ActiveHomeWork");
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

            return View(homework);
        }

        [HttpGet]
        public ActionResult EditHomeWork(int id)
        {
            var obj = db.HomeWork.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditHomeWork(HomeWork ho)
        {
            db.Entry(ho).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("ActiveHomeWork", "Teacher");

        }

        [HttpGet]
        public ActionResult DeleteHomeWork(int id)
        {
            var obj = db.HomeWork.Find(id);
            db.HomeWork.Attach(obj);
            db.HomeWork.Remove(obj);
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

            return RedirectToAction("ActiveHomeWork", "Teacher");
        }
    }

}