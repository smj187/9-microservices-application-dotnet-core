using CatalogService.Core.Domain.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Groups
{
    public class PatchGroupQuantityCommand : IRequest<Group>
    {
        public Guid GroupId { get; set; }
        public int? Quantity { get; set; }
    }
}
