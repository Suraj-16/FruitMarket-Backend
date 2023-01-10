using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class OrderItem
    {
        [Key]
        public int orderid { get; set; }
        public int qty { get; set; }
        public string fruitname { get; set; }
        public decimal fruitprice { get; set; }
        public string fruitimg { get; set; }
        public string fruitdes { get; set; }
    }
}
