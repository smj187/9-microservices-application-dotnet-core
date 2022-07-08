using CatalogService.Core.Domain.Categories;
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
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = null!;
        public List<Guid>? Products { get; set; } = null!;
        public List<Guid>? Sets { get; set; } = null!;
    }
}
