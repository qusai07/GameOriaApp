using GameOria.Domains.Common;


namespace GameOria.Domains.Entities.Games
{
    public class GameImage : BaseAuditableEntity
    { 
        public Guid GameId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
        public string ImageType { get; set; } = string.Empty; // e.g., "Screenshot", "Cover", "Banner"
        public string AltText { get; set; } = string.Empty;

        // Navigation property
        public virtual Game Game { get; set; }

        // Image metadata
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public GameImage(string url, string altText, int displayOrder, Guid gameId)
        {
            Url = url;
            AltText = altText;
            DisplayOrder = displayOrder;
            GameId = gameId;
        }
    }
}
