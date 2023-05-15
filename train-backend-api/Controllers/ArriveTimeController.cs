using Microsoft.AspNetCore.Components.Routing;
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
        public TimeSpan[] arlTimetable =
        {
            new TimeSpan(0,14,0),
            new TimeSpan(6,14,0), new TimeSpan(6,26,0), new TimeSpan(6,38,0), new TimeSpan(6,51,0),
            new TimeSpan(7,4,0), new TimeSpan(7,16,0), new TimeSpan(7,29,0), new TimeSpan(7,42,0), new TimeSpan(7,54,0),
            new TimeSpan(8,7,0), new TimeSpan(8,20,0), new TimeSpan(8,32,0), new TimeSpan(8,45,0), new TimeSpan(8,58,0),
            new TimeSpan(9,11,0), new TimeSpan(9,24,0), new TimeSpan(9,38,0), new TimeSpan(9,48,0),
            new TimeSpan(10,2,0), new TimeSpan(10,20,0), new TimeSpan(10,35,0), new TimeSpan(10,51,0),
            new TimeSpan(11,7,0), new TimeSpan(11,22,0), new TimeSpan(11,38,0), new TimeSpan(11,54,0),
            new TimeSpan(12,9,0), new TimeSpan(12,25,0), new TimeSpan(12,41,0), new TimeSpan(12,56,0),
            new TimeSpan(13,12,0), new TimeSpan(13,28,0), new TimeSpan(13,43,0), new TimeSpan(13,59,0),
            new TimeSpan(14,15,0), new TimeSpan(14,30,0), new TimeSpan(14,45,0),
            new TimeSpan(15,2,0), new TimeSpan(15,17,0), new TimeSpan(15,33,0), new TimeSpan(15,49,0),
            new TimeSpan(16,4,0), new TimeSpan(16,22,0), new TimeSpan(16,37,0), new TimeSpan(16,51,0),
            new TimeSpan(17,7,0), new TimeSpan(17,19,0), new TimeSpan(17,31,0), new TimeSpan(17,44,0), new TimeSpan(17,57,0),
            new TimeSpan(18,9,0), new TimeSpan(18,22,0), new TimeSpan(18,35,0), new TimeSpan(18,47,0),
            new TimeSpan(19,0,0), new TimeSpan(19,13,0), new TimeSpan(19,25,0), new TimeSpan(19,38,0), new TimeSpan(19,51,0),
            new TimeSpan(20,3,0), new TimeSpan(20,16,0), new TimeSpan(20,29,0), new TimeSpan(20,42,0), new TimeSpan(20,56,0),
            new TimeSpan(21,10,0), new TimeSpan(21,20,0), new TimeSpan(21,35,0), new TimeSpan(21,51,0),
            new TimeSpan(22,7,0), new TimeSpan(22,22,0), new TimeSpan(22,38,0), new TimeSpan(22,54,0),
            new TimeSpan(23,9,0), new TimeSpan(23,25,0), new TimeSpan(23,41,0), new TimeSpan(23,56,0)
        };

        public TimeSpan[] arlHolidayTimetable =
        {
            new TimeSpan(0,2,0), new TimeSpan(0,17,0),
            new TimeSpan(6,16,0), new TimeSpan(6,32,0), new TimeSpan(6,48,0),
            new TimeSpan(7,3,0), new TimeSpan(7,19,0), new TimeSpan(7,35,0), new TimeSpan(7,50,0),
            new TimeSpan(8,6,0), new TimeSpan(8,22,0), new TimeSpan(8,37,0), new TimeSpan(8,53,0),
            new TimeSpan(9,9,0), new TimeSpan(9,24,0), new TimeSpan(9,40,0), new TimeSpan(9,56,0),
            new TimeSpan(10,11,0), new TimeSpan(10,27,0), new TimeSpan(10,43,0), new TimeSpan(10,58,0),
            new TimeSpan(11,14,0), new TimeSpan(11,30,0), new TimeSpan(11,45,0),
            new TimeSpan(12,1,0), new TimeSpan(12,17,0), new TimeSpan(12,32,0), new TimeSpan(12,48,0),
            new TimeSpan(13,4,0), new TimeSpan(13,19,0), new TimeSpan(13,35,0), new TimeSpan(13,51,0),
            new TimeSpan(14,6,0), new TimeSpan(14,22,0), new TimeSpan(14,38,0), new TimeSpan(14,53,0),
            new TimeSpan(15,9,0), new TimeSpan(15,25,0), new TimeSpan(15,40,0), new TimeSpan(15,56,0),
            new TimeSpan(16,12,0), new TimeSpan(16,27,0), new TimeSpan(16,43,0), new TimeSpan(16,59,0),
            new TimeSpan(17,14,0), new TimeSpan(17,30,0), new TimeSpan(17,46,0),
            new TimeSpan(18,1,0), new TimeSpan(18,17,0), new TimeSpan(18,33,0), new TimeSpan(18,48,0),
            new TimeSpan(19,4,0), new TimeSpan(19,20,0), new TimeSpan(19,35,0), new TimeSpan(19,51,0),
            new TimeSpan(20,7,0), new TimeSpan(20,22,0), new TimeSpan(20,38,0), new TimeSpan(20,54,0),
            new TimeSpan(21,9,0), new TimeSpan(21,25,0), new TimeSpan(21,41,0), new TimeSpan(21,56,0),
            new TimeSpan(22,12,0), new TimeSpan(22,28,0), new TimeSpan(22,43,0), new TimeSpan(22,59,0),
            new TimeSpan(23,15,0), new TimeSpan(23,30,0), new TimeSpan(23,46,0)
        };



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

            TimeSpan intervalTime = new TimeSpan(0, 0, 0);
            TimeSpan waitingTime = new TimeSpan(0, 0, 0);
            if (table.Rows.Count != 0)
            {
                TimeSpan timenow = DateTime.Now.TimeOfDay;
                //TimeSpan timenow = new TimeSpan(11,5,0);
                DayOfWeek day = DateTime.Today.DayOfWeek;
                //DayOfWeek day = DayOfWeek.Sunday;
                string line = table.Rows[0][4].ToString();
                string color = table.Rows[0][5].ToString();
                bool isExtended = (bool)table.Rows[0][6];

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
                else if (line == "arl")
                {
                    if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday)
                    {
                        foreach (TimeSpan key in arlHolidayTimetable)
                        {
                            if (key > timenow)
                            {
                                waitingTime = key - timenow;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (TimeSpan key in arlTimetable)
                        {
                            if (key > timenow)
                            {
                                waitingTime = key - timenow;
                                break;
                            }
                        }
                    }
                }
            }

            return waitingTime;
        }

        public List<string> stations = new List<string>();

        public List<string[]> routes = new List<string[]> {
            // east bts
            new string[] { "CEN", "E1" },
            new string[] { "E1", "E2" },
            new string[] { "E2", "E3" },
            new string[] { "E3", "E4" },
            new string[] { "E4", "E5" },
            new string[] { "E5", "E6" },
            new string[] { "E6", "E7" },
            new string[] { "E7", "E8" },
            new string[] { "E8", "E9" },
            new string[] { "E9", "E10" },
            new string[] { "E10", "E11" },
            new string[] { "E11", "E12" },
            new string[] { "E12", "E13" },
            new string[] { "E13", "E14" },
            new string[] { "E14", "E15" },
            new string[] { "E15", "E16" },
            new string[] { "E16", "E17" },
            new string[] { "E17", "E18" },
            new string[] { "E18", "E19" },
            new string[] { "E19", "E20" },
            new string[] { "E20", "E21" },
            new string[] { "E21", "E22" },
            new string[] { "E22", "E23" },
            // north bts
            new string[] { "CEN", "N1" },
            new string[] { "N1", "N2" },
            new string[] { "N2", "N3" },
            new string[] { "N3", "N4" },
            new string[] { "N4", "N5" },
            new string[] { "N5", "N7" },

            new string[] { "N7", "N8" },
            new string[] { "N8", "N9" },
            new string[] { "N9", "N10" },
            new string[] { "N10", "N11" },
            new string[] { "N11", "N12" },
            new string[] { "N12", "N13" },
            new string[] { "N13", "N14" },
            new string[] { "N14", "N15" },
            new string[] { "N15", "N16" },
            new string[] { "N16", "N17" },
            new string[] { "N17", "N18" },
            new string[] { "N18", "N19" },
            new string[] { "N19", "N20" },
            new string[] { "N20", "N21" },
            new string[] { "N21", "N22" },
            new string[] { "N22", "N23" },
            new string[] { "N23", "N24" },
            // south bts
            new string[] { "CEN", "S1" },
            new string[] { "S1", "S2" },
            new string[] { "S2", "S3" },
            new string[] { "S3", "S4" },
            new string[] { "S4", "S5" },
            new string[] { "S5", "S6" },
            new string[] { "S6", "S7" },
            new string[] { "S7", "S8" },
            new string[] { "S8", "S9" },
            new string[] { "S9", "S10" },
            new string[] { "S10", "S11" },
            new string[] { "S11", "S12" },
            // west bts
            new string[] { "CEN", "W1" },
            // blue mrt
            new string[] { "BL01", "BL02" },
            new string[] { "BL02", "BL03" },
            new string[] { "BL03", "BL04" },
            new string[] { "BL04", "BL05" },
            new string[] { "BL05", "BL06" },
            new string[] { "BL06", "BL07" },
            new string[] { "BL07", "BL08" },
            new string[] { "BL08", "BL09" },
            new string[] { "BL09", "BL10" },
            new string[] { "BL10", "BL11" },
            new string[] { "BL11", "BL12" },
            new string[] { "BL12", "BL13" },
            new string[] { "BL13", "BL14" },
            new string[] { "BL14", "BL15" },
            new string[] { "BL15", "BL16" },
            new string[] { "BL16", "BL17" },
            new string[] { "BL17", "BL18" },
            new string[] { "BL18", "BL19" },
            new string[] { "BL19", "BL20" },
            new string[] { "BL20", "BL21" },
            new string[] { "BL21", "BL22" },
            new string[] { "BL22", "BL23" },
            new string[] { "BL23", "BL24" },
            new string[] { "BL24", "BL25" },
            new string[] { "BL25", "BL26" },
            new string[] { "BL26", "BL27" },
            new string[] { "BL27", "BL28" },
            new string[] { "BL28", "BL29" },
            new string[] { "BL29", "BL30" },
            new string[] { "BL30", "BL31" },
            new string[] { "BL31", "BL32" },

            new string[] { "BL32", "BL01" },
            new string[] { "BL33", "BL01" },
            new string[] { "BL33", "BL34" },
            new string[] { "BL34", "BL35" },
            new string[] { "BL35", "BL36" },
            new string[] { "BL36", "BL37" },
            new string[] { "BL37", "BL38" },
            // purple mrt
            new string[] { "PP01", "PP02" },
            new string[] { "PP02", "PP03" },
            new string[] { "PP03", "PP04" },
            new string[] { "PP04", "PP05" },
            new string[] { "PP05", "PP06" },
            new string[] { "PP06", "PP07" },
            new string[] { "PP07", "PP08" },
            new string[] { "PP08", "PP09" },
            new string[] { "PP09", "PP10" },
            new string[] { "PP10", "PP11" },
            new string[] { "PP11", "PP12" },
            new string[] { "PP12", "PP13" },
            new string[] { "PP13", "PP14" },
            new string[] { "PP14", "PP15" },
            new string[] { "PP15", "PP16" },
            // arl
            new string[] { "A1", "A2" },
            new string[] { "A2", "A3" },
            new string[] { "A3", "A4" },
            new string[] { "A4", "A5" },
            new string[] { "A5", "A6" },
            new string[] { "A6", "A7" },
            new string[] { "A7", "A8" },
            // intersect
            new string[] { "E4", "BL22" },
            new string[] { "A6", "BL21" },
            new string[] { "S2", "BL26" },
            new string[] { "N8", "BL13" },
            new string[] { "N9", "BL14" },
            new string[] { "PP16", "BL10" },
            new string[] { "S12", "BL34" },
            new string[] { "N2", "A8" }
        };

        public Dictionary<string, List<String>> adjacencyList = new Dictionary<String, List<String>>();

        [HttpGet("time/{start}-{end}")]
        public List<TimeSpan> GetEstimatedTime(string start, string end)
        {
            string query = @"select * from stations";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);


                    foreach (DataRow data in table.Rows)
                    {
                        stations.Add(data["station_id"].ToString());
                    }

                    void AddNode(string station)
                    {
                        adjacencyList.Add(station, new List<string>());
                    }

                    void AddEdge(string origin, string destination)
                    {
                        adjacencyList[origin].Add(destination);
                        adjacencyList[destination].Add(origin);
                    }

                    foreach (string station in stations)
                    {
                        AddNode(station);
                    }

                    foreach (string[] route in routes)
                    {
                        AddEdge(route[0], route[1]);
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            List<List<string>> FindAllRoutes(string start, string end)
            {
                List<List<string>> routes = new List<List<string>>();
                Queue<List<string>> queue = new Queue<List<string>>();
                HashSet<string> visited = new HashSet<string>();

                queue.Enqueue(new List<string> { start });

                while (queue.Count > 0)
                {
                    List<string> currentPath = queue.Dequeue();
                    string currentNode = currentPath.Last();

                    if (currentNode == end)
                    {
                        routes.Add(currentPath);
                        continue;
                    }

                    visited.Add(currentNode);
                    List<string> neighbors = adjacencyList.ContainsKey(currentNode) ? adjacencyList[currentNode] : new List<string>();

                    foreach (string neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            List<string> newPath = new List<string>(currentPath);
                            newPath.Add(neighbor);
                            queue.Enqueue(newPath);
                        }
                    }
                }

                return routes;
            }

            List<List<string>> result = FindAllRoutes(start, end);
            List<List<Models.Station>> stationsAllData = new List<List<Models.Station>>();

            foreach (List<string> routes in result)
            {
                List<Models.Station> stationsData = new List<Models.Station>();
                foreach (string route in routes)
                {
                    query = @"select * from stations where station_id = @id";
                    table = new DataTable();
                    using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                    {
                        mycon.Open();

                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@id", route);
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            Models.Station tempStation = new Models.Station();
                            tempStation.StationId = table.Rows[0][0].ToString();
                            tempStation.StationName = table.Rows[0][1].ToString();
                            tempStation.StationLine = table.Rows[0][2].ToString();
                            tempStation.StationLineColor = table.Rows[0][3].ToString();
                            tempStation.IsExtended = (bool)table.Rows[0][4];
                            tempStation.Latitude = (double)table.Rows[0][5];
                            tempStation.Longitude = (double)table.Rows[0][6];

                            stationsData.Add(tempStation);

                            myReader.Close();
                            mycon.Close();
                        }
                    }
                }
                stationsAllData.Add(stationsData);
            }

            List<TimeSpan> allTotalTime = new List<TimeSpan>();

            foreach (List<Models.Station> stations in stationsAllData)
            {
                int bCount = 0;
                int mCount = 0;
                int mpCount = 0;

                foreach (Models.Station station in stations)
                {
                    if (station.StationLine == "bts")
                    {
                        bCount += 1;
                    }
                    else if (station.StationLine == "bts" && station.IsExtended)
                    {
                        bCount += 1;
                    }
                    else if (station.StationLine == "mrt" && station.StationLineColor == "blue")
                    {
                        mCount += 1;
                    }
                    else if (station.StationLine == "mrt" && station.StationLineColor == "purple")
                    {
                        mpCount += 1;
                    }
                }

                if (stations[0].StationLine == "bts")
                {
                    bCount -= 1;
                }
                else if (stations[0].StationLine == "bts" && stations[0].IsExtended)
                {
                    bCount -= 1;
                }
                else if (stations[0].StationLine == "mrt" && stations[0].StationLineColor == "blue")
                {
                    mCount -= 1;
                }
                else if (stations[0].StationLine == "mrt" && stations[0].StationLineColor == "purple")
                {
                    mpCount -= 1;
                }

                TimeSpan timenow = DateTime.Now.TimeOfDay;
                TimeSpan totalTime = new TimeSpan(0, 0, 0);

                foreach (TimeSpan key in timetable.Keys)
                {
                    if (timenow > key)
                    {
                        totalTime = new TimeSpan(0, 0, 0);
                        for (int i = 0; i < bCount; i++)
                        {
                            totalTime += timetable[key];
                        }
                    }
                }

                TimeSpan temp = totalTime;

                foreach (TimeSpan key in blueTimetable.Keys)
                {
                    if (timenow > key)
                    {
                        totalTime = temp;
                        for (int i = 0; i < mCount; i++)
                        {
                            totalTime += blueTimetable[key];
                        }
                    }
                }

                allTotalTime.Add(totalTime);
            }

            return allTotalTime;
        }
    }
}