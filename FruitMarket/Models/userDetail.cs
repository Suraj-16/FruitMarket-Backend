using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitMarket.Models
{
    public class userDetail
    {
        [Key]
        public int userid { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string address1 { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string district { get; set; }
        [Required]
        public string statename { get; set; }
        [Required]
        public int pincode { get; set; }
    }
}
