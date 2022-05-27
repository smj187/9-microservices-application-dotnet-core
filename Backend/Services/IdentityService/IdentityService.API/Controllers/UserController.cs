using AutoMapper;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
using IdentityService.Contracts.v1.Requests;
using IdentityService.Contracts.v1.Responses;
using IdentityService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest registerUserRequest)
        {
            if((registerUserRequest.Email != registerUserRequest.ConfirmEmail) || (registerUserRequest.Password != registerUserRequest.ConfirmPassword))
            {
                return BadRequest("email or password confirmation does not match");
            }

            var command = new RegisterUserCommand
            {
                Username = registerUserRequest.Username,
                Email = registerUserRequest.Email,
                Password = registerUserRequest.Password
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpGet]
        [Route("find/{id:guid}")]
        public async Task<IActionResult> FindUserAsync([FromRoute] Guid userId)
        {
            var query = new FindUserQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListUsersAsync()
        {
            var query = new ListUsersQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}
