using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Uzoma.Api.Data;
using Uzoma.Api.Models;

namespace Uzoma.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize] // 🔐 All endpoints require a valid JWT
    public class OrdersController : ControllerBase
    {
        private readonly UzomaDbContext _context;

        public OrdersController(UzomaDbContext context)
        {
            _context = context;
        }

        // ----------------------------
        // Helper: Extract UserId from JWT
        // ----------------------------
        // This ensures users can ONLY act as themselves
        // (prevents spoofing UserId from request body)
        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        // ----------------------------
        // CREATE ORDER (Authenticated User)
        // ----------------------------
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            // UserId is taken from JWT, NOT from the client
            var userId = GetUserId();

            var order = new Order
            {
                UserId = userId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            // Build order items from products in the database
            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return BadRequest("Invalid product");

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                // Calculate order total securely on the server
                order.TotalAmount += product.Price * item.Quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // ----------------------------
        // GET CURRENT USER'S ORDERS
        // ----------------------------
        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();

            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Ok(orders);
        }

        // ----------------------------
        // ADMIN: GET ALL ORDERS
        // ----------------------------
        [Authorize(Roles = "Admin")] // 🔐 Admin-only access
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Include(o => o.User)
                .ToListAsync();

            return Ok(orders);
        }
    }
}
