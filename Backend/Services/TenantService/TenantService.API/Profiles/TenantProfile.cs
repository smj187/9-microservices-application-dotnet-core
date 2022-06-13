using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Contracts;
using TenantService.Core.Domain.Aggregates;
using TenantService.Core.Domain.ValueObjects;

namespace TenantService.API.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<Tenant, TenantResponse>();
            CreateMap<Workingday, TenantWorkingDayResponse>();
            CreateMap<Address, TenantAddressResponse>();
        }
    }
}
