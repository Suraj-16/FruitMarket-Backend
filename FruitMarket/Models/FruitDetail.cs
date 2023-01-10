using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class FruitDetail
    {
        [Key]
        public int fruitid { get; set; }
        [Required]
        public string fruitname { get; set; }
        [Required]
        public string fruitimg { get; set; }
        [Required]
        public decimal fruitprice { get; set; }
        [Required]
        public string fruitdes { get; set; }
    }
}
