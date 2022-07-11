using AutoMapper;
using OrderService.Contracts.v1.Requests;
using OrderService.Contracts.v1.Responses;
using OrderService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<Order, OrderResponse>();
        }
    }
}
