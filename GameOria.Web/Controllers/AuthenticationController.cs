using GameOria.Application.DTOs.Login;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("~/Views/Auth/SignUp.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupParameters model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var signupParams = new SignupParameters
            {
                FullName = model.FullName,
                EmailAddress = model.EmailAddress,
                MobileNumber = model.MobileNumber,
                UserName = model.UserName,
                Password = model.Password,
                UserRole = Domains.Enums.Roles.Customer
            };

            var response = await _authService.SignUpAsync(signupParams);

            if (response.IsSuccessStatusCode)
            {
                TempData["ShowOtpModal"] = true;
                return View("~/Views/Auth/SignUp.cshtml", model);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", content);
                return View(model);
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
                return View(model);

            var response = await _authService.LoginAsync(model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }

            var tokenResponse = await response.Content.ReadAsStringAsync();

            HttpContext.Session.SetString("AuthToken", tokenResponse);

            return RedirectToAction("Profile");
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

        [HttpPost]
        public async Task<IActionResult> VerifyOtp(string otp)
        {
            var response = await _authService.VerifyOtpAsync(otp);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }




    }
}
