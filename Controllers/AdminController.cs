using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartManagerLibrarySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageUsers()
        {
            return View();
        }
        public ActionResult AddBook()
        {
            return View();
        }
        public ActionResult EditBook()
        {
            return View();
        }
        public ActionResult ManagerBook()
        {
            return View();
        }
    }
}