using System;
using System.ComponentModel.DataAnnotations;

namespace GameOria.Shared.ViewModels
{
    public class BecomeOrganizerViewModel
    {
        [Required(ErrorMessage = "Business Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Business Email")]
        public string BusinessEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Store Name is required")]
        [StringLength(100, ErrorMessage = "Store Name cannot exceed 100 characters")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identity Number is required")]
        [StringLength(20, ErrorMessage = "Identity Number cannot exceed 20 characters")]
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number (Optional)")]
        public string? PhoneNumber { get; set; }
    }
}
