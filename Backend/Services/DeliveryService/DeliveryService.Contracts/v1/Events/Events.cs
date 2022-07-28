using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.v1.Events
{
    public record DeliverySuccessSagaEvent(Guid CorrelationId, string? Message);
    public record DeliveryFailureSagaEvent(Guid CorrelationId, string? Message);
}
