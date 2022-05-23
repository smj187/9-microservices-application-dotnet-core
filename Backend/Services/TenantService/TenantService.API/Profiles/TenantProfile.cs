using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Requests;
using TenantService.Contracts.v1.Responses;
using TenantService.Core.Entities;

namespace TenantService.API.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<CreateTenantRequest, Tenant>();
            CreateMap<Tenant, TenantResponse>();
        }
    }
}
