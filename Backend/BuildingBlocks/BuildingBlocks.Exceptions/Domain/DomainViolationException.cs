using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Domain
{
    public abstract class DomainViolationException : Exception
    {
        public DomainViolationException(string message)
            : base(message)
        {

        }
    }
}
