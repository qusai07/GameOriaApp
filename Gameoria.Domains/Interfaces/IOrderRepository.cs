using Gameoria.Domains.Entities.Orders;
using Gameoria.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByStoreAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderCode>> GetOrderCodesAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task AssignCodesToOrderAsync(Guid orderId, IEnumerable<OrderCode> codes, CancellationToken cancellationToken = default);
    }
}
