using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripSafe.Models
{
    public class Node
    {
        public int routeId { get; set; }
        public int terminalId { get; set; }
        public int stoppageIndex { get; set; }
    }
}