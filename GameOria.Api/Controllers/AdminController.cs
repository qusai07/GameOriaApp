using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Entities.Users;
using GameOria.Domains.Enums;
using GameOria.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GameOria.Api.Controllers
{
    public class AdminController : BaseController
    {

        public AdminController(IDataService dataService):base(dataService){}
        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var requests = await _dataService.GetAllAsync<ApplicationUser>();
            return Ok(requests);
        }
        [HttpGet("get-All-Organizers")]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _dataService.GetAllAsync<OrganizerUser>();
            return Ok(requests);
        }
        [HttpPost("approve/{userId}")]
        public async Task<IActionResult> ApproveOrganizer(Guid userId)
        {
            var request = await _dataService.GetQuery<OrganizerUser>()
                                .FirstOrDefaultAsync(r => r.UserId == userId && !r.IsVerified);


            if (request == null)
                return NotFound(new APIResponse
                {
                    Success = false,
                    Message = "No pending organizer request found for this user."
                });

            request.IsVerified = true;
            request.VerificationDate = DateTime.UtcNow;

            var user = await _dataService.GetQuery<ApplicationUser>().FirstOrDefaultAsync(u => u.ID == userId);
            if (user == null)
                return NotFound(new APIResponse
                {
                    Success = false,
                    Message = "User not found."
                });

            user.Role = Roles.Organizer;
            await _dataService.SaveAsync();
            return Ok(new APIResponse
            {
                Success = true,
                Message = "User approved as Organizer successfully."
            });
        }

        [HttpPost("toggle-status/{id}")]
        public async Task<IActionResult> ToggleUserStatus(Guid id)
        {
            var user = await _dataService.GetQuery<ApplicationUser>().FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return NotFound("User not found.");

            user.IsActive = !user.IsActive;
            await _dataService.SaveAsync();

            return Ok(new { user.ID, user.IsActive });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _dataService.GetQuery<ApplicationUser>().FirstOrDefaultAsync(u=> u.ID == id);
            if (user == null) return NotFound("UserNotFound.");

            await _dataService.DeleteAsync<ApplicationUser>(user);
            await _dataService.SaveAsync();

            return Ok(new { Message = "User deleted successfully." });
        }

        //public async Task<IActionResult> ChangeRoleUser
        //public async Task<IActionResult> GetStores
        //public async Task<IActionResult> StatusStore
        //public async Task<IActionResult> 
        //public async Task<IActionResult> 


    }
}
