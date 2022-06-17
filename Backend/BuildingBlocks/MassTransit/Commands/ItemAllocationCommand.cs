using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit.Commands
{
    public class ItemAllocationCommand
    {
        private readonly Guid _correlationId;
        private readonly Guid _orderId;
        private readonly List<Guid> _items;

        public ItemAllocationCommand(Guid correlationId, Guid orderId, List<Guid> items)
        {
            _correlationId = correlationId;
            _orderId = orderId;
            _items = items;
        }

        public Guid CorrelationId => _correlationId;
        public Guid OrderId => _orderId;
        public List<Guid> Items => _items;
    }
}
