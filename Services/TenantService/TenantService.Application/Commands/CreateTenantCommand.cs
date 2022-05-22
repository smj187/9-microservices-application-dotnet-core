using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;

namespace TenantService.Application.Commands
{
    public class CreateTenantCommand : IRequest<Tenant>
    {
        public Tenant NewTenant { get; set; } = default!;
    }
}
