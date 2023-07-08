using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Data.Configrations;

namespace Shop.Data
{
    public class ShopDbContext:IdentityDbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options)
        {
            
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandConfigration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
