using AutoMapper;
using DeliveryService.Contracts.v1.Requests;
using DeliveryService.Contracts.v1.Responses;
using DeliveryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.API.Profiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<CreateDeliveryRequest, Delivery>();
            CreateMap<Delivery, DeliveryReponse>();
        }
    }
}
