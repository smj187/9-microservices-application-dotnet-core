using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Queries;
using PaymentService.Contracts.v1.Requests;
using PaymentService.Contracts.v1.Responses;
using PaymentService.Core.Domain.Aggregates;
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


        [HttpGet]
        [Route("{paymentid:guid}")]
        public async Task<IActionResult> FindPaymentAsync([FromRoute] Guid paymentId)
        {
            var query = new FindPaymentQuery
            {
                PaymentId = paymentId
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersAsync()
        {
            var query = new ListPaymentsQuery();

            var data = await _mediator.Send(query);
            return Ok(data);

            //var result = _mapper.Map<IEnumerable<Payment>>(data);
        }
    }
}
