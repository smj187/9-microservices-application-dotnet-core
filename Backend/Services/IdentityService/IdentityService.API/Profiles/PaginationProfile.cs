using AutoMapper;
using BuildingBlocks.EfCore.Helpers;
using IdentityService.Application.DTOs;
using IdentityService.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<PaginatedUsersResponseDTO, PaginatedUserResponse>();

            CreateMap<PagedResultBase, PaginationResponse>()
                .ForMember(dest => dest.CurrentPage, opts => opts.MapFrom(s => s.CurrentPage))
                .ForMember(dest => dest.TotalPages, opts => opts.MapFrom(s => s.PageCount))
                .ForMember(dest => dest.Pages, opts => opts.MapFrom(s => Enumerable.Range(1, s.PageCount)));
        }
    }
}