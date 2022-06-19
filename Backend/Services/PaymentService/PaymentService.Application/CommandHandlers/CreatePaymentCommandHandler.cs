using BuildingBlocks.EfCore.Repositories.Interfaces;
using MediatR;
using PaymentService.Application.Commands;
using PaymentService.Core.Entities;
using PaymentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.CommandHandlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly IPaymentRepository<Payment> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentCommandHandler(IPaymentRepository<Payment> paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            await _paymentRepository.AddAsync(request.NewPayment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.NewPayment;
        }
    }
}
