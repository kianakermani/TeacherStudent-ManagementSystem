using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
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
    }
}