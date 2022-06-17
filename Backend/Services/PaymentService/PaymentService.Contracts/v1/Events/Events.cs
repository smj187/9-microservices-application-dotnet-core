using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Events
{
    public record PaymentSuccessEvent(Guid CorrelationId, Guid OrderId, string? Message);
    public record PaymentFailureEvent(Guid CorrelationId, Guid OrderId, string? Message);
}
