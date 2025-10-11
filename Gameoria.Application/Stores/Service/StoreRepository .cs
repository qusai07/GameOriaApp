using GameOria.Api.Repo.Interface;
using GameOria.Application.Stores.DTOs;
using GameOria.Domains.Entities.Stores;
using Microsoft.EntityFrameworkCore;
namespace GameOria.Application.Stores.Service
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IDataService _dataService;

        public StoreRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<Store>> GetAllAsync()
        {
            return await _dataService.Query<Store>().ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(Guid id)
        {
            return await _dataService.GetByIdAsync<Store>(id);
        }

        public async Task<Store?> GetStoreOwnerByIdAsync(Guid UserId)
        {
            return await _dataService.Query<Store>()
                .FirstOrDefaultAsync(s => s.UserId == UserId);
        }

        public async Task<Store> CreateAsync(Store store)
        {
            await _dataService.AddAsync(store);
            await _dataService.SaveAsync();
            return store;
        }
        public async Task EditAsync(Store store)
        {
            await _dataService.UpdateAsync(store);
            await _dataService.SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var store = await _dataService.GetByIdAsync<Store>(id);
            if (store == null) return false;

            await _dataService.DeleteAsync<Store>(store);
            await _dataService.SaveAsync();
            return true;
        }

        public async Task CompleteStoreProfile(Store store)
        {
            await _dataService.UpdateAsync(store);
            await _dataService.SaveAsync();
        }

    }
}
