
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uzoma.Api.Data;

namespace Uzoma.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly UzomaDbContext _context;

        public CategoriesController(UzomaDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/categories/{slug}/products
        [HttpGet("{slug}/products")]
        public async Task<IActionResult> GetProductsByCategory(string slug)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Slug == slug);

            if (category == null)
                return NotFound($"Category '{slug}' not found");

            return Ok(category.Products);
        }
    }
}
