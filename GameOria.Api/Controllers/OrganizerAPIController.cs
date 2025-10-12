using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Stores;
using GameOria.Domains.Entities.Users;
using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerAPIController : BaseController
    {

        public OrganizerAPIController(IDataService dataService) : base(dataService)
        {
        }
        [HttpPost("Become-organizer-requests")]
        public async Task<IActionResult> BecomeOrganizerRequest([FromBody] OrganizerRequestDto organizerRequestDto)
        {
            var existingRequest = await _dataService.GetQuery<OrganizerUser>()
                .FirstOrDefaultAsync(r => r.IdentityNumber == organizerRequestDto.IdentityNumber && !r.IsVerified);

            if (existingRequest != null)
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "You already have a pending request."
                });

            try
            {
                OrganizerUser user = new()
                {
                    IdentityNumber = organizerRequestDto.IdentityNumber,
                    StoreName = organizerRequestDto.StoreName,
                    Email = organizerRequestDto.Email
                };

                await _dataService.AddAsync(user);
                await _dataService.SaveAsync();
                return Ok(new APIResponse
                {
                    Success = true,
                    Message = "ThankYouForJoin"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "SomethingError"
                });
            }  
        }


        //Notes
        [HttpGet("Get-My-Status-Request")]
        public async Task<IActionResult> GetMyStatusRequest()
        {
            var existingRequest = await _dataService.GetQuery<OrganizerUser>()
                .FirstOrDefaultAsync(r => r.IdentityNumber !=null);

            if (existingRequest == null)
            {
                return NotFound(new APIResponse
                {
                    Success = false,
                    Message = "Organizer request not found."
                });
            }

            if (existingRequest.IsVerified == false)
            {
                return BadRequest(new APIResponse
                {
                    Success = false,
                    Message = "You can't be an organizer."
                });
            }

            if (existingRequest.IsVerified == null)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "You already have a pending request."
                });
            }

            return Ok(new APIResponse
            {
                Success = true,
                Message = "Welcome to GameOria! Please complete your store profile within 3 days."
            });

        }

        [HttpGet("Get-My-Store")]
        public async Task<IActionResult> GetMyStore()
        {
            var userId = GetUserId();
            var store = await _dataService.GetQuery<Store>()
              .FirstOrDefaultAsync(r => r.UserId == userId);
            if(store == null)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "You don't have store yet"
                });
            }
            else
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "Store found",
                    Data = store
                });
            }


        }

    }
}
