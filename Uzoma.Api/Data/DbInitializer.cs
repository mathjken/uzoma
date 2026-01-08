using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UzomaDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // --------------------
            // Seed Categories
            // --------------------
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Id = 1, Name = "Jackets", Slug = "jackets" },
                    new Category { Id = 2, Name = "Shirts", Slug = "shirts" },
                    new Category { Id = 3, Name = "Trousers", Slug = "trousers" },
                    new Category { Id = 4, Name = "Suits", Slug = "suits" },
                    new Category { Id = 5, Name = "Kaftans", Slug = "kaftans" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // --------------------
            // Seed Products
            // --------------------
            if (!context.Products.Any())
            {
                var seedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

                var products = new Product[]
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Leather Jacket",
                        Description = "Genuine leather jacket",
                        Price = 199.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea",
                        CategoryId = 1, // Jackets
                        CreatedAt = seedDate
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Classic T-Shirt",
                        Description = "100% cotton, available in all sizes",
                        Price = 19.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1523381294911-8d3cead13475",
                        CategoryId = 2, // Shirts
                        CreatedAt = seedDate
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Slim Fit Jeans",
                        Description = "Denim, slim fit style",
                        Price = 49.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246",
                        CategoryId = 3, // Trousers
                        CreatedAt = seedDate
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // --------------------
            // You can also seed Users, Orders, etc. here if needed
            // --------------------
        }
    }
}
