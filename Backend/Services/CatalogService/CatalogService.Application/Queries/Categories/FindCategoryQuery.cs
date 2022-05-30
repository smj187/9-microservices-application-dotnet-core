using CatalogService.Core.Entities;
using CatalogService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Categories
{
    public class FindCategoryQuery : IRequest<CategoryDetailsModel>
    {
        public Guid CategoryId { get; set; }
    }
}
