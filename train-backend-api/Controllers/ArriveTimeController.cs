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
            new string[] { "e12", "e4" }
            // add more
        };

        public Dictionary<string, List<String>> adjacencyList = new Dictionary<String, List<String>>();

        [HttpGet("time/{start}-{end}")]
        public TimeSpan GetEstimatedTime(string start, string end)
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

            List<string> BFS(string start, string end)
            {
                var visited = new HashSet<string>();
                var queue = new Queue<string>();
                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    var station = queue.Dequeue();
                    var destinations = adjacencyList[station];

                    foreach (var destination in destinations)
                    {
                        if (destination == end)
                        {
                            visited.Add(destination);
                            return new List<string>(visited);
                        }
                        if (!visited.Contains(destination))
                        {
                            visited.Add(destination);
                            queue.Enqueue(destination);
                        }
                    }
                }

                List<string> result = new List<string>(visited);
                return result;
            }

            List<string> result = BFS(start, end);
            List<Models.Station> stationsData = new List<Models.Station>();

            for (int i = 0; i < result.Count; i++)
            {
                query = @"select * from stations where station_id = @id";
                table = new DataTable();
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();

                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@id", result[i]);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        Models.Station tempStation = new Models.Station();
                        tempStation.StationId = table.Rows[i][0].ToString();
                        tempStation.StationName = table.Rows[i][1].ToString();
                        tempStation.StationLine = table.Rows[i][2].ToString();
                        tempStation.StationLineColor = table.Rows[i][3].ToString();
                        tempStation.IsExtended = (bool)table.Rows[i][4];

                        stationsData.Add(tempStation);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            }

            int bCount = 0;
            int mCount = 0;
            int mpCount = 0;
            int aCount = 0;

            foreach (Models.Station station in stationsData)
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
                else
                {
                    aCount += 1;
                }
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
                    for (int i=0; i<mCount; i++)
                    {
                        totalTime += blueTimetable[key];
                    }
                }
            }

            // cant calculate arl time

            return totalTime;
        }
    }
}