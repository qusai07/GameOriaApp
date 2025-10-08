using GameOria.Domains.Common;


namespace GameOria.Domains.Entities.Games
{
    public class GameImage : BaseAuditableEntity
    {
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }

        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
        public string ImageType { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

}
