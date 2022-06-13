using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Commands
{
    public class RemoveWorkingdayCommand : IRequest<Tenant>
    {
        public Guid TenantId { get; set; }
        public int Weekday { get; set; }
    }
}
