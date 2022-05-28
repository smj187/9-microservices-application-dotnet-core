using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly Queue<IDomainEvent> _events = new();


        public AggregateRoot()
        {
            IsDeleted = false;
        }


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
