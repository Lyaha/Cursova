using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serverna.Model;
using System.Linq.Expressions;
using System;

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CursovaDbContext _context;

        public CategoryController(CursovaDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Category>>> SearchCategoriesByName(string name, string comparison)
        {
            IQueryable<Category> query = _context.Categories;

            var parameter = Expression.Parameter(typeof(Category), "s");
            var property = Expression.PropertyOrField(parameter, "categoryName");
            var constant = Expression.Constant(name);

            Expression predicate = comparison.ToLower() switch
            {
                "startswith" => Expression.Call(property, "StartsWith", null, constant),
                "contains" => Expression.Call(property, "Contains", null, constant),
                _ => throw new ArgumentException("Invalid comparison type.")
            };
            //startswith не робит [completed]

            var lambda = Expression.Lambda<Func<Category, bool>>(predicate, parameter);
            query = query.Where(lambda);

            return await query.ToListAsync();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category cat)
        {
            if (cat == null || string.IsNullOrEmpty(cat.CategoryName))
            {
                return BadRequest("Invalid Category data.");
            }

            _context.Categories.Add(cat);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error creating Category: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetCategory), new { id = cat.CategoryId }, cat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category model)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.CategoryName))
            {
                cat.CategoryName = model.CategoryName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Category: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
