using BuildingBlocks.Exceptions.Domain;
using MediatR;
using PaymentService.Application.Queries;
using PaymentService.Core.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.QueryHandlers
{
    public class FindPaymentQueryHandler : IRequestHandler<FindPaymentQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public FindPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Task<Payment> Handle(FindPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = _paymentRepository.FindAsync(request.PaymentId);
            if (payment == null)
            {
                throw new AggregateNotFoundException(nameof(Payment), request.PaymentId);
            }

            return payment;
        }
    }
}
