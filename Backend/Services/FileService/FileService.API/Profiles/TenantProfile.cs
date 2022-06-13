using AutoMapper;
using FileService.Contracts.v1.Contracts;
using FileService.Core.Domain.Aggregates.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<TenantImageAsset, TenantResponse>();
            CreateMap<TenantVideoAsset, TenantResponse>();
        }
    }
}
