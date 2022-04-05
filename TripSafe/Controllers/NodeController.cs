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
    public class NodeController : Controller
    {
        // GET: Node
        private NodeRepository nodeRepository;
        public NodeController()
        {
            this.nodeRepository = new NodeRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public Object getNodes(int routeId)
        {
            var datas = nodeRepository.getNodes(routeId);
            return Json(datas, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public Object insert(Node newNode)
        {
            nodeRepository.insert(newNode);
            return Json(new { data=1}, JsonRequestBehavior.AllowGet);

        }
    }
}