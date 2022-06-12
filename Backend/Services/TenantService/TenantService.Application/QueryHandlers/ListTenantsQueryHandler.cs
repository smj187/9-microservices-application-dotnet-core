using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;
using TenantService.Core.Entities;

namespace TenantService.Application.QueryHandlers
{
    public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, IEnumerable<Tenant>>
    {
        public ListTenantsQueryHandler()
        {

        }

        public Task<IEnumerable<Tenant>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
