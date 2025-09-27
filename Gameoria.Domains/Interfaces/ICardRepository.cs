
using GameOria.Domains.Entities.Cards;

namespace GameOria.Domains.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<Card>> GetCardsByStoreAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Card>> GetCardsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<bool> IsCardCodeAvailableAsync(string code, CancellationToken cancellationToken = default);
        Task<IEnumerable<CardCode>> GetAvailableCodesForCardAsync(Guid cardId, int count, CancellationToken cancellationToken = default);
        Task<Card?> GetCardWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateCardInventoryAsync(Guid cardId, int quantity, CancellationToken cancellationToken = default);
    }
}
