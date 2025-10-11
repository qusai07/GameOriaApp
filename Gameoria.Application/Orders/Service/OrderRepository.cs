using GameOria.Api.Repo.Interface;
using GameOria.Application.Orders.DTOs;
using GameOria.Domains.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Orders.Service
{
    //Projection 
    public class OrderRepository : IOrderRepository
    {
        private readonly IDataService _dataService;

        public OrderRepository(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id, bool includeDetails = true)
        {
            return await _dataService.GetByIdAsync<OrderDto>(id);

            //var query = _dataService.Query<Order>().Where(o => o.Id == id);

            //if (includeDetails)
            //{
            //    query = query.Include(o => o.Items)
            //                 .Include(o => o.Codes);
            //}

            //return await query.Select(o => new OrderDto
            //{
            //    Id = o.Id,
            //    OrderNumber = o.OrderNumber,
            //    UserId = o.UserId,
            //    TotalAmount = new MoneyDto
            //    {
            //        Amount = o.TotalAmount.Amount,
            //        Currency = o.TotalAmount.Currency
            //    },
            //    Status = o.Status,
            //    PaymentStatus = o.PaymentStatus,
            //    Items = includeDetails ? o.Items.Select(i => new OrderItemDto
            //    {
            //        ProductName = i.ProductName,
            //        Quantity = i.Quantity,
            //        UnitPrice = new MoneyDto
            //        {
            //            Amount = i.UnitPrice.Amount,
            //            Currency = i.UnitPrice.Currency
            //        }
            //    }).ToList() : null,
            //    Codes = includeDetails ? o.Codes.Select(c => new OrderCodeDto
            //    {
            //        Code = c.Code,
            //        ProductType = c.ProductType
            //    }).ToList() : null
            //}).FirstOrDefaultAsync();
        }

        public async Task<List<OrderDto>> GetAllAsync(bool includeDetails = false)
        {

            var query = _dataService.Query<Order>();

            if (includeDetails)
            {
                query = query.Include(o => o.Items)
                             .Include(o => o.Codes);
            }

            return await query.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                UserId = o.UserId,
                TotalAmount = new MoneyDto
                {
                    Amount = o.TotalAmount.Amount,
                    Currency = o.TotalAmount.Currency
                },
                Status = o.Status,
                PaymentStatus = o.PaymentStatus,
                Items = includeDetails ? o.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = new MoneyDto
                    {
                        Amount = i.UnitPrice.Amount,
                        Currency = i.UnitPrice.Currency
                    }
                }).ToList() : null,
                Codes = includeDetails ? o.Codes.Select(c => new OrderCodeDto
                {
                    Code = c.Code,
                    ProductType = c.ProductType
                }).ToList() : null
            }).ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _dataService.AddAsync(order);
            await _dataService.SaveAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            await _dataService.UpdateAsync(order);
            await _dataService.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _dataService.GetByIdAsync<Order>(id);
            if (order != null)
            {
                await  _dataService.DeleteAsync<Order>(order);
                await _dataService.SaveAsync();
            }
        }
    }


}
