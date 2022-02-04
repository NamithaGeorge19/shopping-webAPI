using Microsoft.AspNetCore.Http;
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

        public CommandType CommandType { get; private set; }

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
    }
}
