using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Groups
{
    public class FindGroupQuery : IRequest<Group>
    {
        public Guid GroupId { get; set; }
    }
}
