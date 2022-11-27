using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Domain
{
    public sealed class AggregateNotFoundException : DomainViolationException
    {
        private readonly Guid _id;

        public AggregateNotFoundException(string message, Guid id) : base(message)
        {
            _id = id;
        }

        public Guid Id => _id;
    }
}
