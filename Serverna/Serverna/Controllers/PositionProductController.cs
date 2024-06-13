using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionProductController : ControllerBase
    {
        private readonly CursovaDbContext _context;

        public PositionProductController(CursovaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionProduct>>> GetPositionProducts()
        {
            return await _context.PositionProducts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionProduct>> GetPositionProduct(int id)
        {
            var positionProduct = await _context.PositionProducts.FindAsync(id);

            if (positionProduct == null)
            {
                return NotFound();
            }

            return positionProduct;
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<PositionProduct>>> GetPositionsByProductId(int productId)
        {
            var pos = await _context.PositionProducts
                                      .Where(p => p.IdProd == productId)
                                      .ToListAsync();

            if (pos == null || pos.Count == 0)
            {
                return NotFound();
            }

            return pos;
        }

        [HttpPost]
        public async Task<ActionResult<PositionProduct>> PostPositionProduct(PositionProduct positionProduct)
        {
            _context.PositionProducts.Add(positionProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionProduct", new { id = positionProduct.IdPosProd }, positionProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionProduct(int id, PositionProduct positionProduct)
        {
            if (id != positionProduct.IdPosProd)
            {
                return BadRequest();
            }

            _context.Entry(positionProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionProduct(int id)
        {
            var positionProduct = await _context.PositionProducts.FindAsync(id);
            if (positionProduct == null)
            {
                return NotFound();
            }

            _context.PositionProducts.Remove(positionProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionProductExists(int id)
        {
            return _context.PositionProducts.Any(e => e.IdPosProd == id);
        }
    }
}
