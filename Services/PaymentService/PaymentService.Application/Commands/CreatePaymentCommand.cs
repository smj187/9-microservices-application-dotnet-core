using MediatR;
using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Commands
{
    public class CreatePaymentCommand : IRequest<Payment>
    {
        public Payment NewPayment { get; set; } = default!;
    }
}
