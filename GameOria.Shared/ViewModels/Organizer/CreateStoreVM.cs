

using GameOria.Domains.Entities.Stores;
using System.ComponentModel.DataAnnotations;

namespace GameOria.Shared.ViewModels.Organizer
{
    public class CreateStoreVM
    {
        [Required]
        [Display(Name = "Store Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Logo URL")]
        public string LogoUrl { get; set; } = string.Empty;

        [Display(Name = "Cover Image URL")]
        public string CoverImageUrl { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Contact
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Website")]
        public string ShortcutWebsite { get; set; } = string.Empty;

        // Settings
        [Display(Name = "Auto Approve Reviews")]
        public bool AutoApproveReviews { get; set; }

        [Display(Name = "Allow Digital Products")]
        public bool AllowDigitalProducts { get; set; } = true;

        // Commission
        [Display(Name = "Commission Rate")]
        public decimal CommissionRate { get; set; }

        [Display(Name = "Commission Type")]
        public CommissionType CommissionType { get; set; } = CommissionType.Percentage;

        // Owner Info
        [Required]
        [Display(Name = "Owner First Name")]
        public string OwnerFirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Owner Last Name")]
        public string OwnerLastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Owner Email")]
        public string OwnerEmail { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Owner Phone")]
        public string OwnerPhone { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime OwnerDateOfBirth { get; set; }

        // Banking
        [Display(Name = "Bank Name")]
        public string BankName { get; set; } = string.Empty;

        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber { get; set; } = string.Empty;

        [Display(Name = "Bank Routing Number")]
        public string BankRoutingNumber { get; set; } = string.Empty;

        [Display(Name = "SWIFT Code")]
        public string SwiftCode { get; set; } = string.Empty;

        // Agreement
        [Display(Name = "Accepted Terms")]
        public bool HasAcceptedTerms { get; set; }
    }
}
