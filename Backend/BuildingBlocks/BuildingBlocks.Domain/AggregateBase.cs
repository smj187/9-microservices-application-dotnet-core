using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class AggregateBase : EntityBase, IAggregateBase
    {
        private Guid _id;
        private List<IDomainEvent>? _events;

        protected AggregateBase()
        {
            _id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.Now;
        }

        public override Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        public IReadOnlyCollection<IDomainEvent>? DomainEvents => _events?.AsReadOnly();


        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_events == null)
            {
                _events = new();
            }
            _events.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            if (_events != null)
            {
                _events.Clear();
            }
        }
    }
}
