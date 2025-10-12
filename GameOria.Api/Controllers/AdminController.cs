using GameOria.Api.Repo.Interface;
using GameOria.Application.Interface;
using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Entities.Users;
using GameOria.Domains.Enums;
using GameOria.Infrastructure.Helper.Service;
using GameOria.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace GameOria.Api.Controllers
{
    public class AdminController : BaseController
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IMailService _mailService ;

        public AdminController(IDataService dataService, JwtHelper jwtHelper ,IMailService mailService) : base(dataService)
        {
            _jwtHelper = jwtHelper;
            _mailService = mailService;
        }
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

        [HttpPost("ApproveOrganizer")]
        public async Task<IActionResult> ApproveOrganizer(string identityNumber)
        {
            var request = await _dataService.GetQuery<OrganizerUser>()
                .FirstOrDefaultAsync(r => r.IdentityNumber == identityNumber && !r.IsVerified);

            if (request == null)
                return NotFound(new APIResponse
                {
                    Success = false,
                    Message = "No pending organizer request found for this user."
                });

            request.IsVerified = true;
            request.VerificationDate = DateTime.UtcNow;

            var tempUser = new ApplicationUser
            {
                ID = Guid.NewGuid(),
                UserName = request.StoreName,
                Role = Roles.Organizer,
                EmailAddress = request.Email,
                IdentityNumber = request.IdentityNumber
            };
            var registrationToken = _jwtHelper.GenerateToken(tempUser);

            var encodedToken = HttpUtility.UrlEncode(registrationToken);
            var link = $"https://localhost:7269/MagicLinkOnboarding/CompleteRegistration?token={encodedToken}";

            await _mailService.SendEmailAsync(
                request.Email,
                "Complete Your Registration",
                $"Welcome to GameOria! Please complete your registration by clicking <a href='{link}'>here</a>. This link will expire in 24 hours."
            );

            await _dataService.SaveAsync();

            return Ok(new APIResponse
            {
                Success = true,
                Message = "Approval successful. Registration link sent to user."
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
