using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Commands
{
    public class CreateTenantCommand : IRequest<Tenant>
    {
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Zip { get; set; } = default!;
    }
}
