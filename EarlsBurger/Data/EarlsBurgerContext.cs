using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FoodItem>().ToTable("Fooditem");
        }
    }
}