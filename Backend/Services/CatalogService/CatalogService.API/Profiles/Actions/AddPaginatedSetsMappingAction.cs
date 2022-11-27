using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles.Actions
{
    public class AddPaginatedSetsMappingAction : IMappingAction<PaginatedSetResponseDTO, PaginatedSetResponse>
    {
        public void Process(PaginatedSetResponseDTO source, PaginatedSetResponse destination, ResolutionContext context)
        {
            destination.Sets = context.Mapper.Map<IReadOnlyCollection<SetResponse>>(source.Sets);
            destination.Pagination = context.Mapper.Map<PaginationResponse>(source.Pagination);
        }
    }
}
