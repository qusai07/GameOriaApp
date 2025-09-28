using GameOria.Domains.Enums;
using GameOria.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Api.Controllers
{
    public class AdminController : Controller
    {
        private readonly GameOriaDbContext _context;

        public AdminController(GameOriaDbContext context)
        {
            _context = context;
        }



        [HttpGet("get-All-Customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.ApplicationUsers
                .Where(u => u.Role == Roles.Customer)   
                .Select(u => new
                {
                    u.FullName,
                    u.EmailAddress,
                    u.MobileNumber,
                    u.Role,
                    u.IsActive
                })
                .ToListAsync();

            return Ok(customers);
        }

        [HttpPost("toggle-status/{id}")]
        public async Task<IActionResult> ToggleUserStatus(Guid id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            return Ok(new { user.ID, user.IsActive });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully." });
        }

        public async Task <IActionResult> Index()
        {
            return View();
        }
    }
}
