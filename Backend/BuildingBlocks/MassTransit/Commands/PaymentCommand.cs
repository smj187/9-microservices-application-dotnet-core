using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit.Commands
{
    public class PaymentCommand
    {
        private readonly Guid _correlationId;
        private readonly Guid _orderId;
        private readonly decimal _amount;

        public PaymentCommand(Guid correlationId, Guid orderId, decimal amount)
        {
            _correlationId = correlationId;
            _orderId = orderId;
            _amount = amount;
        }

        public Guid CorrelationId => _correlationId;
        public Guid OrderId => _orderId;
        public decimal Amount => _amount;
    }
}
