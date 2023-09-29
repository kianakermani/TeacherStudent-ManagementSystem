using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherStudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        [Authorize(Roles = "Student")]
        public ActionResult StudentPanel(CourseViewModel co)
        {
            string userName = User.Identity.Name;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM dbo.Courses WHERE StudentID=@UserName";
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

        public ActionResult MyHomeWork(HomeWorkViewModel ho)
        {
            string userName = User.Identity.Name;
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=kiana\sqlexpress;Initial Catalog=TeacherStudentDB;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            string s1 = "SELECT * FROM dbo.HomeWork WHERE StudentID=@UserName";
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
                    homeworkInfo.StudentID = sdr["StudentID"].ToString();
                    homeWorkViewModels.Add(homeworkInfo);

                }
                ho.homework = homeWorkViewModels;

                cnn.Close();
            }
            return View(ho);

        }
    }
}