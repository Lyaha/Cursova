using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serverna.Model;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly CursovaDbContext _context;

        public SuppliersController(CursovaDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var stock = await _context.Suppliers.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpGet("searchtext")]
        public async Task<ActionResult<IEnumerable<Supplier>>> SearchSuppliers(string fieldName, string value, string comparison)
        {
            IQueryable<Supplier> query = _context.Suppliers;

            var parameter = Expression.Parameter(typeof(Supplier), "s");
            var property = Expression.PropertyOrField(parameter, fieldName);
            var constant = Expression.Constant(value);

            Expression predicate = comparison.ToLower() switch
            {
                "startswith" => Expression.Call(property, "StartsWith", null, constant),
                "contains" => Expression.Call(property, "Contains", null, constant),
                _ => throw new ArgumentException("Invalid comparison type.")
            };

            var lambda = Expression.Lambda<Func<Supplier, bool>>(predicate, parameter);
            query = query.Where(lambda);

            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier sup)
        {
            if (sup == null || string.IsNullOrEmpty(sup.SupplierName) || string.IsNullOrEmpty(sup.ContactPerson) || string.IsNullOrEmpty(sup.ContactPhone) || string.IsNullOrEmpty(sup.ContactEmail))
            {
                return BadRequest("Invalid Supplier data.");
            }

                _context.Suppliers.Add(sup);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error creating Supplier: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetSupplier), new { id = sup.SupplierId }, sup);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id, Supplier model)
        {
            var sup = await _context.Suppliers.FindAsync(id);
            if (sup == null)
            {
                return NotFound();
            }

            // Обновляем только не-null свойства
            if (!string.IsNullOrEmpty(model.SupplierName))
            {
                sup.SupplierName = model.SupplierName;
            }
            if (!string.IsNullOrEmpty(model.ContactPerson))
            {
                sup.ContactPerson = model.ContactPerson;
            }
            if (!string.IsNullOrEmpty(model.ContactEmail))
            {
                sup.ContactEmail = model.ContactEmail;
            }
            if (!string.IsNullOrEmpty(model.ContactPhone))
            {
                sup.ContactPhone = model.ContactPhone;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error updating Supplier: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var sup = await _context.Suppliers.FindAsync(id);
            if (sup == null)
            {
                return NotFound();
            }
            try
            {
                _context.Suppliers.Remove(sup);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error delete Supplier: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return NoContent();
        }
    }
}
