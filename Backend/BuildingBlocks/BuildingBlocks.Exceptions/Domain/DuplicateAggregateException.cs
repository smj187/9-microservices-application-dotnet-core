using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Domain
{
    public class DuplicateAggregateException : Exception
    {
        public DuplicateAggregateException(string message)
            : base(message)
        {

        }
    }
}
