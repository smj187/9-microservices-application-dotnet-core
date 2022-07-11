using MassTransit;
using MassTransit.Saga;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Core.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.StateMachines
{
    public class OrderStateMachineInstance : SagaStateMachineInstance, ISagaVersion
    {
        [BsonId]
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;
        public string TenantId { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid BasketId { get; set; }
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public decimal TotalAmount { get; set; }

        // meta
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ModifiedAt { get; set; } = null;

        // persistence
        public int Version { get; set; }
    }
}
