using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EarlsBurger.Models;

namespace EarlsBurger.Data
{
    public class EarlsBurgerContext : DbContext
    {
        public EarlsBurgerContext (DbContextOptions<EarlsBurgerContext> options)
            : base(options)
        {
        }

        public DbSet<EarlsBurger.Models.FoodItem> FoodItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>().ToTable("Fooditem");
        }
    }
}
