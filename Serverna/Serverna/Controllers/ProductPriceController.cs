using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;
using System.Data;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    { 
        private readonly CursovaDbContext _context;

        public ProductPriceController(CursovaDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrices()
        {
            return await _context.ProductPrices.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrice>> GetProductPrice(int id)
        {
            var stock = await _context.ProductPrices.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string fieldName, [FromQuery] string value, [FromQuery] string comparison = "eq")
        {
            try
            {
                var parameter = Expression.Parameter(typeof(ProductPrice), "pp");
                var property = Expression.Property(parameter, fieldName);

                var propertyType = property.Type;
                var constant = Expression.Constant(Convert.ChangeType(value, propertyType));

                Expression comparisonExpression;

                switch (comparison.ToLower())
                {
                    case "gt":
                        comparisonExpression = Expression.GreaterThan(property, constant);
                        break;
                    case "lt":
                        comparisonExpression = Expression.LessThan(property, constant);
                        break;
                    case "gte":
                        comparisonExpression = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case "lte":
                        comparisonExpression = Expression.LessThanOrEqual(property, constant);
                        break;
                    case "eq":
                    default:
                        comparisonExpression = Expression.Equal(property, constant);
                        break;
                }

                var lambda = Expression.Lambda<Func<ProductPrice, bool>>(comparisonExpression, parameter);

                var result = await _context.ProductPrices.Where(lambda).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid field name, value, or comparison type: {ex.Message}");
            }
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> GetPriceByProduct(int productId)
        {
            var price = await _context.ProductPrices
                                      .Where(p => p.ProductId == productId)
                                      .ToListAsync();

            if (price == null || price.Count == 0)
            {
                return NotFound();
            }

            return price;
        }

        [HttpPost]
        public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice price)
        {
            if (price == null || price.Price <= 0 || price.ProductId <= 0 || price.EndDate == null || price.StartDate == null  || price.Quantity <= 0 || string.IsNullOrEmpty(price.BatchNumber) )
            {
                return BadRequest("Invalid Product Price data.");

            }


            if (price.EndDate > DateTime.Now)
            {
                _context.ProductPrices.Add(price);
            }
            else
            {
                return BadRequest("Wrong date or your product is already damaged");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Probuct Price: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetProductPrice), new { id = price.PriceId }, price);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPrice(int id, ProductPrice model)
        {
            var price = await _context.ProductPrices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            // Обновляем только не-null свойства
            if (model.ProductId > 0)
            {
                price.ProductId = model.ProductId;
            }
            if (model.StartDate != null)
            {
                price.StartDate = model.StartDate;
            }
            if (model.EndDate > DateTime.Now)
            {
                price.EndDate = model.EndDate;
            }
            if (model.Price > 0 )
            {
                price.Price = model.Price;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error updating Product Price: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPrice(int id)
        {
            var price = await _context.ProductPrices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            try
            {
                _context.ProductPrices.Remove(price);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error delete Product Price: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return NoContent();
        }
    }
}
