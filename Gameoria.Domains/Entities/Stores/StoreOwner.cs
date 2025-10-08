using GameOria.Domains.Common;
using GameOria.Domains.Entities.Identity;


namespace GameOria.Domains.Entities.Stores
{
    public class StoreOwner : BaseAuditableEntity
    {
        public Guid UserId { get; set; } 
        // FK to Store
        public virtual Store Store { get; set; }
        public Guid StoreId { get; set; }


        // Personal
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        // Business
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyRegistrationNumber { get; set; } = string.Empty;
        public string TaxIdentificationNumber { get; set; } = string.Empty;

        // Address
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        // Verification
        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string? VerificationDocument { get; set; }

        // Banking
        public string BankName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankRoutingNumber { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;

        // Agreement
        public bool HasAcceptedTerms { get; set; }
        public DateTime? TermsAcceptanceDate { get; set; }
        public string? TermsAcceptanceIp { get; set; }
    }

}
