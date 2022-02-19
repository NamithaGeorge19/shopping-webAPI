using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopping_webAPI.models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace shopping_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration varConfiguration;

        public ProductController(IConfiguration configuration)
        {
            varConfiguration = configuration;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> GetProductList()
        {
            List<Product> productList = new List<Product>();
            string connectionString = varConfiguration.GetConnectionString("ShoppingAppConnection");
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetProducts", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataset);
            }
            for (int i= 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataset.Tables[0].Rows[i]["Id"]);
                product.Name = dataset.Tables[0].Rows[i]["Name"].ToString();
                product.Price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["Price"]);
                productList.Add(product);
            }
            return productList;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ProductDetail GetProductDetail(int id)
        {
            ProductDetail product = new ProductDetail();
            string connectionString = varConfiguration.GetConnectionString("ShoppingAppConnection");
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetProductById", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataSet);
                product.Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Id"]);
                product.Name = dataSet.Tables[0].Rows[0]["Name"].ToString();
                product.Price = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["Price"]);
                product.Quantity = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Quantity"]);
            }
            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public void InsertProduct([FromBody] ProductDetail productDetail)
        {
            string connectionString = varConfiguration.GetConnectionString("ShoppingAppConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand Command = new SqlCommand("insertProduct", connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = productDetail.Name;
                Command.Parameters.AddWithValue("@Price", SqlDbType.Decimal).Value = productDetail.Price;
                Command.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = productDetail.Quantity;
                Command.ExecuteNonQuery();


            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void UpdateProduct(int id, [FromBody] ProductDetail productDetail)
        {
            string connectionString = varConfiguration.GetConnectionString("ShoppingAppConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand Command = new SqlCommand("updateProduct", connection);
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                Command.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = productDetail.Name;
                Command.Parameters.AddWithValue("@Price", SqlDbType.Decimal).Value = productDetail.Price;
                Command.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = productDetail.Quantity;
                Command.ExecuteNonQuery();
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            string connectionString = varConfiguration.GetConnectionString("ShoppingAppConnection");
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}
