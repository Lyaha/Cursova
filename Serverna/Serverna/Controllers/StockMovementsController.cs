using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : Controller
    {
        private readonly CursovaDbContext _context;

        public StockMovementsController(CursovaDbContext context)
        {
            _context = context;
        }

        // GET: api/MovementType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovement>>> GetStockMovements()
        {
            return await _context.StockMovements.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovement>> GetStockMovement(int id)
        {
            var stock = await _context.StockMovements.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpGet("type/{typeId}")]
        public async Task<ActionResult<IEnumerable<StockMovement>>> GetStockByType(int typeId)
        {
            var stocks = await _context.StockMovements
                                      .Where(s => s.MovementTypeId == typeId)
                                      .ToListAsync();

            if (stocks == null || stocks.Count == 0)
            {
                return NotFound();
            }

            return stocks;
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<StockMovement>>> GetStockByProduct(int productId)
        {
            var stocks = await _context.StockMovements
                                      .Where(s => s.ProductId == productId)
                                      .ToListAsync();

            if (stocks == null || stocks.Count == 0)
            {
                return NotFound();
            }

            return stocks;
        }
        [HttpPost]
        public async Task<ActionResult<StockMovement>> PostStockMovement(StockMovement stock)
        {
            if (stock == null || string.IsNullOrEmpty(stock.BatchNumber) || stock.Quantity<=0 || stock.ProductId <= 0 || stock.MovementTypeId <=0)

            {
                return BadRequest("Invalid Stock Movement data.");
            }

            stock.MovementDate = DateTime.Now;
            _context.StockMovements.Add(stock);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error creating Stock Movement: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetStockMovement), new { id = stock.MovementId }, stock);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockMovement(int id, StockMovement model)
        {
            var stock = await _context.StockMovements.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            // Обновляем только не-null свойства
            if(model.MovementTypeId > 0)
            {
                stock.MovementTypeId = model.MovementTypeId;
            }
            if(model.ProductId > 0)
            {
                stock.ProductId = model.ProductId;
            }
            if (model.Quantity>0)
            {
                stock.Quantity = model.Quantity;
            }
            if (!string.IsNullOrEmpty(model.BatchNumber))
            {
                stock.BatchNumber = model.BatchNumber;
            }
            if(!string.IsNullOrEmpty(model.Notes))
            {
                stock.Notes = model.Notes;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error updating Stock Movement: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockMovement(int id)
        {
            var stock = await _context.StockMovements.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.StockMovements.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
