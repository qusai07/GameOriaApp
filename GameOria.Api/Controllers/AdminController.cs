using GameOria.Domains.Entities.Users;
using GameOria.Domains.Enums;
using GameOria.Infrastructure.Data;
using GameOria.Shared.Response;
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

        [HttpGet("get-All-Organizers")]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _context.OrganizerUsers
                .ToListAsync();
            return Ok(requests);
        }
        [HttpPost("approve/{userId}")]
        public async Task<IActionResult> ApproveOrganizer(Guid userId)
        {
            var request = await _context.OrganizerUsers
                .FirstOrDefaultAsync(r => r.ID == userId && !r.IsVerified);

            if (request == null)
                return NotFound("No pending organizer request found for this user.");

            request.IsVerified = true;
            request.VerificationDate = DateTime.UtcNow;

            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var existingOrganizer = await _context.OrganizerUsers.FindAsync(userId);
            if (existingOrganizer == null)
            {
                OrganizerUser organizer = new ()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    StoreName = request.StoreName,
                    BusinessEmail = request.BusinessEmail,
                    IdentityNumber = request.IdentityNumber,
                    PhoneNumber = request.PhoneNumber,
                    IsVerified = true,
                    VerificationDate = DateTime.UtcNow
                };

                _context.OrganizerUsers.Add(organizer);
            }

            await _context.SaveChangesAsync();

            return Ok(new APIResponse
            {
                Message = "User approved as Organizer successfully.",
                Data = request
            });
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
