using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class CartItem
    {
        [Key]
        public int cartid { get; set; }
        public string fruitname { get; set; }
        public string fruitimg { get; set; }
        public decimal fruitprice { get; set; }
        public int qty { get; set; }
    }
}
