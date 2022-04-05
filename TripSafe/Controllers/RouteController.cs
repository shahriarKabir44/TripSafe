using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripSafe.Models;
using TripSafe.Repositories;

namespace TripSafe.Controllers
{
    public class RouteController : Controller
    {
        private RouteRepository routeRepository;
        private NodeRepository nodeRepository;
        public RouteController()
        {
            routeRepository = new RouteRepository();
            nodeRepository = new NodeRepository();
        }
        // GET: Route
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult viewDetails(int routeId)
        {
            Route routeInfo = routeRepository.findRoute(routeId);
            ViewBag.routeId = routeId;
            ViewBag.routeInfo = routeInfo;
            return View();
        }
        public object getRoutes()
        {
            var datas = routeRepository.getRoutes();
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult createRoute()
        {
            return View();
        }
        [HttpPost]
        public Object insert(Route newRoute)
        {
            Route data = routeRepository.create(newRoute);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}