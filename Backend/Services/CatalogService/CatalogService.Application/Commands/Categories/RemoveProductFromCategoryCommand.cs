using CatalogService.Core.Domain.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Categories
{
    public class RemoveProductFromCategoryCommand : IRequest<Category>
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }
}
