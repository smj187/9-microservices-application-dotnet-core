using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.Interfaces
{
    public interface IMultitenancyService
    {
        string GetTenantId();
        string GetConnectionString();
    }
}
