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
                // Category 1: Jackets
                new() { Name = "Leather Biker Jacket", Price = 220, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5", CreatedAt = seedDate, Description = "Premium leather biker jacket." },
                new() { Name = "Denim Jacket", Price = 140, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1576995853123-5a10305d93c0", CreatedAt = seedDate, Description = "Classic blue denim." },
                new() { Name = "Casual Weekend Jacket", Price = 130, CategoryId = 1, ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea", CreatedAt = seedDate, Description = "Perfect for light weather." },

                // Category 2: Hoodies
                new() { Name = "Classic Pullover Hoodie", Price = 75, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1556821840-3a63f95609a7", CreatedAt = seedDate, Description = "Soft cotton blend." },
                new() { Name = "Oversized Street Hoodie", Price = 85, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1513789178605-8930c7464bb7", CreatedAt = seedDate, Description = "Modern oversized fit." },
                new() { Name = "Minimal Zip Hoodie", Price = 70, CategoryId = 2, ImageUrl = "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633", CreatedAt = seedDate, Description = "Simple and clean." },

                // Category 3: Coats
                new() { Name = "Wool Trench Coat", Price = 260, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1516513070339-630266931ecf", CreatedAt = seedDate, Description = "Elegant winter wear." },
                new() { Name = "Long Winter Coat", Price = 310, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1539533377285-3422b00a3001", CreatedAt = seedDate, Description = "Maximum warmth." },
                new() { Name = "Smart Casual Coat", Price = 210, CategoryId = 3, ImageUrl = "https://images.unsplash.com/photo-1544022613-e87ca75a784a", CreatedAt = seedDate, Description = "Versatile style." },

                // Category 4: Sportswear
                new() { Name = "Athletic Training Jacket", Price = 95, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1517836357463-d25dfeac3438", CreatedAt = seedDate, Description = "Breathable fabric." },
                new() { Name = "Running Track Top", Price = 80, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1571008887538-b36bb32f4571", CreatedAt = seedDate, Description = "Built for speed." },
                new() { Name = "Performance Sports Hoodie", Price = 90, CategoryId = 4, ImageUrl = "https://images.unsplash.com/photo-1510017803434-a899398421b3", CreatedAt = seedDate, Description = "Moisture-wicking tech." },

                // Category 5: Casual
                new() { Name = "Everyday Casual Shirt", Price = 60, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c", CreatedAt = seedDate, Description = "Breathable linen." },
                new() { Name = "Relaxed Fit Trousers", Price = 90, CategoryId = 5, ImageUrl = "https://images.unsplash.com/photo-1624371414361-e6e8ea7c7526", CreatedAt = seedDate, Description = "Comfort for all day." },

                // Category 6: Formal
                new() { Name = "Tailored Formal Suit", Price = 480, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1598808503744-3ede93aa26c9", CreatedAt = seedDate, Description = "Premium Italian wool suit." },
                new() { Name = "Classic Blazer", Price = 230, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1507679799987-c73779587ccf", CreatedAt = seedDate, Description = "Sharp office look." },
                new() { Name = "Formal Waistcoat", Price = 160, CategoryId = 6, ImageUrl = "https://images.unsplash.com/photo-1617114919297-3c8ddb01f599", CreatedAt = seedDate, Description = "A sophisticated formal vest." },

                // Category 7: Outerwear
                new() { Name = "Puffer Jacket", Price = 180, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1545594928-140236a28186", CreatedAt = seedDate, Description = "Insulated puffer." },
                new() { Name = "Windbreaker Shell", Price = 120, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1504191467641-2815d859bb30", CreatedAt = seedDate, Description = "Water resistant." },
                new() { Name = "Lightweight Outer Shell", Price = 110, CategoryId = 7, ImageUrl = "https://images.unsplash.com/photo-1515886657613-9f3515b0c78f", CreatedAt = seedDate, Description = "Compact and light." }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}