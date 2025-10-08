using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Web.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IHttpClientFactory httpClientFactory , IOrganizerService organizerService)
        {
            _httpClientFactory = httpClientFactory;
            _organizerService = organizerService;
        }

        public IActionResult BecomeOrganizer()
        {
            return View("~/Views/Organizer/BecomeOrganizer.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BecomeOrganizer(BecomeOrganizerViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Organizer/BecomeOrganizer.cshtml", model);

            var httpClient = _httpClientFactory.CreateClient("GameOriaApi");

            OrganizerRequestDto requestDto = new ()
            {
                BusinessEmail = model.BusinessEmail,
                StoreName = model.StoreName,
                IdentityNumber = model.IdentityNumber,
                PhoneNumber = model.PhoneNumber
            };

            var response = await _organizerService.RequestBecomeOrganizer(requestDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Your request has been submitted successfully!";
                return RedirectToAction();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, error);
                return View("~/Views/Organizer/BecomeOrganizer.cshtml", model);
            }
        }


        //public async Task<IActionResult> 






    }
}
