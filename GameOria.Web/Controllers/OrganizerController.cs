using GameOria.Application.Stores.DTOs;
using GameOria.Shared.DTOs.Organizer;
using GameOria.Shared.ViewModels;
using GameOria.Shared.ViewModels.Organizer;
using GameOria.Web.Service.Implementation;
using GameOria.Web.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public async Task<IActionResult> MyStore()
        {
            var store = await _organizerService.GetMyStoreAsync();

            if (store == null)
                return View("~/Views/Organizer/CreateMyStore.cshtml");

            return View("~/Views/Organizer/MyStore.cshtml", store);
        }

        public IActionResult CreateMyStore()
        {
            return View("~/Views/Organizer/CreateMyStore.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateMyStore(CreateStoreVM createStoreVm)
        {
            if(!ModelState.IsValid)
                return View("~/Views/Organizer/CreateMyStore.cshtml");

            CreateStoreVM storOrganizerDto = new()
            {
                Name = createStoreVm.Name,
                Description = createStoreVm.Description,
                LogoUrl = createStoreVm.LogoUrl,
                CoverImageUrl = createStoreVm.CoverImageUrl,


                Email = createStoreVm.Email,
                Phone = createStoreVm.Phone,
                ShortcutWebsite = createStoreVm.ShortcutWebsite,

                AutoApproveReviews = createStoreVm.AutoApproveReviews,
                OwnerFirstName = createStoreVm.OwnerFirstName,
                OwnerLastName = createStoreVm.OwnerLastName,
                OwnerDateOfBirth = createStoreVm.OwnerDateOfBirth,
                OwnerEmail = createStoreVm.OwnerEmail,
                OwnerPhone = createStoreVm.OwnerPhone,

                BankName = createStoreVm.BankName,  
                BankAccountNumber = createStoreVm.BankAccountNumber,
                BankRoutingNumber = createStoreVm.BankRoutingNumber,
                SwiftCode = createStoreVm.SwiftCode,

                HasAcceptedTerms = createStoreVm.HasAcceptedTerms,

            };
            

            return View("~/Views/Organizer/CreateMyStore.cshtml");
        }








    }
}
