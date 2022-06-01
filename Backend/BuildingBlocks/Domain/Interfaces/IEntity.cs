using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }

        DateTimeOffset CreatedAt { get; }
        DateTimeOffset? ModifiedAt { get; }

        bool IsDeleted { get; }
    }
}
