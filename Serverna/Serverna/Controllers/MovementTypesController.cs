using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;

namespace Serverna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovemenTypesController : Controller
    {
        private readonly CursovaDbContext _context;

        public MovemenTypesController(CursovaDbContext context)
        {
            _context = context;
        }

        // GET: api/MovementType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovementType>>> GetMovementTypes()
        {
            return await _context.MovementTypes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MovementType>> GetMovementType(int id)
        {
            var roles = await _context.MovementTypes.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }
        [HttpPost]
        public async Task<ActionResult<MovementType>> PostMovementType(MovementType type)
        {
            if (type == null || string.IsNullOrEmpty(type.TypeName))
            {
                return BadRequest("Invalid Movement Type data.");
            }

            _context.MovementTypes.Add(type);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error creating Movement Type: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return CreatedAtAction(nameof(GetMovementType), new { id = type.MovementTypeId }, type);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovementType(int id, MovementType model)
        {
            var type = await _context.MovementTypes.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            // Обновляем только не-null свойства
            if (!string.IsNullOrEmpty(model.TypeName))
            {
                type.TypeName = model.TypeName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error updating Movement Type: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovementType(int id)
        {
            var type = await _context.MovementTypes.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.MovementTypes.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
