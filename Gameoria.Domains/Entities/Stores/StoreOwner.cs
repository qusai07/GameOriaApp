using GameOria.Domains.Common;
using GameOria.Domains.Entities.Identity;


namespace GameOria.Domains.Entities.Stores
{
    public class StoreOwner : BaseAuditableEntity
    {
        public string UserId { get; set; } // ApplicationUser Id
        public Guid StoreId { get; set; }

        // Personal Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        // Business Information
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyRegistrationNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty;

        // Address Information
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        // Verification Status
        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string? VerificationDocument { get; set; }

        // Banking Information
        public string BankName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankRoutingNumber { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;

        // Navigation Properties
        public virtual ApplicationUser User { get; set; }
        public virtual Store Store { get; set; }

        // Agreement Information
        public bool HasAcceptedTerms { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public string? TermsAcceptanceIp { get; set; }
    }
}
