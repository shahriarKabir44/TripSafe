﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripSafe.Models
{
    public class Road
    {
        public int Id { get; set; }
        public String name { get; set; }
        public int terminal1 { get; set; }
        public int terminal2 { get; set; }
        public double length { get; set; }
    }
}