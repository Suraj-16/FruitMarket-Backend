using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class CartDetail
    {

        [Key]
        public int cartid { get; set; }
        [Required]
        [Range(200, Double.MaxValue, ErrorMessage ="Provide Valid userid")]
        public int userid { get; set; }
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Provide Valid fruitid")]
        public int fruitid { get; set; }
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "Quantity should be greater than 0")]
        public int qty { get; set; }
        [Required]
        public bool isremoved { get; set; }
    }
}
