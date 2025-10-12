using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Stores.DTOs
{
  
    public class StorOrganizerDto
        {
            // Basic Info
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string LogoUrl { get; set; } = string.Empty;
            public string CoverImageUrl { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public bool IsVerified { get; set; }

            // Contact
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string ShortcutWebsite { get; set; } = string.Empty;

            // Metrics
            public decimal AverageRating { get; set; }
            public int TotalReviews { get; set; }
            public int TotalSales { get; set; }
            public DateTime? LastSaleDate { get; set; }

            // Settings
            public bool AutoApproveReviews { get; set; }
            public bool AllowDigitalProducts { get; set; }
            public string[] AcceptedPaymentMethods { get; set; } = Array.Empty<string>();
            public string[] SupportedCurrencies { get; set; } = Array.Empty<string>();

            // Commission
            public decimal CommissionRate { get; set; }
            public string CommissionType { get; set; } = string.Empty;

            // Owner Info
            public Guid UserId { get; set; }
            public string OwnerFirstName { get; set; } = string.Empty;
            public string OwnerLastName { get; set; } = string.Empty;
            public string OwnerEmail { get; set; } = string.Empty;
            public string OwnerPhone { get; set; } = string.Empty;
            public DateTime OwnerDateOfBirth { get; set; }

            // Verification
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
   

    public class BaseStore
    { 
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public bool IsVerified { get; set; }
        public string StoreName { get; set; } 
    }
    public class CompleteStore : BaseStore
    {
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; } 
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public DateTime OwnerDateOfBirth { get; set; }
        public string? LogoUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string Title { get; set; } 
        public string TitleDescription { get; set; }
        public string ShortcutWebsite { get; set; } 

    }
    //public class StoreReviewDto : Base
    //{
    //    public string UserId { get; set; } = string.Empty;
    //    public int Rating { get; set; }
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;

    //    public bool IsApproved { get; set; }
    //    public DateTime? ApprovedAt { get; set; }
    //    public string? ApprovedBy { get; set; }
    //}
}
