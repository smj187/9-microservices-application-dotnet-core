using CatalogService.Core.Domain.Set;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Sets
{
    public class FindSetQuery : IRequest<Set>
    {
        public Guid SetId { get; set; }
    }
}
