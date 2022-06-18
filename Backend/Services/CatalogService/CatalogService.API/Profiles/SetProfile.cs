using AutoMapper;
using CatalogService.Contracts.v1.Contracts;
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
            CreateMap<Set, SetResponse>();
        }
    }
}
