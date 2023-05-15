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
using static System.Collections.Specialized.BitVector32;

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

            return new JsonResult(stationsAllData);
        }





        // fee
        [HttpGet("fees/{start}-{end}")]
        public List<int> GetFees(string start, string end)
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

            List<int> allFees = new List<int>();

            foreach (List<Models.Station> stations in stationsAllData)
            {
                int fees = 0;
                int bCount = 0;
                int mCount = 0;
                int mpCount = 0;
                int aCount = 0;
                bool extended = false;

                foreach (Models.Station station in stations)
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
                else
                {
                    aCount -= 1;
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

                allFees.Add(fees);
            }

            return allFees;
        }
    }
}