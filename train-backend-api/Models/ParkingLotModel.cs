using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainSystem.Models
{
    public class ParkingLot
    {
        public string parking_id { get; set; }
        public string parkinglot_name { get; set; }
        public string station_id { get; set; }
        public int car_lot { get; set; }
        public int car_fee { get; set; }
        public int car_fee_per { get; set; }
        public int car_fee_use { get; set; }
        public int car_fee_use_per { get; set; }
        public int car_fee_day { get; set; }
        public int car_fee_month { get; set; }
        public bool car_inside { get; set; }
        public int motorcycle_lot { get; set; }
        public int motorcycle_fee { get; set; }
        public int motorcycle_fee_per { get; set; }
        public int motorcycle_fee_use { get; set; }
        public int motorcycle_fee_use_per { get; set; }
        public int motorcycle_fee_day { get; set; }
        public int motorcycle_fee_month { get; set; }
        public bool motorcycle_inside { get; set; }
        public string open_time { get; set; }
        public string close_time { get; set; }
        public string open_time_weekend { get; set; }
        public string close_time_weekend { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}