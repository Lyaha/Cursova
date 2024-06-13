using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serverna.Model;

namespace Serverna.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsersController : ControllerBase
     {
         private readonly CursovaDbContext _context;

         public UsersController(CursovaDbContext context)
         {
             _context = context;
         }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
         [HttpGet("{id}")]
         public async Task<ActionResult<LoginUser>> GetUser(int id)
         {
             var user = await _context.Users.FindAsync(id);

             if (user == null)
             {
                 return NotFound();
             }
            LoginUser us = new LoginUser()
            {
                UserId = user.UserId,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                RoleId = user.RoleId
            };
             return us;
         }
         [HttpGet("role/{roleId}")]
         public async Task<ActionResult<IEnumerable<LoginUser>>> GetUsersByRole(int roleId)
         {
             var users = await _context.Users
                                       .Where(u => u.RoleId == roleId)
                                       .ToListAsync();

             if (users == null || users.Count == 0)
             {
                 return NotFound();
             }

            var uss = new List<LoginUser>();
            foreach (var user in users)
            {
                LoginUser us = new LoginUser()
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    RoleId = user.RoleId
                };
                uss.Add(us);
            }

            return uss;
         }
         [HttpPost]
         public async Task<ActionResult<User>> PostUser(CreateUserModel model)
         {
             if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
             {
                 return BadRequest("Invalid user data.");
             }

             var user = new User
             {
                 Username = model.Username,
                 Password = model.Password,
                 Email = model.Email,
                 RoleId = model.RoleId
             };

             _context.Users.Add(user);

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (Exception ex)
             {
                 // Логирование ошибки
                 Console.WriteLine($"Error creating user: {ex.Message}");
                 return StatusCode(500, "Internal server error");
             }

             return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
         }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, CreateUserModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Обновляем только не-null свойства
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.Username = model.Username;
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = model.Password;
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }

            if (model.RoleId != 0)
            {
                user.RoleId = model.RoleId;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error updating user: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}