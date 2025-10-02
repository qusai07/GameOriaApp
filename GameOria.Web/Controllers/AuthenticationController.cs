using AutoMapper;
using GameOria.Application.DTOs.Login;
using GameOria.Shared.DTOs.SigUp;
using GameOria.Shared.Request;
using GameOria.Shared.Response;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameOria.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthenticationController(IAuthService authService , IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
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

            var signupDto = _mapper.Map<SignupParameters>(model);
            var response = await _authService.SignUpAsync(signupDto);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var signupResponse = JsonConvert.DeserializeObject<SignupResponse>(content);
                return RedirectToAction("VerifyOTP", "Authentication", new { id = signupResponse.Id });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("Error", content);
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






    }
}
