using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Sets
{
    public class RemoveProductFromSetCommand : IRequest<Set>
    {
        public Guid SetId { get; set; }
        public Guid ProductId { get; set; }
    }
}
