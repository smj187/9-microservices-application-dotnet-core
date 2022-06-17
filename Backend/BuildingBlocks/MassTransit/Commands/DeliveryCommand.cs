using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit.Commands
{
    public class DeliveryCommand
    {
        private readonly Guid _correlationId;
        private readonly Guid _orderId;
        private readonly Guid _userId;
        private readonly List<Guid> _items;

        public DeliveryCommand(Guid correlationId, Guid orderId, Guid userId, List<Guid> items)
        {
            _correlationId = correlationId;
            _orderId = orderId;
            _userId = userId;
            _items = items;
        }

        public Guid CorrelationId => _correlationId;
        public Guid OrderId => _orderId;
        public Guid UserId => _userId;
        public List<Guid> Items => _items;
    }
}
