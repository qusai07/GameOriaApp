using Gameoria.Domains.Common;
using Gameoria.Domains.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Games
{
    public class GameReview : BaseAuditableEntity
    {
        public Guid GameId { get; set; }
        public string UserId { get; set; } // Using string as it's ApplicationUser Id

        public decimal Rating { get; set; } // 1-5
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Game Game { get; set; }
        public virtual User.User User { get; set; }

        // Moderation
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }

        // Metrics
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int ReportsCount { get; set; }

        // Response
        public string? DeveloperResponse { get; set; }
        public DateTime? DeveloperResponseDate { get; set; }
    }
}
