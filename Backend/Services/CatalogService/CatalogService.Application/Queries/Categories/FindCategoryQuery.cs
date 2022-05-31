using CatalogService.Core.Entities.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Categories
{
    public class FindCategoryQuery : IRequest<Category>
    {
        public Guid CategoryId { get; set; }
    }
}
