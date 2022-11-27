using AutoMapper;
using CatalogService.API.Profiles.Actions;
using CatalogService.Application.DTOs;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<Set, SetDetailsResponse>();
            CreateMap<Set, SetResponse>();

            CreateMap<PaginatedSetResponseDTO, PaginatedSetResponse>()
                .AfterMap<AddPaginatedSetsMappingAction>();
        }
    }
}