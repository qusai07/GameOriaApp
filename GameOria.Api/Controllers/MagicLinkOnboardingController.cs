using GameOria.Api.Helper.Service;
using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Identity;
using GameOria.Domains.Enums;
using GameOria.Infrastructure.Helper.Model;
using GameOria.Shared.Response;
using GameOria.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace GameOria.Api.Controllers
{
    public class MagicLinkOnboardingController : BaseController
    {
        private readonly JwtHelper _jwtHelper;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;


        public MagicLinkOnboardingController(
            IDataService dataService,
            IPasswordHasher<ApplicationUser> passwordHasher,
            JwtHelper jwtHelper):base(dataService)
        {
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }

        [HttpGet("ValidateMagicLink")]
        public IActionResult ValidateMagicLink(string token)
        {
            if (!_jwtHelper.ValidateToken(token, out string identityNumber))
                return BadRequest("Invalid or expired token.");

            return Ok(identityNumber);
        }

        [HttpPost("CompleteRegistration")]
        public async Task<IActionResult> CompleteRegistration(CompleteRegistrationVM model)
        {
            if (!_jwtHelper.ValidateToken(model.Token, out string identityNumberFromToken))
                return BadRequest("Invalid or expired token.");

            var existingUser = await _dataService.GetQuery<ApplicationUser>()
                                                 .FirstOrDefaultAsync(u => u.IdentityNumber == identityNumberFromToken);

            if (existingUser != null)
                return BadRequest("User with this IdentityNumber already exists.");

            ApplicationUser user = new()
            {
                ID = Guid.NewGuid(),
                FullName = model.FullName,
                UserName = model.UserName,
                EmailAddress = model.EmailAddress,
                MobileNumber = model.MobileNumber,
                IsActive = false,
                OtpCode = OtpHelper.GenerateOtp(6),
                OtpDate = DateTime.UtcNow,
                Role = Roles.Organizer,
                IdentityNumber = identityNumberFromToken,

            };
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);


            await _dataService.AddAsync(user);
            await _dataService.SaveAsync();

            return Ok(new APIResponse
            {
                Success = true,
                Data = user.ID,
                Message = "Registration completed successfully."

            });
        }



    }
}

