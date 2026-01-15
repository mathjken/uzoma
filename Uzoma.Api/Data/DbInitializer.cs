using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UzomaDbContext context)
        {
            // Apply any pending migrations
            context.Database.Migrate();

            // Only seed products if the table is empty
            if (context.Products.Any()) return;

            var seedDate = DateTime.UtcNow;

            var products = new List<Product>
            {
                // Jackets (CategoryId 1)
                new() { Name = "Leather Biker Jacket", Price = 220, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1520975916090-3105956dac38", CreatedAt = seedDate, Description = "Premium leather biker jacket." },
                new() { Name = "Denim Jacket", Price = 140, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1542060748-10c28b62716b", CreatedAt = seedDate, Description = "Classic blue denim." },
                new() { Name = "Casual Weekend Jacket", Price = 130, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246", CreatedAt = seedDate, Description = "Perfect for light weather." },

                // Hoodies (CategoryId 2)
                new() { Name = "Classic Pullover Hoodie", Price = 75, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1618354691321-75e01c4b56a2", CreatedAt = seedDate, Description = "Soft cotton blend." },
                new() { Name = "Oversized Street Hoodie", Price = 85, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1593032465175-481ac7f4014f", CreatedAt = seedDate, Description = "Modern oversized fit." },
                new() { Name = "Minimal Zip Hoodie", Price = 70, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1600180758890-6b94519a8ba6", CreatedAt = seedDate, Description = "Simple and clean." },

                // Coats (CategoryId 3)
                new() { Name = "Wool Trench Coat", Price = 260, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1512436991641-6745cdb1723f", CreatedAt = seedDate, Description = "Elegant winter wear." },
                new() { Name = "Long Winter Coat", Price = 310, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1542060748-10c28b62716b", CreatedAt = seedDate, Description = "Maximum warmth." },
                new() { Name = "Smart Casual Coat", Price = 210, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c", CreatedAt = seedDate, Description = "Versatile style." },

                // Sportswear (CategoryId 4)
                new() { Name = "Athletic Training Jacket", Price = 95, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1592878904946-b3cd8ae243d0", CreatedAt = seedDate, Description = "Breathable fabric." },
                new() { Name = "Running Track Top", Price = 80, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1600180758890-6b94519a8ba6", CreatedAt = seedDate, Description = "Built for speed." },
                new() { Name = "Performance Sports Hoodie", Price = 90, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1592878904946-b3cd8ae243d0", CreatedAt = seedDate, Description = "Moisture-wicking tech." },

                // Casual (CategoryId 5)
                new() { Name = "Everyday Casual Shirt", Price = 60, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c", CreatedAt = seedDate, Description = "Breathable linen." },
                new() { Name = "Relaxed Fit Trousers", Price = 90, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1585386959984-a41552231693", CreatedAt = seedDate, Description = "Comfort for all day." },

                // Formal (CategoryId 6)
                new() { Name = "Tailored Formal Suit", Price = 480, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1593030761757-71fae45fa0e7", CreatedAt = seedDate, Description = "Premium Italian wool." },
                new() { Name = "Classic Blazer", Price = 230, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1521334884684-d80222895322", CreatedAt = seedDate, Description = "Sharp office look." },
                new() { Name = "Formal Waistcoat", Price = 160, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1593030761757-71fae45fa0e7", CreatedAt = seedDate, Description = "The finishing touch." },

                // Outerwear (CategoryId 7)
                new() { Name = "Puffer Jacket", Price = 180, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1617957743043-3d85b9e2f14b", CreatedAt = seedDate, Description = "Insulated puffer." },
                new() { Name = "Windbreaker Shell", Price = 120, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1514996937319-344454492b37", CreatedAt = seedDate, Description = "Water resistant." },
                new() { Name = "Lightweight Outer Shell", Price = 110, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1514996937319-344454492b37", CreatedAt = seedDate, Description = "Compact and light." }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}