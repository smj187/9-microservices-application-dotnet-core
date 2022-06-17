using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.v1.Events
{
    public record DeliverySuccessEvent(Guid CorrelationId, Guid OrderId, string? Message);
    public record DeliveryFailureEvent(Guid CorrelationId, Guid OrderId, string? Message);
}
