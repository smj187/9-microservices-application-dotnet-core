using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.StateMachines.Responses
{
    public class CheckOrderStatusResponse
    {

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public List<Guid> Items { get; set; }

        public string Status { get; set; }
    }
}
