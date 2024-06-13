using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;

namespace Serverna.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessRightsController : ControllerBase
    {
        private readonly CursovaDbContext _context;

        public AccessRightsController(CursovaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessRight>>> GetAccessRights()
        {
            return Ok(await _context.AccessRights.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessRight>> GetAccessRight(int id)
        {
            var accessRight = await _context.AccessRights.FindAsync(id);
            if (accessRight == null)
            {
                return NotFound();
            }
            return Ok(accessRight);
        }

        [HttpPost]
        public async Task<ActionResult<AccessRight>> CreateAccessRight(AccessRight accessRight)
        {
            _context.AccessRights.Add(accessRight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccessRight), new { id = accessRight.AccessId }, accessRight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccessRight(int id, AccessRight accessRight)
        {
            if (id != accessRight.AccessId)
            {
                return BadRequest();
            }

            _context.Entry(accessRight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessRightExists(id))
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
        public async Task<IActionResult> DeleteAccessRight(int id)
        {
            var accessRight = await _context.AccessRights.FindAsync(id);
            if (accessRight == null)
            {
                return NotFound();
            }

            _context.AccessRights.Remove(accessRight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessRightExists(int id)
        {
            return _context.AccessRights.Any(e => e.AccessId == id);
        }
    }
}
