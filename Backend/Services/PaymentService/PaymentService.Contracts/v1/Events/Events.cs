using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Events
{
    public record PaymentProcessSagaSuccessEvent(Guid CorrelationId, string? Message = null);
    public record PaymentProcessSagaFailureEvent(Guid CorrelationId, string? Message = null);
}
