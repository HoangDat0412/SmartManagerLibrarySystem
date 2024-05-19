using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartManagerLibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ManagerBook()
        {
            return View();
        }
        [Authorize]
        public ActionResult BookDetails()
        {
            return View();
        }
        [Authorize]
        public ActionResult BorrowedBooks()
        {
            return View();
        }
        [Authorize]
        public ActionResult BorrowHistory()
        {
            return View();
        }
    }
}