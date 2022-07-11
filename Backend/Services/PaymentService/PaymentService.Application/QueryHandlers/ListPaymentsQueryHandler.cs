using MediatR;
using PaymentService.Application.Queries;
using PaymentService.Core.Domain.Aggregates;
using PaymentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.QueryHandlers
{
    public class ListPaymentsQueryHandler : IRequestHandler<ListPaymentsQuery, IReadOnlyCollection<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public ListPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IReadOnlyCollection<Payment>> Handle(ListPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.ListAsync();
        }
    }
}
