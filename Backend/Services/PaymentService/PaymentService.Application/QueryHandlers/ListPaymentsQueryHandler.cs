using MediatR;
using PaymentService.Application.Queries;
using PaymentService.Core.Entities;
using PaymentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.QueryHandlers
{
    public class ListPaymentsQueryHandler : IRequestHandler<ListPaymentsQuery, IEnumerable<Payment>>
    {
        private readonly IPaymentRepository<Payment> _paymentRepository;

        public ListPaymentsQueryHandler(IPaymentRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> Handle(ListPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.ListAsync();
        }
    }
}
