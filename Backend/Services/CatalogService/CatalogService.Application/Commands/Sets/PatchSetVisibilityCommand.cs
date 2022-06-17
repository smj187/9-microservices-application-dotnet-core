using CatalogService.Core.Domain.Set;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Sets
{
    public class PatchSetVisibilityCommand : IRequest<Set>
    {
        public Guid SetId { get; set; }

        public bool IsVisible { get; set; }
    }
}
