using GameOria.Api.Repo.Interface;
using GameOria.Application.Orders.Service;
using GameOria.Domains.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IDataService _dataService;

        public OrderItemRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<OrderItem?> GetByIdAsync(Guid id)
        {
            return await _dataService.GetByIdAsync<OrderItem>(id);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
        {
            return await _dataService.Query<OrderItem>()
                                     .Where(x => x.OrderId == orderId)
                                     .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _dataService.GetAllAsync<OrderItem>();
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _dataService.AddAsync(orderItem);
            await _dataService.SaveAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            await _dataService.UpdateAsync(orderItem);
            await _dataService.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _dataService.DeleteAsync<OrderItem>(id);
            await _dataService.SaveAsync();
        }
    }
}
