using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EarlsBurger.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

namespace EarlsBurger.Data
{
    public class EarlsBurgerContext : IdentityDbContext
    {
        public EarlsBurgerContext (DbContextOptions<EarlsBurgerContext> options)
            : base(options)
        {
        }

        public DbSet<EarlsBurger.Models.FoodItem> FoodItems { get; set; } = default!;

        public class CheckoutCustomer
           {
             [Key]
             [StringLength(50)]
             public string Email { get; set; }
             [StringLength(50)]
             public string Name { get; set; }
             public int BasketID { get; set; }
             
           }

        public class Basket
          {
            [Key]
            public int BasketID { get; set; }
          }

        public class BasketItem
          {
            [Required]
            public int StockID { get; set; }
            [Required]
            public int BasketID { get; set; }
            [Required]
            public int Quantity { get; set; }
          }
        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; } = default!;
        public DbSet<Basket> Baskets { get; set; } = default!;
        public DbSet<BasketItem> BasketItems { get; set; } = default!;
        public DbSet<OrderHistory> OrderHistories { get; set; } = default!;
        public DbSet<OrderItems> OrderItems { get; set; } = default!;
        [NotMapped]
        public DbSet<CheckoutItem> CheckoutItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FoodItem>().ToTable("Fooditem");
            
            modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });
            modelBuilder.Entity<OrderItems>().HasKey(t => new { t.StockID, t.OrderNo });
       }
    }
}