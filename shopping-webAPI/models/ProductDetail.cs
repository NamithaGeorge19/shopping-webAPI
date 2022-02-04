using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping_webAPI.models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
