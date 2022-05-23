using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Repositories.Products
{
    public interface IProductQueryRepository : IQueryRepository<Product>
    {

    }
}
