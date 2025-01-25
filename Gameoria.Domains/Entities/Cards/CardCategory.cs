using Gameoria.Domains.Common;


namespace Gameoria.Domains.Entities.Cards
{
    public class CardCategory : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
