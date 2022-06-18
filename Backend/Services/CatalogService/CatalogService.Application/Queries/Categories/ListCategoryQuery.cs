using CatalogService.Core.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Categories
{
    public class ListCategoryQuery : IRequest<IReadOnlyCollection<Category>>
    {

    }
}
