using Gameoria.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Entities.Games
{
    public class GameCategory : BaseAuditableEntity
    {
        public Guid GameId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get;  set; } = string.Empty;
        public string Description { get;  set; } = string.Empty; 
        public string IconUrl { get;  set; } = string.Empty ;

        public virtual ICollection<Game> Games { get;  set; } = new List<Game>();
        public GameCategory(Guid gameId, Guid categoryId)
        {
            GameId = gameId;
            CategoryId = categoryId;
        }

    }
}
