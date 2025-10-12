using GameOria.Application.DTOs.Login;
using GameOria.Application.DTOs.ResetPassword;
using GameOria.Application.DTOs.SigUp;
using GameOria.Application.DTOs.UserInformation;
using GameOria.Api.Helper.Service;
using GameOria.Api.Repo.Interface;
using GameOria.Domains.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using GameOria.Shared.Response;
using GameOria.Infrastructure.Helper.Service;

namespace GameOria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;  
        private readonly IDistributedCache _cache;

        public AuthController(IDataService dataService, IConfiguration configuration, IPasswordHasher<ApplicationUser> passwordHasher, JwtHelper jwtHelper) : base(dataService)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }

        //APIResponse Done
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignupParameters signupParameters)
        {
            var existingUser = await _dataService.GetQuery<ApplicationUser>()
                     .Where(u => u.UserName == signupParameters.UserName
                     || u.MobileNumber == signupParameters.MobileNumber || u.EmailAddress == signupParameters.EmailAddress
                     ).ToListAsync();

            var errors = new List<string>();
            if (existingUser.Any(x => x.UserName == signupParameters.UserName)) errors.Add("UserNameUsed");
            if (existingUser.Any(x => x.MobileNumber == signupParameters.MobileNumber)) errors.Add("MobileNumberUsed");
            if (existingUser.Any(x => x.EmailAddress == signupParameters.EmailAddress)) errors.Add("EmailAddressUsed");

            if (errors.Any()) return BadRequest(errors);

            try
            {
                ApplicationUser user = new ()
                {
                    FullName = signupParameters.FullName,
                    UserName = signupParameters.UserName,
                    EmailAddress = signupParameters.EmailAddress,
                    MobileNumber = signupParameters.MobileNumber,
                    IsActive = false,
                    OtpCode = OtpHelper.GenerateOtp(6),
                    OtpDate = DateTime.UtcNow,
                    Role = signupParameters.UserRole,
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, signupParameters.Password);
                await _dataService.AddAsync(user);
                await _dataService.SaveAsync();
                return Ok(new APIResponse
                {
                    Success = true,
                    Message = "SignUpSuccess",
                    Data = user.ID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Errors = new List<string> { ex.Message }
                });
            }

        }

        //APIResponse Done
        [HttpPost("SignupResendOtp")]
        public async Task<IActionResult> SignupResendOtp([FromBody] SignupUserParameters signupUserParameters)
        {
            try
            {
                var user = await GetUserByIdAsync(signupUserParameters.Id);

                if (user == null)
                {
                    return StatusCode(500,new APIResponse
                    {
                        Success = false,
                        Message = "UserNotFound"
                    });
                }

                var otpTimeOut = _configuration.GetValue("OtpTimeOut", 2); // default 2 mins
                if (DateTime.UtcNow - user.OtpDate < TimeSpan.FromMinutes(otpTimeOut))
                {
                    return StatusCode(500, new APIResponse
                    {
                        Success = false,
                        Message = "OtpAlreadySent",
                    });
                }

                // Generate new OTP
                user.OtpCode = OtpHelper.GenerateOtp(6);
                user.OtpDate = DateTime.UtcNow;
                await _dataService.SaveAsync();
                return Ok(user.OtpCode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        //APIResponse Done
        [HttpPost("SignupVerifyOtp")]
        public async Task<IActionResult> SignupVerifyOtp([FromBody] SignupVerifyOtpParameters signupVerifyOtpParameters)
        {
            try
            {
                var user = await GetUserByIdAsync(signupVerifyOtpParameters.Id);

                if (user == null) return BadRequest("UserNotFound");

                var otpTimeOut = _configuration.GetValue("OtpTimeOut", 2); // 2 mins
                if (DateTime.UtcNow - user.OtpDate > TimeSpan.FromMinutes(otpTimeOut))
                    return BadRequest("OtpExpired");

                if (user.OtpCode != signupVerifyOtpParameters.OtpCode)
                    return BadRequest("OtpNotMatched");

                user.IsActive = true;
                await _dataService.SaveAsync();
                return Ok("AccountVerified");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
           
        }

        //[HttpGet("CheckNationalNumber/{userId}")]
        //public async Task<IActionResult> CheckNationalNumber(Guid userId)
        //{
        //    var user = await GetUserByIdAsync(userId);
        //    if (user == null)
        //        return NotFound("UserNotFound");

        //    if (string.IsNullOrEmpty(user.IdentityNumber))
        //        return Ok(new
        //        {
        //            HasNationalNumber = false,
        //            Message = "User has no National Number registered."
        //        });

        //    return Ok(new
        //    {
        //        HasNationalNumber = true,
        //        NationalNumber = user.IdentityNumber,
        //        FullName = user.FullName,
        //        MobileNumber = user.MobileNumber,
        //        EmailAddress = user.EmailAddress
        //    });
        //}


        //APIResponse Done
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginParameters loginParameters)
        {
            try
            {
                var user = _dataService.GetQuery<ApplicationUser>()
                 .FirstOrDefault(x => x.UserName == loginParameters.UserNameOrEmail || x.EmailAddress == loginParameters.UserNameOrEmail);


                if (user == null)
                    return BadRequest("UserNotFound");

                if (!user.IsActive)
                    return BadRequest("AccountNotActive");

                var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginParameters.Password);
                if (passwordCheck == PasswordVerificationResult.Failed)
                    return BadRequest("InvalidPassword");


                var token = _jwtHelper.GenerateToken(user);
                return Ok(new APIResponse{
                Data = token + " " + user.Role,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse  
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }

        }

        // Email Sender Class (SMTP + MailKit) we Need that
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var user = _dataService.GetQuery<ApplicationUser>()
              .FirstOrDefault(u => u.EmailAddress == request.Email);
                if (user == null)
                    return BadRequest("UserNotFound");

                var resetCode = new Random().Next(100000, 999999).ToString();

                PasswordResetRequest resetRequest = new ()
                {
                    UserId = user.ID,
                    ResetCode = resetCode,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(15)
                };
                await _dataService.CreateAsync(resetRequest);
                await _dataService.SaveAsync();

                return Ok("Otp Sent");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var user = await _dataService.GetQuery<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.EmailAddress == request.Email);

                if (user == null)
                    return BadRequest("UserNotFound");

                var resetRequest = await _dataService.GetQuery<PasswordResetRequest>()
                .FirstOrDefaultAsync(r => r.UserId == user.ID && r.ResetCode == request.ResetCode && !r.IsUsed);

                if (resetRequest == null)
                    return BadRequest("InvalidResetCode");

                if (resetRequest.ExpiryDate < DateTime.UtcNow)
                    return BadRequest("ResetCodeExpired");

                user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);

                resetRequest.IsUsed = true;

                await _dataService.SaveAsync();
                return Ok("PasswordResetSuccessful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized();

                var user = await _dataService.GetByIdAsync<ApplicationUser>(userId.Value);
                if (user == null)
                    return NotFound("UserNotFound");

                var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.OldPassword);
                if (passwordCheck == PasswordVerificationResult.Failed)
                    return BadRequest("InvalidOldPassword");

                user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
                await _dataService.UpdateAsync(user);
                await _dataService.SaveAsync();
                return Ok("PasswordChangedSuccessfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [Authorize]
        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null)
                    return Unauthorized();

                var user = await _dataService.GetByIdAsync<ApplicationUser>(userId);

                if (user == null)
                    return NotFound("UserNotFound");

                return Ok(new
                {
                    user.FullName,
                    user.UserName,
                    user.EmailAddress,
                    user.IsActive,
                    user.MobileNumber
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [Authorize]
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            try
            {    
                var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var user = await _dataService.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
                return NotFound("UserNotFound");

            var isEmailTaken = await _dataService.GetQuery<ApplicationUser>()
                .AnyAsync(u => u.EmailAddress == request.EmailAddress && u.ID != user.ID);
            if (isEmailTaken)
                return BadRequest("EmailAlreadyInUse");

            var isNumberTaken = await _dataService.GetQuery<ApplicationUser>()
                .AnyAsync(u => u.MobileNumber == request.MobileNumber && u.ID != user.ID);
            if (isNumberTaken)
                return BadRequest("NumberAlreadyInUse");

            var isUserNameTaken = await _dataService.GetQuery<ApplicationUser>()
                .AnyAsync(u => u.UserName == request.UserName && u.ID != user.ID);
            if (isUserNameTaken)
                return BadRequest("UserNameAlreadyInUse");


            // Update data
            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.EmailAddress = request.EmailAddress;
            user.MobileNumber = request.MobileNumber;

            await _dataService.UpdateAsync(user);
            await _dataService.SaveAsync();

            return Ok("ProfileUpdatedSuccessfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
    
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok("LoggedOutSuccessfully");
        }

    }

}
