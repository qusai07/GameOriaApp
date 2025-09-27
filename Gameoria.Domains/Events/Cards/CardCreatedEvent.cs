

using GameOria.Domains.Common;
using GameOria.Domains.Entities.Cards;

namespace GameOria.Domains.Events.Cards
{
    public class CardCreatedEvent : BaseEntity
    {
        public Card Card { get; }

        public CardCreatedEvent(Card card)
        {
            Card = card;
        }
    }
}
