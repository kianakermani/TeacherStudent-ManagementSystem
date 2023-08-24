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

    public class AdminController : Controller
    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();
        // GET: Admin
        public ActionResult AdminPanel(AdminViewModel model)
        {
            var admin = db.Admin.Where(a => a.AdminID == model.AdminID && a.Name == model.Name
            && a.Email == model.Email && a.Phone == model.Phone && a.Address == model.Address
            );
            return View();
        }
    }
}