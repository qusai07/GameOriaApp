using GameOria.Shared.Response;
using GameOria.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace GameOria.Web.Controllers
{
    public class MagicLinkOnboardingController : Controller
    {
        private readonly HttpClient _httpClient;

        public MagicLinkOnboardingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GameOriaApi");
        }
     
        [HttpGet]
        public async Task<IActionResult> CompleteRegistration(string token)
        {
            if (string.IsNullOrEmpty(token))
                return View("InvalidLink");

            token = HttpUtility.UrlDecode(token);
            var response = await _httpClient.GetAsync($"http://localhost:7075/api/MagicLinkOnboarding/ValidateMagicLink?token={token}");

         
            if (!response.IsSuccessStatusCode)
                return View("InvalidLink");

            CompleteRegistrationVM model = new ()
            {
                Token = token
            };
            return View("~/Views/Auth/CompletereRistration.cshtml", model);

        }

        [HttpPost]
        public async Task<IActionResult> FinshRegistration(CompleteRegistrationVM model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Auth/CompleteRegistrationForm.cshtml", model);

            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:7075/api/MagicLinkOnboarding/CompleteRegistration", model);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

                ViewData["SuccessMessage"] = "Registration completed successfully!";

                return RedirectToAction("VerifyOtp", "Authentication", new { id = apiResponse?.Data });
            }

            ViewData["ErrorMessage"] = "An error occurred while completing registration.";
            return View("~/Views/Auth/CompleteRegistrationForm.cshtml", model);
        }


    }
}
