using BuildingBlocks.EfCore.Repositories;
using IdentityService.Core.Aggregates;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Repositories
{
    public class ApplicationUserRepository : EfRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IdentityContext context)
            : base(context)
        {

        }
    }
}
