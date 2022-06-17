using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.StateMachines.Responses
{
    public class OrderNotFoundResponse
    {
        public Guid OrderId { get; set; }

        public string Message { get; set; }
    }
}
