using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public class UzomaDbContext : DbContext
    {
        public UzomaDbContext(DbContextOptions<UzomaDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------
            // Configure decimal precision
            // --------------------
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.UnitPrice)
                .HasPrecision(18, 2);

            // --------------------
            // Relationships
            // --------------------
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // --------------------
            // Seed data
            // --------------------
            var seedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Jackets", Slug = "jackets" },
                new Category { Id = 2, Name = "Shirts", Slug = "shirts" },
                new Category { Id = 3, Name = "Trousers", Slug = "trousers" },
                new Category { Id = 4, Name = "Suits", Slug = "suits" },
                new Category { Id = 5, Name = "Kaftans", Slug = "kaftans" }
            );

            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Leather Jacket",
                    Description = "Genuine leather jacket",
                    Price = 199.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea",
                    CategoryId = 1, // Link to Jackets
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = 2,
                    Name = "Classic T-Shirt",
                    Description = "100% cotton, available in all sizes",
                    Price = 19.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1523381294911-8d3cead13475",
                    CategoryId = 2, // Link to Shirts
                    CreatedAt = seedDate
                },
                new Product
                {
                    Id = 3,
                    Name = "Slim Fit Jeans",
                    Description = "Denim, slim fit style",
                    Price = 49.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246",
                    CategoryId = 3, // Link to Trousers
                    CreatedAt = seedDate
                }
            );
        }
    }
}
