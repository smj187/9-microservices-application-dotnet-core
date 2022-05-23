using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> ListOrderAsync();
        Task<Order> CreateOrderAsync(Order order);
    }
}
