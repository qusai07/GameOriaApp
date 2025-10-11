using GameOria.Application.Stores.DTOs;
using GameOria.Domains.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Stores.Service
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllAsync();

        Task<Store?> GetByIdAsync(Guid id);

        Task<Store> CreateAsync(Store store);

        Task<bool> DeleteAsync(Guid id);

        Task CompleteStoreProfile(Store storeOwner);

        Task EditAsync(Store baseStore);

        Task<Store?> GetStoreOwnerByIdAsync(Guid UserId);

    }
}
