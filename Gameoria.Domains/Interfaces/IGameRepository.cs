using Gameoria.Domains.Entities.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesByStoreAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Game>> GetFeaturedGamesAsync(int count, CancellationToken cancellationToken = default);
        Task<IEnumerable<Game>> SearchGamesAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<bool> IsGameCodeAvailableAsync(string code, CancellationToken cancellationToken = default);
        Task<IEnumerable<GameCode>> GetAvailableCodesForGameAsync(Guid gameId, int count, CancellationToken cancellationToken = default);
        Task<Game?> GetGameWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<GameReview>> GetGameReviewsAsync(Guid gameId, CancellationToken cancellationToken = default);
        Task UpdateGameInventoryAsync(Guid gameId, int quantity, CancellationToken cancellationToken = default);
    }
}
