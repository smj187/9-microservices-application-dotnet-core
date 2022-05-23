using MediatR;
using PaymentService.Application.Commands;
using PaymentService.Application.Repositories;
using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.CommandHandlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly PaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.CreatePaymentAsync(request.NewPayment);
        }
    }
}
