using GameOria.Domains.Common;

namespace GameOria.Domains.Entities.Games
{
    public class GameCategory : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    }

}
