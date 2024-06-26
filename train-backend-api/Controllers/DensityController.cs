﻿using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TrainSystem.Models;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TrainSystem.Controller
{
    [Route("api/density")]
    [ApiController]
    public class DensityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DensityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // for testing
        [Route("testservicedb")]
        [HttpGet]
        public JsonResult GetJsonResult()
        {
            DataTable table = new DataTable();
            string query = @"select * from density_station_new";
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

                    return new JsonResult(table);
                }
            }
        }

        [Route("testservicedb2")]
        [HttpGet]
        public JsonResult GetJsonResult2()
        {
            DataTable table = new DataTable();
            string query = @"select * from density_parkinglot_new";
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

                    return new JsonResult(table);
                }
            }
        }

        [Route("testdeleteall")]
        [HttpPost]
        public string DeleteAllRecords(string keyword)
        {
            if (keyword != "cleardata")
            {
                return "Data still hasn't been deleted";
            }

            string query = "delete from density_station_new";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Deleted all data successfully";
                }
            }
        }

        [Route("testdeleteall2")]
        [HttpPost]
        public string DeleteAllRecords2(string keyword)
        {
            if (keyword != "cleardata")
            {
                return "Data still hasn't been deleted";
            }

            string query = "delete from density_parkinglot_new";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Deleted all data successfully";
                }
            }
        }

        public Dictionary<string, int> AvgMaxPopulation = new Dictionary<string, int>()
        {
            { "lightgreen", 256 }, // 16 * 2 * 4 * 2 (door * door side * avg passenger/side * station side)
            { "darkgreen", 256 }, // 16 * 2 * 4 * 2
            { "blue", 192 }, // 12 * 2 * 4 * 2
            { "purple", 192 }, // 12 * 2 * 4 * 2
            { "red", 128 }, // 8 * 2 * 4 * 2
            { "gold", 48 }, // 4 * 2 * 3 * 2
            { "pink", 128 }, // 8 * 2 * 4 * 2
            { "yellow", 128 }, // 8 * 2 * 4 * 2
            { "darkred", 288 }, // 18 * 2 * 4 * 2
            { "lightred", 288 } // 18 * 2 * 4 * 2
        };

        // station
        [Route("station-add")]
        [HttpPost]
        public string AddStationDensityRecord(StationDensity data)
        {
            DataTable table = new DataTable();
            string selectquery = @"select * from density_station_new where user_id = @user_id and station_id = @station_id";
            string query = @"insert into density_station_new (user_id, station_id, status) values (@user_id, @station_id, @status)";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand selectcommand = new MySqlCommand(selectquery, mycon))
                {
                    selectcommand.Parameters.AddWithValue("@user_id", data.user_id);
                    selectcommand.Parameters.AddWithValue("@station_id", data.station_id);
                    myReader = selectcommand.ExecuteReader();
                    table.Load(myReader);

                    if (table.Rows.Count == 0)
                    {
                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                            myCommand.Parameters.AddWithValue("@station_id", data.station_id);
                            myCommand.Parameters.AddWithValue("@status", data.status);
                            myReader = myCommand.ExecuteReader();

                            myReader.Close();
                            mycon.Close();

                            return "Data added successfully";
                        }
                    }
                    else
                    {
                        myReader.Close();
                        mycon.Close();

                        return UpdateStationDensityRecord(data);
                    }
                }
            }
        }

        [Route("station-update")]
        [HttpPut]
        public string UpdateStationDensityRecord(StationDensity data)
        {
            string query = @"update density_station_new set status = @status where user_id = @user_id and station_id = @station_id";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                    myCommand.Parameters.AddWithValue("@station_id", data.station_id);
                    myCommand.Parameters.AddWithValue("@status", data.status);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Data updated successfully";
                }
            }
        }

        [Route("station-delete")]
        [HttpDelete]
        public string DeleteStationDensityRecord(StationDensity data)
        {
            string query = @"delete from density_station_new where user_id = @user_id and station_id = @station_id";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                    myCommand.Parameters.AddWithValue("@station_id", data.station_id);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Data deleted successfully";
                }
            }
        }

        [Route("station-calculate-density")]
        [HttpPost]
        public JsonResult CalculateStaionDensity(string station_id, string station_linecolor)
        {
            if (station_linecolor == "orange")
            {
                var jsonData1 = new
                {
                    station_id = station_id,
                    result = "out of service",
                };

                return new JsonResult(jsonData1);
            }

            string query = @"select * from density_station_new where station_id = @station_id and status = true";
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

            int passengers = table.Rows.Count;
            int threshold = AvgMaxPopulation[station_linecolor];
            string result = "no data";

            if (passengers <= threshold / 2) {
                result = "low";
            }
            else if (passengers <= threshold)
            {
                result = "medium";
            }
            else
            {
                result = "high";
            }

            var jsonData = new
            {
                station_id = station_id,
                result = result,
            };

            return new JsonResult(jsonData);
        }

        // parking lot
        [Route("parkinglot-add")]
        [HttpPost]
        public string AddParkingLotDensityRecord(ParkingLotDensity data)
        {
            DataTable table = new DataTable();
            string selectquery = @"select * from density_parkinglot_new where user_id = @user_id and parking_id = @parking_id";
            string query = @"insert into density_parkinglot_new (user_id, parking_id, status) values (@user_id, @parking_id, @status)";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand selectcommand = new MySqlCommand(selectquery, mycon))
                {
                    selectcommand.Parameters.AddWithValue("@user_id", data.user_id);
                    selectcommand.Parameters.AddWithValue("@parking_id", data.parking_id);
                    myReader = selectcommand.ExecuteReader();
                    table.Load(myReader);

                    if (table.Rows.Count == 0)
                    {
                        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                        {
                            myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                            myCommand.Parameters.AddWithValue("@parking_id", data.parking_id);
                            myCommand.Parameters.AddWithValue("@status", data.status);
                            myReader = myCommand.ExecuteReader();

                            myReader.Close();
                            mycon.Close();

                            return "Data added successfully";
                        }
                    }
                    else
                    {
                        myReader.Close();
                        mycon.Close();

                        return UpdateParkingLotDensityRecord(data);
                    }
                }
            }
        }

        [Route("parkinglot-update")]
        [HttpPut]
        public string UpdateParkingLotDensityRecord(ParkingLotDensity data)
        {
            string query = @"update density_parkinglot_new set status = @status where user_id = @user_id and parking_id = @parking_id";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                    myCommand.Parameters.AddWithValue("@parking_id", data.parking_id);
                    myCommand.Parameters.AddWithValue("@status", data.status);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Data updated successfully";
                }
            }
        }

        [Route("parkinglot-delete")]
        [HttpDelete]
        public string DeleteParkingLotDensityRecord(ParkingLotDensity data)
        {
            string query = @"delete from density_parkinglot_new where user_id = @user_id and parking_id = @parking_id";
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_id", data.user_id);
                    myCommand.Parameters.AddWithValue("@parking_id", data.parking_id);
                    myReader = myCommand.ExecuteReader();

                    myReader.Close();
                    mycon.Close();

                    return "Data deleted successfully";
                }
            }
        }

        [Route("parkinglot-calculate-density")]
        [HttpPost]
        public JsonResult CalculateParkingLotDensity(string parking_id, int lot)
        {
            string query = @"select * from density_parkinglot_new where parking_id = @parking_id and status = true";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@parking_id", parking_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            int passengers = table.Rows.Count;
            int threshold = lot;
            string result = "no data";

            if (passengers <= threshold / 3)
            {
                result = "low";
            }
            else if (passengers <= threshold * 2 / 3)
            {
                result = "medium";
            }
            else
            {
                result = "high";
            }

            var jsonData = new
            {
                parking_id = parking_id,
                result = result,
            };

            return new JsonResult(jsonData);
        }
    }
}