using GameOria.Domains.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Orders.Service
{
    public interface IOrderCodeRepository
    {
        Task<OrderCode?> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderCode>> GetByOrderIdAsync(Guid orderId);
        Task<IEnumerable<OrderCode>> GetAllAsync();

        Task AddAsync(OrderCode orderCode);
        Task UpdateAsync(OrderCode orderCode);
        Task DeleteAsync(Guid id);
    }
}
