using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Queries
{
    public class FindTenantQuery : IRequest<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
