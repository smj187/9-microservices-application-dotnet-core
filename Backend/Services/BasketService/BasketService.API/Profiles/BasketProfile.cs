using AutoMapper;
using BasketService.Contracts.v1.Contracts;
using BasketService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Item, ItemResponse>();
            CreateMap<Basket, BasketResponse>();
        }
    }
}
