using GameOria.Application.Orders.DTOs;
using GameOria.Domains.Entities.Orders;

namespace GameOria.Api.Repo.Interface
{
    public interface IOrderRepository
    {
        Task<OrderDto?> GetByIdAsync(Guid id, bool includeDetails = true);
        Task<List<OrderDto>> GetAllAsync(bool includeDetails = false);
        Task<Order> CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id); 
    }
}
