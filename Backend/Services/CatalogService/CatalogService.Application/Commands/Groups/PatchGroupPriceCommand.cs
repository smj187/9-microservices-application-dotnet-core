using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Groups
{
    public class PatchGroupPriceCommand : IRequest<Group>
    {
        public Guid GroupId { get; set; }

        public decimal Price { get; set; }
    }
}
