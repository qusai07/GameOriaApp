using Gameoria.Domains.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store?> GetStoreWithProductsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Store>> GetStoresByOrganizerAsync(string organizerId, CancellationToken cancellationToken = default);
        Task<bool> IsStoreNameUniqueAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<StoreReview>> GetStoreReviewsAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task UpdateStoreStatisticsAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<bool> VerifyStoreOwnershipAsync(Guid storeId, string userId, CancellationToken cancellationToken = default);
    }
}
