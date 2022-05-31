using CatalogService.Core.Entities.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Groups
{
    public class CreateGroupCommand : IRequest<Group>
    {
        public Group NewGroup { get; set; } = default!;
    }
}
