using GameOria.Domains.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Orders.Service
{
    public interface IOrderItemRepository
    {
        Task<OrderItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId);
        Task<IEnumerable<OrderItem>> GetAllAsync();

        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(Guid id);
    }
}
