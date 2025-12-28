using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Data;
using Uzoma.Api.Models;

namespace Uzoma.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly UzomaDbContext _context;

        public ProductsController(UzomaDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}
