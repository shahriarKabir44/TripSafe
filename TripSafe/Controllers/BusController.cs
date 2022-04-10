using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using TripSafe.Models;
using TripSafe.Repositories;

namespace TripSafe.Controllers
{
    public class BusController : Controller
    {
        BusRepository busRepository;
        public BusController()
        {
            busRepository = new BusRepository();
        }
        // GET: Bus
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public Object findBus(int busId)
        {
            var bus = busRepository.findBus(busId);
            return Json(bus, JsonRequestBehavior.AllowGet);
        }
    }
}