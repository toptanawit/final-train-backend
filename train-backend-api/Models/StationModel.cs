using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainSystem.Models
{
    public class Station
    {
        public string StationId { get; set; }
        public string StationName { get; set; }
        public string StationLine { get; set; }
        public string StationLineColor { get; set; }
        public bool IsExtended { get; set; }
    }
}