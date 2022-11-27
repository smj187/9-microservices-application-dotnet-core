using BuildingBlocks.EfCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Aggregates
{
    public interface IApplicationUserRepository : IEfRepository<ApplicationUser>
    {

    }
}
