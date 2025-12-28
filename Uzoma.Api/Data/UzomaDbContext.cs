using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public class UzomaDbContext : DbContext
    {
        public UzomaDbContext(DbContextOptions<UzomaDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fix for Price decimal precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
