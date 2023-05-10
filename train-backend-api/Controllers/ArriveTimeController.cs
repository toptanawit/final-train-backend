using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TrainSystem.Models;



namespace TrainSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArriveTimeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ArriveTimeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // BTS
        public Dictionary<TimeSpan, TimeSpan> timetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,5,0) },
            { new TimeSpan(7,0,0), new TimeSpan(0,2,40) },
            { new TimeSpan(9,0,0), new TimeSpan(0,3,35) },
            { new TimeSpan(9,30,0), new TimeSpan(0,6,30) },
            { new TimeSpan(16,0,0), new TimeSpan(0,4,25) },
            { new TimeSpan(16,30,0), new TimeSpan(0,2,40) },
            { new TimeSpan(17,0,0), new TimeSpan(0,2,40) },
            { new TimeSpan(20,0,0), new TimeSpan(0,4,25) },
            { new TimeSpan(21,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        public Dictionary<TimeSpan, TimeSpan> extendedTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,5,0) },
            { new TimeSpan(7,0,0), new TimeSpan(0,5,20) },
            { new TimeSpan(9,0,0), new TimeSpan(0,3,35) },
            { new TimeSpan(9,30,0), new TimeSpan(0,6,30) },
            { new TimeSpan(16,0,0), new TimeSpan(0,4,25) },
            { new TimeSpan(16,30,0), new TimeSpan(0,5,20) },
            { new TimeSpan(17,0,0), new TimeSpan(0,5,20) },
            { new TimeSpan(20,0,0), new TimeSpan(0,4,25) },
            { new TimeSpan(21,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        public Dictionary<TimeSpan, TimeSpan> darkgreenTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(7,0,0), new TimeSpan(0,3,45) },
            { new TimeSpan(9,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(9,30,0), new TimeSpan(0,6,0) },
            { new TimeSpan(16,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(16,30,0), new TimeSpan(0,6,0) },
            { new TimeSpan(17,0,0), new TimeSpan(0,3,45) },
            { new TimeSpan(20,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(21,0,0), new TimeSpan(0,6,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        public Dictionary<TimeSpan, TimeSpan> holidayTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(8,0,0), new TimeSpan(0,5,55) },
            { new TimeSpan(9,0,0), new TimeSpan(0,5,55) },
            { new TimeSpan(11,00,0), new TimeSpan(0,4,30) },
            { new TimeSpan(21,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        public Dictionary<TimeSpan, TimeSpan> holidayExtendedTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(8,0,0), new TimeSpan(0,5,55) },
            { new TimeSpan(9,0,0), new TimeSpan(0,5,55) },
            { new TimeSpan(11,00,0), new TimeSpan(0,6,0) },
            { new TimeSpan(21,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        public Dictionary<TimeSpan, TimeSpan> holidayDarkgreenTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(8,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(9,0,0), new TimeSpan(0,5,40) },
            { new TimeSpan(11,00,0), new TimeSpan(0,5,40) },
            { new TimeSpan(21,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(22,0,0), new TimeSpan(0,8,0) }
        };

        // MRT
        public Dictionary<TimeSpan, TimeSpan> blueTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(7,0,0), new TimeSpan(0,4,0) },
            { new TimeSpan(9,0,0), new TimeSpan(0,7,0) },
            { new TimeSpan(16,30,0), new TimeSpan(0,4,0) },
            { new TimeSpan(19,30,0), new TimeSpan(0,7,0) },
        };

        public Dictionary<TimeSpan, TimeSpan> purpleTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(5,30,0), new TimeSpan(0,9,0) },
            { new TimeSpan(6,30,0), new TimeSpan(0,6,0) },
            { new TimeSpan(8,30,0), new TimeSpan(0,9,0) },
            { new TimeSpan(17,00,0), new TimeSpan(0,6,0) },
            { new TimeSpan(19,30,0), new TimeSpan(0,9,0) },
        };

        public Dictionary<TimeSpan, TimeSpan> holidayPurpleTimetable = new Dictionary<TimeSpan, TimeSpan>()
        {
            { new TimeSpan(6,0,0), new TimeSpan(0,9,0) },
            { new TimeSpan(6,30,0), new TimeSpan(0,6,0) },
            { new TimeSpan(8,30,0), new TimeSpan(0,9,0) },
            { new TimeSpan(17,00,0), new TimeSpan(0,6,0) },
            { new TimeSpan(19,30,0), new TimeSpan(0,9,0) },
        };

        // ARL


        [HttpGet("{station_id}")]
        public TimeSpan GetArrivingTime(string station_id)
        {
            string query = @"select * from stations where station_id = @station_id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@station_id", station_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            TimeSpan intervalTime = new TimeSpan(0,0,0);
            TimeSpan waitingTime = new TimeSpan(0,0,0);
            if (table.Rows.Count != 0)
            {
                TimeSpan timenow = DateTime.Now.TimeOfDay;
                //TimeSpan timenow = new TimeSpan(11,5,0);
                DayOfWeek day = DateTime.Today.DayOfWeek;
                //DayOfWeek day = DayOfWeek.Sunday;
                string line = table.Rows[0][2].ToString();
                string color = table.Rows[0][3].ToString();
                bool isExtended = (bool)table.Rows[0][4];

                if (line == "bts")
                {
                    if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
                    {
                        if (color == "lightgreen")
                        {
                            if (isExtended)
                            {
                                foreach (TimeSpan key in extendedTimetable.Keys)
                                {
                                    if (timenow > key)
                                    {
                                        intervalTime = extendedTimetable[key];

                                        TimeSpan temp = key;
                                        while (temp < timenow)
                                        {
                                            temp += intervalTime;
                                        }
                                        waitingTime = temp - timenow;
                                    }
                                }
                            }
                            else
                            {
                                foreach (TimeSpan key in timetable.Keys)
                                {
                                    if (timenow > key)
                                    {
                                        intervalTime = timetable[key];

                                        TimeSpan temp = key;
                                        while (temp < timenow)
                                        {
                                            temp += intervalTime;
                                        }
                                        waitingTime = temp - timenow;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (TimeSpan key in darkgreenTimetable.Keys)
                            {
                                if (timenow > key)
                                {
                                    intervalTime = darkgreenTimetable[key];

                                    TimeSpan temp = key;
                                    while (temp < timenow)
                                    {
                                        temp += intervalTime;
                                    }
                                    waitingTime = temp - timenow;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (color == "lightgreen")
                        {
                            if (isExtended)
                            {
                                foreach (TimeSpan key in holidayExtendedTimetable.Keys)
                                {
                                    if (timenow > key)
                                    {
                                        intervalTime = holidayExtendedTimetable[key];

                                        TimeSpan temp = key;
                                        while (temp < timenow)
                                        {
                                            temp += intervalTime;
                                        }
                                        waitingTime = temp - timenow;
                                    }
                                }
                            }
                            else
                            {
                                foreach (TimeSpan key in holidayTimetable.Keys)
                                {
                                    if (timenow > key)
                                    {
                                        intervalTime = holidayTimetable[key];

                                        TimeSpan temp = key;
                                        while (temp < timenow)
                                        {
                                            temp += intervalTime;
                                        }
                                        waitingTime = temp - timenow;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (TimeSpan key in holidayDarkgreenTimetable.Keys)
                            {
                                if (timenow > key)
                                {
                                    intervalTime = holidayDarkgreenTimetable[key];

                                    TimeSpan temp = key;
                                    while (temp < timenow)
                                    {
                                        temp += intervalTime;
                                    }
                                    waitingTime = temp - timenow;
                                }
                            }
                        }
                    }
                }
                else if (line == "mrt")
                {
                    if (color == "blue")
                    {
                        foreach (TimeSpan key in blueTimetable.Keys)
                        {
                            if (timenow > key)
                            {
                                intervalTime = blueTimetable[key];

                                TimeSpan temp = key;
                                while (temp < timenow)
                                {
                                    temp += intervalTime;
                                }
                                waitingTime = temp - timenow;
                            }
                        }
                    }
                    else
                    {
                        if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
                        {
                            foreach (TimeSpan key in purpleTimetable.Keys)
                            {
                                if (timenow > key)
                                {
                                    intervalTime = purpleTimetable[key];

                                    TimeSpan temp = key;
                                    while (temp < timenow)
                                    {
                                        temp += intervalTime;
                                    }
                                    waitingTime = temp - timenow;
                                }
                            }
                        }
                        else
                        {
                            foreach (TimeSpan key in holidayPurpleTimetable.Keys)
                            {
                                if (timenow > key)
                                {
                                    intervalTime = holidayPurpleTimetable[key];

                                    TimeSpan temp = key;
                                    while (temp < timenow)
                                    {
                                        temp += intervalTime;
                                    }
                                    waitingTime = temp - timenow;
                                }
                            }
                        }
                    }
                }
            }

            return waitingTime;
        }

        [HttpGet]
        public TimeSpan GetTimeNow()
        {
            return DateTime.Now.TimeOfDay;
        }
    }
}