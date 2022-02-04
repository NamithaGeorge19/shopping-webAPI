using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopping_webAPI.models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace shopping_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            List<Product> productList = new List<Product>();
            string connectionString = _configuration.GetConnectionString("ShoppingAppConnection");
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetProducts", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataset);
            }
            var test = dataset.Tables[0].Rows.Count;
            for (int i= 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataset.Tables[0].Rows[i]["Id"]);
                product.Name = dataset.Tables[0].Rows[i]["Name"].ToString();
                productList.Add(product);
            }
            return productList;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product varProduct = new Product();
            varProduct = ProductDAL.GetProductDetails(id);
            return varProduct;
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
