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
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Zip { get; set; } = default!;


        public decimal MinimunOrderAmount { get; set; }
        public bool IsFreeDelivery { get; set; }
        public decimal? DeliveryCost { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Imprint { get; set; }
    }
}
