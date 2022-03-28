using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripSafe.Repositories;

namespace TripSafe.Controllers
{
    public class TerminalController : Controller
    {
        TerminalRepository terminalRepository;
        public TerminalController()
        {
            terminalRepository = new TerminalRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public Object getTerminals()
        {
            var data = terminalRepository.getTerminals();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}