using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class OrderDetail
    {
        [Key]
        public int orderid { get; set; }
        [Required]
        public int userid { get; set; }
        [Required]
        public int fruitid { get; set; }
        [Required]
        public int qty { get; set; }
        [Required]
        public bool isremoved { get; set; }
    }
}
