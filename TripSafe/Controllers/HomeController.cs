using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TripSafe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult EmployeeLogin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}