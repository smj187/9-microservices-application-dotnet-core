using CatalogService.Core.Entities.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Categories
{
    public class PatchCategoryVisibilityCommand : IRequest<Category>
    {
        public Guid CategoryId { get; set; }
        public bool IsVisible { get; set; }
    }
}
