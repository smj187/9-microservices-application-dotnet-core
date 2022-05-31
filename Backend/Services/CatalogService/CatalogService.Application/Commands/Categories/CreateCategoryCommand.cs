using CatalogService.Core.Entities.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public Category NewCategory { get; set; } = default!;
    }
}
