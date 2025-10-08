

using GameOria.Domains.Common;
using GameOria.Domains.Entities.Games;

namespace GameOria.Domains.Events.Games
{
    public class GameUpdatedEvent : BaseEntity
    {
        public Game Game { get; }
        public Game OldGame { get; }
        public string[] ModifiedProperties { get; }
        public string UpdatedBy { get; }
        public DateTime UpdatedAt { get; }

        public GameUpdatedEvent(Game newGame, Game oldGame, string[] modifiedProperties, string updatedBy)
        {
            Game = newGame;
            OldGame = oldGame;
            ModifiedProperties = modifiedProperties;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // Helper method to check if a specific property was modified
        public bool IsPropertyModified(string propertyName)
        {
            return ModifiedProperties.Contains(propertyName);
        }

        // Helper method to get changes in price
        public (bool HasChanged, decimal OldPrice, decimal NewPrice) GetPriceChanges()
        {
            if (IsPropertyModified(nameof(Game.Price)))
            {
                return (true, OldGame.Price.Amount, Game.Price.Amount);
            }
            return (false, 0, 0);
        }

        // Helper method to get all significant changes
        public Dictionary<string, (object OldValue, object NewValue)> GetSignificantChanges()
        {
            var changes = new Dictionary<string, (object OldValue, object NewValue)>();

            foreach (var property in ModifiedProperties)
            {
                var prop = typeof(Game).GetProperty(property);
                if (prop != null)
                {
                    var oldValue = prop.GetValue(OldGame);
                    var newValue = prop.GetValue(Game);
                    changes.Add(property, (oldValue, newValue));
                }
            }

            return changes;
        }

        // Helper method to determine if the update requires notification
        public bool RequiresNotification()
        {
            return IsPropertyModified(nameof(Game.Price)) ||
                   //IsPropertyModified(nameof(Game.IsAvailable)) ||
                   IsPropertyModified(nameof(Game.IsPublished));
        }

        // Helper method to determine if the update requires moderation
        public bool RequiresModeration()
        {
            return IsPropertyModified(nameof(Game.Price)) ||
                   IsPropertyModified(nameof(Game.Description)) ||
                   IsPropertyModified(nameof(Game.HasInAppPurchases));
        }

        // Helper method to get update summary
        public string GetUpdateSummary()
        {
            var changes = GetSignificantChanges();
            var summary = new List<string>();

            foreach (var change in changes)
            {
                summary.Add($"{change.Key}: {change.Value.OldValue} -> {change.Value.NewValue}");
            }

            return string.Join(", ", summary);
        }

        // Helper method to check if critical fields were modified
        public bool HasCriticalChanges()
        {
            var criticalFields = new[]
            {
                nameof(Game.Price),
                //nameof(Game.IsAvailable),
                nameof(Game.Platform),
                nameof(Game.Region)
            };

            return ModifiedProperties.Any(p => criticalFields.Contains(p));
        }
    }
}
