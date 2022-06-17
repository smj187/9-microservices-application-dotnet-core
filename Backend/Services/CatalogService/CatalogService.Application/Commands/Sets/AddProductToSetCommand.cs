using CatalogService.Core.Domain.Set;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Sets
{
    public class AddProductToSetCommand : IRequest<Set>
    {
        public Guid SetId { get; set; }
        public Guid ProductId { get; set; }
    }
}
