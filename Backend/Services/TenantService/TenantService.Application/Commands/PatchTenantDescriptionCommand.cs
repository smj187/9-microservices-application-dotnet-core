using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Commands
{
    public class PatchTenantDescriptionCommand : IRequest<Tenant>
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null;
    }
}
