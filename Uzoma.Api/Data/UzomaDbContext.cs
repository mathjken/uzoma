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

            // Configure decimal precision
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>().Property(i => i.UnitPrice).HasPrecision(18, 2);

            // Relationships
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

            // Static Seed Data (Categories Only)
            // It is safer to seed Categories here so IDs 1-7 always exist for your products
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Jackets", Slug = "jackets" },
                new Category { Id = 2, Name = "Hoodies", Slug = "hoodies" },
                new Category { Id = 3, Name = "Coats", Slug = "coats" },
                new Category { Id = 4, Name = "Sportswear", Slug = "sportswear" },
                new Category { Id = 5, Name = "Casual", Slug = "casual" },
                new Category { Id = 6, Name = "Formal", Slug = "formal" },
                new Category { Id = 7, Name = "Outerwear", Slug = "outerwear" }
            );
        }
    }
}