using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Interfaces
{
    public interface IAggregateRoot : IEntity<Guid>
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
