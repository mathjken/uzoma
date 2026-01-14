using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Models;

namespace Uzoma.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UzomaDbContext context)
        {
            // Apply migrations safely
            context.Database.Migrate();

            // --------------------
            // Seed Categories
            // --------------------
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Jackets", Slug = "jackets" },
                    new Category { Name = "Shirts", Slug = "shirts" },
                    new Category { Name = "Trousers", Slug = "trousers" },
                    new Category { Name = "Suits", Slug = "suits" },
                    new Category { Name = "Kaftans", Slug = "kaftans" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // --------------------
            // Seed Products (20)
            // --------------------
            if (!context.Products.Any())
            {
                var seedDate = DateTime.UtcNow;

                var products = new Product[]
                {
                    // Jackets (CategoryId = 1)
                    new Product {
                        Name = "Premium Leather Jacket",
                        Description = "Handcrafted premium leather jacket with modern cut",
                        Price = 219.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea",
                        CategoryId = 1,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Denim Jacket",
                        Description = "Classic blue denim jacket for casual wear",
                        Price = 129.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1512436991641-6745cdb1723f",
                        CategoryId = 1,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Winter Parka Jacket",
                        Description = "Insulated parka jacket for cold weather",
                        Price = 189.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf",
                        CategoryId = 1,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Bomber Jacket",
                        Description = "Lightweight bomber jacket with zip closure",
                        Price = 99.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1520975916090-3105956dac38",
                        CategoryId = 1,
                        CreatedAt = seedDate
                    },

                    // Shirts (CategoryId = 2)
                    new Product {
                        Name = "Classic White Shirt",
                        Description = "Formal white cotton shirt",
                        Price = 39.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1523381294911-8d3cead13475",
                        CategoryId = 2,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Casual Linen Shirt",
                        Description = "Breathable linen shirt for casual outings",
                        Price = 44.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c",
                        CategoryId = 2,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Striped Office Shirt",
                        Description = "Slim fit striped shirt for office wear",
                        Price = 49.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1600180758890-6b94519a8ba6",
                        CategoryId = 2,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Short Sleeve Shirt",
                        Description = "Relaxed short sleeve summer shirt",
                        Price = 29.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1603252109303-2751441dd157",
                        CategoryId = 2,
                        CreatedAt = seedDate
                    },

                    // Trousers (CategoryId = 3)
                    new Product {
                        Name = "Slim Fit Jeans",
                        Description = "Dark wash slim fit denim jeans",
                        Price = 59.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246",
                        CategoryId = 3,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Chino Trousers",
                        Description = "Smart casual chinos",
                        Price = 54.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c",
                        CategoryId = 3,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Formal Black Trousers",
                        Description = "Classic formal trousers for business wear",
                        Price = 69.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1584865288642-42078c1d4f6d",
                        CategoryId = 3,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Cargo Pants",
                        Description = "Utility cargo trousers with multiple pockets",
                        Price = 64.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1618354691258-92e68f0f7e52",
                        CategoryId = 3,
                        CreatedAt = seedDate
                    },

                    // Suits (CategoryId = 4)
                    new Product {
                        Name = "Classic Navy Suit",
                        Description = "Two-piece navy suit, slim fit",
                        Price = 299.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1521335629791-ce4aec67dd53",
                        CategoryId = 4,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Grey Business Suit",
                        Description = "Professional grey suit for corporate wear",
                        Price = 319.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1593032465175-481ac7f401a0",
                        CategoryId = 4,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Wedding Suit",
                        Description = "Elegant suit perfect for weddings",
                        Price = 399.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1592878904946-b3cd8ae243d0",
                        CategoryId = 4,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Double Breasted Suit",
                        Description = "Luxury double breasted tailored suit",
                        Price = 449.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1539533113208-f6df8cc8b543",
                        CategoryId = 4,
                        CreatedAt = seedDate
                    },

                    // Kaftans (CategoryId = 5)
                    new Product {
                        Name = "Traditional Kaftan",
                        Description = "African traditional kaftan attire",
                        Price = 149.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf",
                        CategoryId = 5,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Embroidered Kaftan",
                        Description = "Luxury embroidered kaftan",
                        Price = 179.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1582719478181-2c4dbe45a4b7",
                        CategoryId = 5,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Modern Kaftan",
                        Description = "Modern cut kaftan for special occasions",
                        Price = 159.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1620799139507-2a76f79c82f3",
                        CategoryId = 5,
                        CreatedAt = seedDate
                    },
                    new Product {
                        Name = "Casual Kaftan",
                        Description = "Lightweight kaftan for everyday comfort",
                        Price = 119.99m,
                        ImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6c94d9b6a1",
                        CategoryId = 5,
                        CreatedAt = seedDate
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}s