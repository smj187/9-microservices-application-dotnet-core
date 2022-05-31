using AutoMapper;
using CatalogService.Contracts.v1;
using CatalogService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            // requests
            CreateMap<CreateGroupRequest, Group>()
                .ConstructUsing((src, ctx) =>
                {
                    var tags = ctx.Mapper.Map<IEnumerable<string>>(src.Tags ?? new List<string>());
                    return new Group(src.Name, src.Price, src.Description, src.PriceDescription, tags);
                });


            // responses
            CreateMap<Group, GroupResponse>();
        }
    }
}
