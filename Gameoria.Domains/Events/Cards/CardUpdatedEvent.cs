using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Events.Cards
{
    public class CardUpdatedEvent : BaseEntity
    {
        public Card Card { get; }
        public Card OldCard { get; }
        public string[] ModifiedProperties { get; }

        public CardUpdatedEvent(Card newCard, Card oldCard, string[] modifiedProperties)
        {
            Card = newCard;
            OldCard = oldCard;
            ModifiedProperties = modifiedProperties;
        }

        // Helper method to check if a specific property was modified
        public bool IsPropertyModified(string propertyName)
        {
            return ModifiedProperties.Contains(propertyName);
        }

        // Helper method to get old value for a property if it was modified
        public T GetOldValue<T>(string propertyName)
        {
            if (IsPropertyModified(propertyName))
            {
                var property = typeof(Card).GetProperty(propertyName);
                return (T)property?.GetValue(OldCard);
            }
            return default;
        }

        // Helper method to get significant changes
        public Dictionary<string, (object OldValue, object NewValue)> GetSignificantChanges()
        {
            var changes = new Dictionary<string, (object OldValue, object NewValue)>();

            foreach (var property in ModifiedProperties)
            {
                var prop = typeof(Card).GetProperty(property);
                if (prop != null)
                {
                    var oldValue = prop.GetValue(OldCard);
                    var newValue = prop.GetValue(Card);
                    changes.Add(property, (oldValue, newValue));
                }
            }

            return changes;
        }
    }
}
