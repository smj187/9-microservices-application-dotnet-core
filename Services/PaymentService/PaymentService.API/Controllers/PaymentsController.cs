using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Commands;
using PaymentService.Application.Queries;
using PaymentService.Contracts.v1.Requests;
using PaymentService.Contracts.v1.Responses;
using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PaymentsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentRequest createOrderRequest)
        {
            var mapped = _mapper.Map<Payment>(createOrderRequest);

            var command = new CreatePaymentCommand
            {
                NewPayment = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<PaymentResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersAsync()
        {
            var query = new ListPaymentsQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IEnumerable<Payment>>(data);
            return Ok(result);
        }
    }
}
