using MediatR;
using PaymentService.Core.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Queries
{
    public class FindPaymentQuery : IRequest<Payment>
    {
        public Guid PaymentId { get; set; }
    }
}
