using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.StateMachines.Events
{
    public record CreateOrderSagaEvent(Guid BasketId, Guid UserId, Guid TenantId, List<Guid> Products, List<Guid> Sets);
    
}
