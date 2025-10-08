using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Users;
using GameOria.Infrastructure.Data;
using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameOria.Api.Controllers
{
    public class OrganizerAPIController : BaseController
    {
        private readonly GameOriaDbContext _context;

        public OrganizerAPIController(IDataService dataService ,GameOriaDbContext context) : base(dataService)
        {
            _context = context;
        }
        [HttpPost("Become-organizer-requests")]
        public async Task<IActionResult> BecomeOrganizerRequest([FromBody] OrganizerRequestDto organizerRequestDto)
        {
            var existingRequest = await _dataService.GetQuery<OrganizerUser>()
                .FirstOrDefaultAsync(r => r.UserId == organizerRequestDto.UserId && !r.IsVerified);

            if (existingRequest != null)
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "You already have a pending request."
                });

            try
            {
                OrganizerUser Request = new()
                {
                    UserId = organizerRequestDto.UserId,
                    BusinessEmail = organizerRequestDto.BusinessEmail,
                    IdentityNumber = organizerRequestDto.IdentityNumber,
                    PhoneNumber = organizerRequestDto.PhoneNumber,
                    StoreName = organizerRequestDto.StoreName,
                };

                await _dataService.AddAsync(Request);
                await _dataService.SaveAsync();
                return Ok(new APIResponse
                {
                    Success = true,
                    Message = "ThankYouForJoin"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }  
        }
    }
}
