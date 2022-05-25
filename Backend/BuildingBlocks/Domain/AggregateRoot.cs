using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly Queue<IDomainEvent> _events = new();

        private readonly Guid _id;


        public AggregateRoot()
        {
            // TODO: id generation
            _id = Guid.NewGuid();

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
            IsDeleted = false;
        }

        public Guid Id => _id;

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? ModifiedAt { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.ToImmutableArray();

        public void ClearDomainEvents()
        {
            _events.Clear();
        }

        public void AddEvent(IDomainEvent domainEvent)
        {
            _events.Enqueue(domainEvent);
        }
    }
}
