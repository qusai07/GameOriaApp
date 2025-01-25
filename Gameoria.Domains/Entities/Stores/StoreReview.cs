using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Cards;
using Gameoria.Domains.Entities.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Stores
{
    public class StoreReview : BaseAuditableEntity
    {
        public Guid StoreId { get; set; }
        public string UserId { get; set; } // ApplicationUser Id

        // Review Content
        public int Rating { get; set; } // 1-5
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual Store Store { get; set; }
        public virtual User.User User { get; set; }

        // Moderation
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public bool IsReported { get; set; }
        public string? ReportReason { get; set; }

        // Metrics
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int ReportsCount { get; set; }

        // Store Response
        public string? StoreResponse { get; set; }
        public DateTime? StoreResponseDate { get; set; }
        public string? StoreResponseBy { get; set; }

        // Purchase Information
        public Guid? OrderId { get; set; }
        public DateTime? PurchaseDate { get; set; }

        // Review Metadata
        public string? ReviewerCountry { get; set; }
        public string? ReviewerDevice { get; set; }
        public string? ReviewerIp { get; set; }
    }
}
