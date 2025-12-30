using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public class UzomaDbContext : DbContext
    {
        public UzomaDbContext(DbContextOptions<UzomaDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Classic T-Shirt",
                    Description = "100% cotton, available in all sizes",
                    Price = 19.99m,
                    ImageUrl = "https://example.com/images/classic-tshirt.jpg",
                    Category = "T-Shirts",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Slim Fit Jeans",
                    Description = "Denim, slim fit style",
                    Price = 49.99m,
                    ImageUrl = "https://example.com/images/slim-fit-jeans.jpg",
                    Category = "Jeans",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Leather Jacket",
                    Description = "Genuine leather, stylish",
                    Price = 199.99m,
                    ImageUrl = "https://example.com/images/leather-jacket.jpg",
                    Category = "Jackets",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
