using GameOria.Api.Repo.Interface;
using GameOria.Application.Orders.Service;
using GameOria.Domains.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Infrastructure.Repositories
{
    public class OrderCodeRepository : IOrderCodeRepository
    {
        private readonly IDataService _dataService;

        public OrderCodeRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<OrderCode?> GetByIdAsync(Guid id)
        {
            return await _dataService.GetByIdAsync<OrderCode>(id);
        }

        public async Task<IEnumerable<OrderCode>> GetByOrderIdAsync(Guid orderId)
        {
            return await _dataService.Query<OrderCode>()
                                     .Where(x => x.OrderId == orderId)
                                     .ToListAsync();
        }

        public async Task<IEnumerable<OrderCode>> GetAllAsync()
        {
            return await _dataService.GetAllAsync<OrderCode>();
        }

        public async Task AddAsync(OrderCode orderCode)
        {
            await _dataService.AddAsync(orderCode);
            await _dataService.SaveAsync();
        }

        public async Task UpdateAsync(OrderCode orderCode)
        {
            await _dataService.UpdateAsync(orderCode);
            await _dataService.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _dataService.DeleteAsync<OrderCode>(id);
            await _dataService.SaveAsync();
        }
    }
}
