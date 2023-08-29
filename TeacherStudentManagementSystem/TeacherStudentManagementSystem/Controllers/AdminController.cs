using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeacherStudentManagementSystem.Controllers
{

    public class AdminController : Controller

    {
        TeacherStudentDBEntities db = new TeacherStudentDBEntities();

        // GET: Admin

        //public ActionResult Ad(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admin.Find(id + 1);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View();
        //}


    }
}