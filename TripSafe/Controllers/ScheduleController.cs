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
    public class ScheduleController : Controller
    {
        private ScheduleRepository scheduleRepository;
        public ScheduleController()
        {
            scheduleRepository = new ScheduleRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object findByRoute(int routeId)
        {
            var data = scheduleRepository.getMany($"select  terminalId,routeId,arrivalTime,departureTime,stoppageIndex, (select terminal.name from terminal where terminal.Id=schedule.terminalId) as terminalName from schedule where routeId={routeId};");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}