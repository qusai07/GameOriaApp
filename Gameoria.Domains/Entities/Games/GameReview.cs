
using GameOria.Domains.Common;
using GameOria.Domains.Entities.Identity;


namespace GameOria.Domains.Entities.Games
{
    public class GameReview : BaseAuditableEntity
    {
        public Guid GameId { get; set; }
        public decimal Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Game Game { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public int ReportsCount { get; set; }

        public string? DeveloperResponse { get; set; }
        public DateTime? DeveloperResponseDate { get; set; }
    }

}
