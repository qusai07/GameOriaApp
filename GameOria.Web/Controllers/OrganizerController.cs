using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.ViewModels;
using GameOria.Web.Service.Implementation;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Web.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizerService;

        public OrganizerController(IOrganizerService organizerService)
        {
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

            OrganizerRequestDto requestDto = new ()
            {
                Email = model.BusinessEmail,
                StoreName = model.StoreName,
                IdentityNumber = model.IdentityNumber,
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
        public IActionResult CompleteStoreProfile()
        {
            return View("~/Views/Organizer/CompleteStoreProfile.cshtml");
        }

        public IActionResult MyStore()
        {
            var store = _organizerService.GetMyStore();
            if (store == null)
                return View("~/Views/Organizer/CreateMyStore.cshtml");
            else
                return View("~/Views/Organizer/MyStore.cshtml",store);
        }
        public IActionResult CreateMyStore()
        {
            return View("~/Views/Organizer/CreateMyStore.cshtml");
        }
        //public async Task <IActionResult> CreateMyStore(CreateStoreVm createStoreVm)
        //{
        //    return View("~/Views/Organizer/CreateMyStore.cshtml");
        //}








    }
}
