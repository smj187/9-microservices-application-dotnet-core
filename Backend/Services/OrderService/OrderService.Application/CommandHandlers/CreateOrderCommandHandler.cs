using BuildingBlocks.EfCore.Interfaces;
using MediatR;
using OrderService.Application.Commands;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository<Order> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderRepository<Order> orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(request.NewOrder);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.NewOrder;
        }
    }
}
