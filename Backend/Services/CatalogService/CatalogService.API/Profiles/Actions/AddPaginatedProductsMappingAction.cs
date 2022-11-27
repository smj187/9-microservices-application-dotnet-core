using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class AddPaginatedProductsMappingAction : IMappingAction<PaginatedProductResponseDTO, PaginatedProductResponse>
    {
        public void Process(PaginatedProductResponseDTO source, PaginatedProductResponse destination, ResolutionContext context)
        {
            destination.Products = context.Mapper.Map<IReadOnlyCollection<ProductResponse>>(source.Products);
            destination.Pagination = context.Mapper.Map<PaginationResponse>(source.Pagination);
        }
    }
}