using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Extensions
{
    public static class NullOrNegativGuard
    {
        public static decimal NullOrNegativ(this IGuardClause guardClause, decimal input, string parameterName)
        {
            if (input < 0)
            {
                throw new ArgumentNullException("Value cannot be negativ", parameterName);
            }

            return input;
        }
    }
}
