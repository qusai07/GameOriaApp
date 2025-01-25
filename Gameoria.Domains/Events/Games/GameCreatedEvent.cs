using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameoria.Domains.Common;
using Gameoria.Domains.Entities.Games;

namespace Gameoria.Domains.Events.Games
{
    public class GameCreatedEvent : BaseEntity
    {
        private Game game;

        private Game Game { get; }
        public string CreatedBy { get; }
        public DateTime CreatedAt { get; }
        public Guid StoreId { get; }

        public GameCreatedEvent(Game game, string createdBy, Guid storeId)
        {
            Game = game;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            StoreId = storeId;
        }

        public GameCreatedEvent(Game game)
        {
            this.game = game;
        }

        // Helper methods for event handling
        public Dictionary<string, object> GetGameMetadata()
        {
            return new Dictionary<string, object>
            {
                { "GameId", Game.Id },
                { "GameName", Game.Name },
                { "StoreId", StoreId },
                { "CreatedBy", CreatedBy },
                { "CreatedAt", CreatedAt },
                { "Price", Game.Price },
                { "Category", Game.CategoryId },
                { "Platform", Game.Platform },
                { "Region", Game.Region }
            };
        }

        public bool RequiresModeration()
        {
            // Logic to determine if the game needs moderation
            return Game.Price.Amount > 1000 ||
                   Game.IsPreOrder ||
                   Game.HasInAppPurchases;
        }

        public bool RequiresNotification()
        {
            // Logic to determine if notifications should be sent
            return Game.IsPublished &&
                   !Game.IsDraft &&
                   Game.IsAvailable;
        }
    }
}
