using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TrainSystem.Models;

namespace TrainSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RouteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from routes";
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

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Models.Route route)
        {
            string query = @"insert into routes (route_start, route_end, tag, user_saved) values (@start, @end, @tag, @user) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user", route.UserSaved);
                    myCommand.Parameters.AddWithValue("@start", route.RouteStart);
                    myCommand.Parameters.AddWithValue("@end", route.RouteEnd);
                    myCommand.Parameters.AddWithValue("@tag", route.Tag);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Models.Route route)
        {
            string query = @"update routes set route_start = @start, route_end = @end, tag = @tag where username = @user";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@username", route.UserSaved);
                    myCommand.Parameters.AddWithValue("@start", route.RouteStart);
                    myCommand.Parameters.AddWithValue("@end", route.RouteEnd);
                    myCommand.Parameters.AddWithValue("@tag", route.Tag);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from routes where route_id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Delete Successfully");
        }

        // bfs

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
            new string[] { "N5", "N6" },
            new string[] { "N6", "N7" },
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
            // west bts
            new string[] { "CEN", "W1" },
            // blue mrt
            new string[] { "N1", "N2" },
            new string[] { "N2", "N3" },
            new string[] { "N3", "N4" },
            new string[] { "N4", "N5" },
            new string[] { "N5", "N6" },
            new string[] { "N6", "N7" },
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
            new string[] { "N24", "N25" },
            new string[] { "N25", "N26" },
            new string[] { "N26", "N27" },
            new string[] { "N27", "N28" },
            new string[] { "N28", "N29" },
            new string[] { "N29", "N30" },
            new string[] { "N30", "N31" },
            new string[] { "N31", "N32" },
            new string[] { "N32", "N33" },
            new string[] { "N33", "N34" },
            new string[] { "N34", "N35" },
            new string[] { "N35", "N36" },
            new string[] { "N36", "N37" },
            new string[] { "N37", "N38" },
            // purple mrt
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
            // arl
            new string[] { "E1", "E2" },
            new string[] { "E2", "E3" },
            new string[] { "E3", "E4" },
            new string[] { "E4", "E5" },
            new string[] { "E5", "E6" },
            new string[] { "E6", "E7" },
            new string[] { "E7", "E8" },
            // intersect
            new string[] { "E1", "E2" },
            new string[] { "E2", "E3" },
            new string[] { "E3", "E4" },
            new string[] { "E4", "E5" },
            new string[] { "E5", "E6" },
            new string[] { "E6", "E7" },
            new string[] { "E7", "E8" },
            new string[] { "E8", "E9" },
            new string[] { "E9", "E10" }
        };

        public Dictionary<string, List<String>> adjacencyList = new Dictionary<String, List<String>>();

        [HttpGet("{start}-{end}")]
        public JsonResult GetAllPossibleRoutes(string start, string end)
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

            for (int i=0; i<result.Count; i++)
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

            return new JsonResult(stationsData);
        }

        [HttpGet("fees/{start}-{end}")]
        public int GetFees(string start, string end)
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
            bool extended = false;
            int fees = 0;

            foreach (Models.Station station in stationsData)
            {
                if (station.StationLine == "bts")
                {
                    bCount += 1;
                }
                else if (station.StationLine == "bts" && station.IsExtended)
                {
                    bCount += 1;
                    extended = true;
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

            if (bCount == 1)
            {
                fees += 16;
                if (extended) { fees += 1; }
            }
            else if (bCount == 2)
            {
                fees += 23;
                if (extended) { fees += 2; }
            }
            else if (bCount == 3)
            {
                fees += 26;
                if (extended) { fees += 2; }
            }
            else if (bCount == 4)
            {
                fees += 30;
                if (extended) { fees += 2; }
            }
            else if (bCount == 5)
            {
                fees += 33;
                if (extended) { fees += 2; }
            }
            else if (bCount == 6)
            {
                fees += 37;
                if (extended) { fees += 3; }
            }
            else if (bCount == 7)
            {
                fees += 40;
                if (extended) { fees += 3; }
            }
            else if (bCount >= 8)
            {
                fees += 44;
                if (extended) { fees += 3; }
            }



            if (mCount == 1)
            {
                fees += 16;
            }
            else if (mCount == 2)
            {
                fees += 19;
            }
            else if (mCount == 3)
            {
                fees += 21;
            }
            else if (mCount == 4)
            {
                fees += 23;
            }
            else if (mCount == 5)
            {
                fees += 25;
            }
            else if (mCount == 6)
            {
                fees += 28;
            }
            else if (mCount == 7)
            {
                fees += 30;
            }
            else if (mCount == 8)
            {
                fees += 32;
            }
            else if (mCount == 9)
            {
                fees += 35;
            }
            else if (mCount == 10)
            {
                fees += 37;
            }
            else if (mCount == 11)
            {
                fees += 39;
            }
            else if (mCount >= 12)
            {
                fees += 42;
            }



            if (mpCount == 1)
            {
                fees += 17;
            }
            else if (mpCount == 2)
            {
                fees += 20;
            }
            else if (mpCount == 3)
            {
                fees += 23;
            }
            else if (mpCount == 4)
            {
                fees += 25;
            }
            else if (mpCount == 5)
            {
                fees += 27;
            }
            else if (mpCount == 6)
            {
                fees += 30;
            }
            else if (mpCount == 7)
            {
                fees += 33;
            }
            else if (mpCount == 8)
            {
                fees += 36;
            }
            else if (mpCount == 9)
            {
                fees += 38;
            }
            else if (mpCount == 10)
            {
                fees += 40;
            }
            else if (mpCount >= 11)
            {
                fees += 42;
            }



            if (aCount == 1)
            {
                fees += 15;
            }
            else if (aCount == 2)
            {
                fees += 20;
            }
            else if (aCount == 3)
            {
                fees += 25;
            }
            else if (aCount == 4)
            {
                fees += 30;
            }
            else if (aCount == 5)
            {
                fees += 35;
            }
            else if (aCount == 6)
            {
                fees += 40;
            }
            else if (aCount == 7)
            {
                fees += 45;
            }



            return fees;
        }
    }
}