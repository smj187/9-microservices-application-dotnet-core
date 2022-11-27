using AutoMapper;
using BuildingBlocks.Mongo.Helpers;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Sets;
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
            CreateMap<MongoPaginationResult, PaginationResponse>(MemberList.Destination);
        }
    }
}