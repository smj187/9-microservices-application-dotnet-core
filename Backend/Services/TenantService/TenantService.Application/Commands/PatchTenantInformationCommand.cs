using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Commands
{
    public class PatchTenantInformationCommand : IRequest<Tenant>
    {
        public Guid TenantId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null;

        public decimal MinimunOrderAmount { get; set; } = default!;
        public bool IsFreeDelivery { get; set; } = default!;
        public decimal? DeliveryCost { get; set; } = null;
        public string? WebsiteUrl { get; set; } = null;
        public string? Imprint { get; set; } = null;

        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Payments { get; set; } = null;
    }
}
