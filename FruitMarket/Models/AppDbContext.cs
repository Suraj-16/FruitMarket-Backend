using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace FruitMarket.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {

        }

        public DbSet<FruitDetail> fruitdetails { get; set; }

        public DbSet<CartDetail> cartdetails { get; set; }

        public DbSet<userDetail> userdetails { get; set; }

        public DbSet<CartItem> cartItems { get; set; }

        public DbSet<OrderDetail> orderdetails { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }
    }
}
