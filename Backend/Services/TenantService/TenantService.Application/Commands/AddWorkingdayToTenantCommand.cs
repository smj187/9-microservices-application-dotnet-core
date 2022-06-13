using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Commands
{
    public class AddWorkingdayToTenantCommand : IRequest<Tenant>
    {
        public Guid TenantId { get; set; }

        public int Workingday { get; set; }
        public int? OpeningHour { get; set; } = null;
        public int? ClosingHour { get; set; } = null;
        public int? OpeningMinute { get; set; } = null;
        public int? ClosingMinute { get; set; } = null;
    }
}
