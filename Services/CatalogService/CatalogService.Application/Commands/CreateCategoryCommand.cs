using CatalogService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public Category NewCategory { get; set; } = default!;
    }
}
