using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries
{
    public class ListCategoriesQuery : IRequest<IEnumerable<Category>>
    {

    }
}
