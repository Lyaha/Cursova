using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serverna.Model;
using System.Data;
using System.Linq.Expressions;

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CursovaDbContext _context;
        private readonly IFileProvider _fileProvider;

        public ProductController(CursovaDbContext context, IFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("image/{productId}")]
        public IActionResult GetProductImage(int productId)
        {
            var filePath = Path.Combine("wwwroot", "image", "products", $"prod{productId}.png");
            var file = _fileProvider.GetFileInfo(filePath);

            if (!file.Exists)
            {
                return NotFound("Image not found");
            }

            var readStream = file.CreateReadStream();
            var mimeType = "image/png";
            return new FileStreamResult(readStream, mimeType);
        }

        [HttpDelete("image/{productId}")]
        public IActionResult DeleteProductImage(int productId)
        {
            try
            {
                var imagePath = Path.Combine("wwwroot", "image", "products", $"prod{productId}.png");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                    return Ok("Product image deleted successfully.");
                }
                else
                {
                    return NotFound("Product image not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting product image: {ex.Message}");
            }
        }

        [HttpPost("upload/{productId}")]
        public async Task<IActionResult> UploadImage(int productId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadPath = Path.Combine("wwwroot", "image", "products");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, $"prod{productId}.png");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { Path = $"/image/products/prod{productId}.png" });
        }

        [HttpGet("columns")]
        public IActionResult GetColumns()
        {
            try
            {
                var columns = typeof(Product).GetProperties()
                        .Select(p =>
                        {
                            var propertyType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                            return new { p.Name, DataType = propertyType.Name };
                        })
                        .ToList();

                var filteredColumns = columns.Where(c => c.DataType == "Int32" ||
                                                         c.DataType == "String" ||
                                                         c.DataType == "Decimal" ||
                                                         c.DataType == "DateTime")
                                             .ToList();

                return Ok(filteredColumns);
            }
            catch (Exception ex)
            {
                return BadRequest($"Some error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var prod = await _context.Products.FindAsync(id);

            if (prod == null)
            {
                return NotFound();
            }

            return prod;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string fieldName, [FromQuery] string value, [FromQuery] string comparison = "eq")
        {
            //переделанный поиск propertyType принимает нормальное значение 
            //првоерить все типы поиска:
            //gt+ lt+ gte+ lte+ eq+  [completed]
            //поменять в других контр. поиск : 0 из X 
            try
            {
                var parameter = Expression.Parameter(typeof(Product), "pp");
                var property = Expression.Property(parameter, fieldName);

                var propertyType = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
                //Task 7 [completed]
                //принимает значение System.Nullable`1 -> fildname правильный, так же и с параметром, разницы нет писать с большой или с маленькой, нужно перечитать документацию!  

                var convertedValue = Convert.ChangeType(value, propertyType);
                var constant = Expression.Constant(convertedValue, propertyType);

                Expression nonNullableProperty = property;
                if (propertyType != property.Type)
                {
                    nonNullableProperty = Expression.Convert(property, propertyType);
                }

                Expression comparisonExpression;

                switch (comparison.ToLower())
                {
                    case "gt":
                        comparisonExpression = Expression.GreaterThan(nonNullableProperty, constant);
                        break;
                    case "lt":
                        comparisonExpression = Expression.LessThan(nonNullableProperty, constant);
                        break;
                    case "gte":
                        comparisonExpression = Expression.GreaterThanOrEqual(nonNullableProperty, constant);
                        break;
                    case "lte":
                        comparisonExpression = Expression.LessThanOrEqual(nonNullableProperty, constant);
                        break;
                    case "eq":
                    default:
                        comparisonExpression = Expression.Equal(nonNullableProperty, constant);
                        break;
                }

                var lambda = Expression.Lambda<Func<Product, bool>>(comparisonExpression, parameter);
                var result = await _context.Products.Where(lambda).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid field name, value, or comparison type: {ex.Message}");
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int categoryId)
        {
            var prod = await _context.Products
                                      .Where(s => s.CategoryId == categoryId)
                                      .ToListAsync();

            if (prod == null || prod.Count == 0)
            {
                return NotFound();
            }

            return prod;
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetStockBySupplier(int supplierId)
        {
            var prod = await _context.Products
                                      .Where(s => s.SupplierId == supplierId)
                                      .ToListAsync();

            if (prod == null || prod.Count == 0)
            {
                return NotFound();
            }

            return prod;
        }

        [HttpGet("searchtext")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct(string fieldName, string value, string comparison)
        {
            IQueryable<Product> query = _context.Products;

            var parameter = Expression.Parameter(typeof(Product), "s");
            var property = Expression.PropertyOrField(parameter, fieldName);
            var constant = Expression.Constant(value);

            Expression predicate = comparison.ToLower() switch
            {
                "startswith" => Expression.Call(property, "StartsWith", null, constant),
                "contains" => Expression.Call(property, "Contains", null, constant),
                _ => throw new ArgumentException("Invalid comparison type.")
            };
            //startswith не робит [completed]

            var lambda = Expression.Lambda<Func<Product, bool>>(predicate, parameter);
            query = query.Where(lambda);

            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product prod)
        {
            if (prod == null || string.IsNullOrEmpty(prod.ProductName) || string.IsNullOrEmpty(prod.Description) || prod.SupplierId <= 0 || prod.CategoryId <= 0 || prod.StockQuantity <= 0 || prod.UnitPrice <= 0)

            {
                return BadRequest("Invalid product data.");
            }

            prod.LastUpdated = DateTime.Now;
            _context.Products.Add(prod);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetProduct), new { id = prod.ProductId }, prod);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product model)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }


            if (model.CategoryId > 0)
            {
                prod.CategoryId = model.CategoryId;
            }
            if (model.SupplierId > 0)
            {
                prod.SupplierId = model.SupplierId;
            }
            if (DateTime.Now != prod.LastUpdated)
            {
                prod.LastUpdated = DateTime.Now;
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                prod.Description = model.Description;
            }
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                prod.ProductName= model.ProductName;
            }
            if (model.UnitPrice > 0)
            {
                prod.UnitPrice = model.UnitPrice;
            }
            if(model.StockQuantity >= 0)
            {
                prod.StockQuantity = model.StockQuantity;
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
