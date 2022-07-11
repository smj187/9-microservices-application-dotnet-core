using AutoMapper;
using PaymentService.Contracts.v1.Requests;
using PaymentService.Contracts.v1.Responses;
using PaymentService.Core.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.API.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<CreatePaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>();
        }
    }
}
