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
            _events = new();
        }

        public AggregateRoot(Guid id)
            : base(id)
        {
            _events = new();
        }



        public bool IsDeleted { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => GetDomainEvents();

        private Queue<IDomainEvent> GetDomainEvents()
        {
            if(_events == null)
            {
                return new Queue<IDomainEvent>();
            }

            return _events;
        }

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
