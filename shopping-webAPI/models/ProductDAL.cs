using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace shopping_webAPI.models
{
    public class ProductDAL
    {
        private readonly IConfiguration _configuration;

        public ProductDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string connectionString = _configuration.GetConnectionString("ShoppingAppConnection");
            return products;
        }

        public static Product GetProductDetails(int id)
        {
            Product product = new Product();

            return product;
        }

        public static Product InsertProductDetails(Product product)
        {
            //call Stored procedure
            return product;
        }

        public static Product UpdateProductDetails(int id)
        {
            Product product = new Product();

            return product;
        }

        public static bool DeleteProductDetails(int id)
        {
            
            return true;
        }
    }
}
