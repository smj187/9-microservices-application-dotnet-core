using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message)
        {

        }

        public AggregateNotFoundException(string type, Guid id) : base($"No aggregate ({type}) for {id} was found")
        {

        }
    }
}
