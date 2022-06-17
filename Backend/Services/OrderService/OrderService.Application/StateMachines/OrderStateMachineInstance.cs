using MassTransit;
using OrderService.Core.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.StateMachines
{
    public class OrderStateMachineInstance : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }


        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> Items { get; set; } = new();
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }

        // meta
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ModifiedAt { get; set; } = null;

        // persistence
        public int Version { get; set; }
    }
}
