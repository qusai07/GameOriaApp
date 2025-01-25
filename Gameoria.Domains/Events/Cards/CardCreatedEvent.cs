using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Events.Cards
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
