//using GameOria.Api.Repo.Interface;
//using GameOria.Application.Orders.Service;
//using GameOria.Domains.Entities.Cards;
//using GameOria.Domains.Entities.Games;
//using GameOria.Domains.Entities.Orders;
//using Microsoft.AspNetCore.Mvc;

//namespace GameOria.Api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class OrderController : ControllerBase
//    {
//        private readonly IOrderRepository _orderRepository;
//        private readonly IOrderItemRepository _orderItemRepository;
//        private readonly IOrderCodeRepository _orderCodeRepository;
//        private readonly IDataService _dateService;

//        public OrderController(
//            IOrderRepository orderRepository,
//            IOrderItemRepository orderItemRepository,
//            IOrderCodeRepository orderCodeRepository,
//            IDataService dateService)
//        {
//            _orderRepository = orderRepository;
//            _orderItemRepository = orderItemRepository;
//            _orderCodeRepository = orderCodeRepository;
//            _dateService = dateService;
//        }

//        // GET: api/Order
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var orders = await _orderRepository.GetAllAsync();
//            return Ok(orders);
//        }

//        // GET: api/Order/{id}
//        [HttpGet("{id:guid}")]
//        public async Task<IActionResult> GetById(Guid id)
//        {
//            var order = await _orderRepository.GetByIdAsync(id, includeDetails: true);
//            if (order == null)
//                return NotFound();

//            return Ok(order);
//        }

//        // POST: api/Order
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] Order order)
//        {
//            if (order == null)
//                return BadRequest();

//            //order.CreatedAt = _dateService.Now();
//            //order.UpdatedAt = _dateService.Now();

//            var createdOrder = await _orderRepository.CreateAsync(order);
//            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
//        }

//        // PUT: api/Order/{id}
//        [HttpPut("{id:guid}")]
//        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
//        {
//            var existingOrder = await _orderRepository.GetByIdAsync(id);
//            if (existingOrder == null)
//                return NotFound();

//            existingOrder.Status = order.Status;
//            //existingOrder.UpdatedAt = _dateService.Now();
//            // other fields you want to allow updating

//            await _orderRepository.UpdateAsync(existingOrder);
//            return NoContent();
//        }

//        // DELETE: api/Order/{id}
//        [HttpDelete("{id:guid}")]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            var success = await _orderRepository.DeleteAsync(id);
//            if (!success) return NotFound();
//            return NoContent();
//        }

//        // POST: api/Order/{orderId}/Item
//        [HttpPost("{orderId:guid}/Item")]
//        public async Task<IActionResult> AddItem(Guid orderId, [FromBody] OrderItem item)
//        {
//            var order = await _orderRepository.GetByIdAsync(orderId);
//            if (order == null) return NotFound("Order not found");

//            item.OrderId = orderId;
//            item.StoreId = item.StoreId; // keep store info
//            await _orderItemRepository.AddAsync(item);

//            return Ok(item);
//        }

//        // GET: api/Order/{orderId}/Items
//        [HttpGet("{orderId:guid}/Items")]
//        public async Task<IActionResult> GetItems(Guid orderId)
//        {
//            var items = await _orderItemRepository.GetByOrderIdAsync(orderId);
//            return Ok(items);
//        }

//        // POST: api/Order/{orderId}/Item/{itemId}/Code/Game
//        [HttpPost("{orderId:guid}/Item/{itemId:guid}/Code/Game")]
//        public async Task<IActionResult> AssignGameCode(Guid orderId, Guid itemId, [FromBody] GameCode code)
//        {
//            var item = await _orderItemRepository.GetByIdAsync(itemId);
//            if (item == null) return NotFound();

//            var orderCode = OrderCode.CreateFromGameCode(item, code);
//            await _orderCodeRepository.AddAsync(orderCode);

//            return Ok(orderCode);
//        }

//        // POST: api/Order/{orderId}/Item/{itemId}/Code/Card
//        [HttpPost("{orderId:guid}/Item/{itemId:guid}/Code/Card")]
//        public async Task<IActionResult> AssignCardCode(Guid orderId, Guid itemId, [FromBody] CardCode code)
//        {
//            var item = await _orderItemRepository.GetByIdAsync(itemId);
//            if (item == null) return NotFound();

//            var orderCode = OrderCode.CreateFromCardCode(item, code);
//            await _orderCodeRepository.AddAsync(orderCode);

//            return Ok(orderCode);
//        }

//        // GET: api/Order/{orderId}/Codes
//        [HttpGet("{orderId:guid}/Codes")]
//        public async Task<IActionResult> GetCodes(Guid orderId)
//        {
//            var order = await _orderRepository.GetByIdAsync(orderId, includeDetails: true);
//            if (order == null) return NotFound();

//            return Ok(order.OrderItems.SelectMany(i => i.OrderCodes));
//        }
//    }
//}
