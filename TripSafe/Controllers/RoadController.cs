using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripSafe.Models;
using TripSafe.Repositories;

namespace TripSafe.Controllers
{
    public class RoadController : Controller
    {
        RoadRepository roadRepository;
        public RoadController()
        {
            roadRepository = new RoadRepository();
        }
        // GET: Road
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public Object getRoads()
        {
            var data = roadRepository.GetRoads();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public Object insert(Road newRoad)
        {
            var data = roadRepository.insertRoad(newRoad);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}