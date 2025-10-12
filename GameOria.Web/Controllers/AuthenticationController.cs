using AutoMapper;
using GameOria.Application.DTOs.Login;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.Request;
using GameOria.Shared.Response;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Handlers;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameOria.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly AuthHeaderHandler _authHeaderHandler;
        private readonly IMapper _mapper;
        public AuthenticationController(IAuthService authService , IMapper mapper, AuthHeaderHandler authHeaderHandler)
        {
            _authService = authService;
            _mapper = mapper;
            _authHeaderHandler = authHeaderHandler;
        }
        public IActionResult SignUp()
        {
            var model = new SignupViewModel();
            return View("~/Views/Auth/SignUp.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _authService.SignUpAsync(_mapper.Map<SignupParameters>(model));
            var apiResponse = await _authHeaderHandler.HandleApiResponse(response);

            if (apiResponse.Success)
            {
                return RedirectToAction("VerifyOtp", "Authentication", new { id = apiResponse.Data?.ToString() });
            }

            else 
            {
                ModelState.AddModelError("", apiResponse.Message ?? "Sign up failed.");
                return View("~/Views/Auth/SignUp.cshtml", model);
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Auth/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Auth/Login.cshtml", model);

            var response = await _authService.LoginAsync(model);
            var apiResponse = await _authHeaderHandler.HandleApiResponse(response);

            if (apiResponse.Success)
            {
                var tokenAndRole = apiResponse.Data.ToString();

                var parts = tokenAndRole.Split(' ');
                var token = parts[0];
                var role = parts.Length > 1 ? parts[1] : string.Empty;

                if (!string.IsNullOrEmpty(token))
                {
                    //  JWT  HttpOnly Cookie
                    Response.Cookies.Append("AuthToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, //  HTTPS
                        SameSite = SameSiteMode.Strict, //  CSRF
                        Expires = DateTimeOffset.Now.AddMinutes(60)
                    });

                    // In Session
                    HttpContext.Session.SetString("UserRole", role);
                }



                switch (role.ToLower())
                {
                    case "admin":
                        return RedirectToAction("Dashboard", "Admin");
                    case "organizer":
                        return RedirectToAction("Profile", "Authentication");
                    case "user":
                        return RedirectToAction("Home", "User");
                    default:
                        return RedirectToAction("Index", "Home");
                }

            }

            if (!apiResponse.Success)
            {
                var errorMessage = apiResponse.Message ?? "Invalid login attempt.";
                if (apiResponse.Errors?.Any() == true)
                    errorMessage += " " + string.Join(" ", apiResponse.Errors);

                ModelState.AddModelError("", errorMessage);
                return View("~/Views/Auth/Login.cshtml", model);
            }
            else
            {
                ModelState.AddModelError("", apiResponse.Message ?? " Login failed.");
                return View("~/Views/Auth/Login.cshtml", model);

            }

        }

        public async Task<IActionResult> Profile()
        {
            var profile = await _authService.GetProfileAsync();

            if (profile == null)
                return Unauthorized();

            return View("~/Views/Auth/Profile.cshtml", profile);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel request)
        {
            var response = await _authService.UpdateProfileAsync(request);

            if (!response.IsSuccessStatusCode)
                return View("~/Views/Auth/Profile.cshtml", request); 

            return RedirectToAction("Profile");
        }
     
        public IActionResult VerifyOtp(Guid Id)
        {
            var model = new VerifyOtpViewModel { Id = Id };
            return View("~/Views/Auth/VerfiyOTP.cshtml",model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtp(VerifyOtpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.VerifyOtpAsync(model.Id, model.Otp);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Login", "Authentication");

            ModelState.AddModelError("Otp", "Invalid OTP, please try again.");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResendOtp(ResendOtp model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("VerifyOtp", new { id = model.Id });

            var result = await _authService.ResendOtpAsync(model.Id);

            if (result.IsSuccessStatusCode)
            {
                TempData["OtpMessage"] = "A new OTP has been sent to your email/phone.";
                return RedirectToAction("VerifyOtp", new { id = model.Id });
            }

            TempData["OtpError"] = "Failed to resend OTP, please try again.";
            return RedirectToAction("VerifyOtp", new { id = model.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.LogoutAsync();

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Remove("token");

                if (Request.Cookies.ContainsKey("token"))
                {
                    Response.Cookies.Delete("token");
                }
                HttpContext.Session.Clear();
                Response.Cookies.Delete("AuthToken");

                return Ok("LoggedOutSuccessfully");
            }

            return BadRequest("LogoutFailed");
        }








    }
}
