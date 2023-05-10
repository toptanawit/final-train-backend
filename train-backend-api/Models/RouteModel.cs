using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainSystem.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteStart { get; set; }
        public string RouteEnd { get; set; }
        public string Tag { get; set; }
        public string UserSaved { get; set; }
    }
}