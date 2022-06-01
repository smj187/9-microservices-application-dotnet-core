using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string entity, Guid id) 
            : base($"Aggreate '{entity.ToLower()}' [{id}] was not found.")
        {

        }
    }
}
