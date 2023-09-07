using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherStudentManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();

        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherPanel()
        {

            return View();
        }
    }
}