using GameOria.Api.Repo.Interface;
using GameOria.Application.Orders.DTOs;
using GameOria.Domains.Entities.Orders;
using Microsoft.AspNetCore.Mvc;

namespace GameOria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll(bool includeDetails = false)
        {
            var orders = await _orderRepository.GetAllAsync(includeDetails);
            return Ok(orders);
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id, bool includeDetails = true)
        {
            var order = await _orderRepository.GetByIdAsync(id, includeDetails);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] Order order)
        {
            var createdOrder = await _orderRepository.CreateAsync(order);
            // يمكن تحويله لـ DTO قبل الإرجاع
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }

        // PUT: api/Orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest();
            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        // DELETE: api/Orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
