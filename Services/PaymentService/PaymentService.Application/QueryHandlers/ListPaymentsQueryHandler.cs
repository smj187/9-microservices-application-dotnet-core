using MediatR;
using PaymentService.Application.Queries;
using PaymentService.Application.Repositories;
using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.QueryHandlers
{
    public class ListPaymentsQueryHandler : IRequestHandler<ListPaymentsQuery, IEnumerable<Payment>>
    {
        private readonly PaymentRepository _paymentRepository;

        public ListPaymentsQueryHandler(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> Handle(ListPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _paymentRepository.ListPaymentsAsync();
        }
    }
}
