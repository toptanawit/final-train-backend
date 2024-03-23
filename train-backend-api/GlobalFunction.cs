//using Microsoft.AspNetCore.Components.Routing;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Routing;
//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using TrainSystem.Models;

//public static class GlobalFunction
//{
//    private readonly IConfiguration _configuration;
//    public GlobalFunction(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public static int test()
//    {
//        return 1;
//    }

//    public JsonResult GetAllStations()
//    {
//        string query = @"select * from stations";
//        DataTable table = new DataTable();
//        string sqlDataSource = _configuration.GetConnectionString("TrainAppCon");
//        MySqlDataReader myReader;
//        using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
//        {
//            mycon.Open();
//            using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
//            {
//                myReader = myCommand.ExecuteReader();
//                table.Load(myReader);

//                myReader.Close();
//                mycon.Close();
//            }
//        }

//        return new JsonResult(table);
//    }
//}

