using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Interfaces
{
    public interface IDomainEvent
    {

    }

    public interface IDomainEvent<out TKey> : IDomainEvent
    {
        long AggregateVersion { get; }
        TKey AggregateId { get; }
        DateTime Timestamp { get; }
    }
}
