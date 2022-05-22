using Microsoft.EntityFrameworkCore;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _orderContext.Add(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> ListOrderAsync()
        {
            return await _orderContext.Orders.ToListAsync();
        }
    }
}
