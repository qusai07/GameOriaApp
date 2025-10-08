using GameOria.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Orders.DTOs
{
    // Order DTO
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public MoneyDto TotalAmount { get; set; } = null!;
        public List<OrderItemDto> Items { get; set; } = new();
        public List<OrderCodeDto> Codes { get; set; } = new();
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public MoneyDto UnitPrice { get; set; } = null!;
    }

    public class OrderCodeDto
    {
        public string Code { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
    }

    public class MoneyDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }


}
