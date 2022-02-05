﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using shopping_webAPI.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace shopping_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public SellerController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        [HttpGet]
        public IEnumerable<Seller> GetSellerList()
        {
            List<Seller> SellerList = new List<Seller>();
            string connectionString = configuration.GetConnectionString("ShoppingAppConnection");
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand SqlCommand = new SqlCommand("GetSellerList", connection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlCommand);
                dataAdapter.Fill(dataSet);
            }
            for (int i = 0; i< dataSet.Tables[0].Rows.Count; i++)
            {
                Seller Seller = new Seller();
                Seller.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Id"]);
                Seller.Name = Convert.ToString(dataSet.Tables[0].Rows[i]["Name"]);
                SellerList.Add(Seller);
            }
            return SellerList;
        }

        [HttpGet("{id}")]
        public SellerDetail GetSellerDetail(int Id)
        {
            SellerDetail SellerDetail = new SellerDetail();
            string connectionString = configuration.GetConnectionString("ShoppingAppConnection");
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand SqlCommand = new SqlCommand("GetSellerById", connection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Id;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlCommand);
                dataAdapter.Fill(dataSet);
            }
            SellerDetail.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
            SellerDetail.Name = Convert.ToString(dataSet.Tables[0].Rows[0]["Name"]);
            SellerDetail.Phone = Convert.ToString(dataSet.Tables[0].Rows[0]["Phone"]);
            SellerDetail.Email = Convert.ToString(dataSet.Tables[0].Rows[0]["Email"]);
            return SellerDetail;

        }
    }
}
