using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Groups
{
    public class PatchGroupVisibilityCommand : IRequest<Group>
    {
        public Guid GroupId { get; set; }

        public bool IsVisible { get; set; }
    }
}
