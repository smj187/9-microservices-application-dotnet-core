using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Events
{
    public record PaymentSuccessSagaEvent(Guid CorrelationId, Guid OrderId, string? Message = null);
    public record PaymentFailureSagaEvent(Guid CorrelationId, Guid OrderId, string? Message = null);
}
