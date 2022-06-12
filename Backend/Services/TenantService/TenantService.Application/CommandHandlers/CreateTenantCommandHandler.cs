using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;

namespace TenantService.Application.CommandHandlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Tenant>
    {
        public CreateTenantCommandHandler()
        {

        }

        public Task<Tenant> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
