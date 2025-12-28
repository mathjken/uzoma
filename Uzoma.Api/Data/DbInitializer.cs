using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(UzomaDbContext context)
        {
            if (context.Products.Any())
                return; // DB already seeded

            var products = new List<Product>
            {
                new Product { Name = "Uzoma Silk Dress", Description = "Elegant silk evening dress", Price = 120.00M, ImageUrl = "https://example.com/images/silk-dress.jpg", Category = "Dresses" },
                new Product { Name = "Uzoma Denim Jacket", Description = "Stylish blue denim jacket", Price = 75.00M, ImageUrl = "https://example.com/images/denim-jacket.jpg", Category = "Jackets" },
                new Product { Name = "Uzoma Leather Boots", Description = "Brown leather ankle boots", Price = 95.50M, ImageUrl = "https://example.com/images/leather-boots.jpg", Category = "Footwear" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
