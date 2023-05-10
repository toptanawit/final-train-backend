using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
            new string[] { "e12", "e4" }
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

            JsonResult BFS(string start, string end)
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
                            return new JsonResult(visited);
                        }
                        if (!visited.Contains(destination))
                        {
                            visited.Add(destination);
                            queue.Enqueue(destination);
                        }
                    }
                }

                return new JsonResult(visited);
            }

            JsonResult result = BFS(start, end);

            return result;
        }
    }
}