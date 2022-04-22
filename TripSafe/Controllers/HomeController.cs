using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using TripSafe.Models;
 
using TripSafe.Reposotories;

namespace TripSafe.Controllers
{
    public class HomeController : Controller
    {
        private TicketRepository ticketRepository;
        public HomeController()
        {
            ticketRepository = new TicketRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public Object search(int start_terminal, int end_terminal, int arrivalTime, int vacancy)
        {
            return Json(ticketRepository.searchBus(start_terminal, end_terminal, arrivalTime, vacancy), JsonRequestBehavior.AllowGet);
        }
    }
}