using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
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
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest request)
        {
            var newUser = _mapper.Map<User>(request);
            newUser.CreatedAt = DateTimeOffset.Now;

            var command = new RegisterUserCommand
            {
                User = newUser,
                Password = request.Password,
            };


            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AuthenticateUserResponse>(data));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("role-promote")]
        public async Task<IActionResult> PromoteAsync([FromBody] RolePromoteRequest request)
        {
            var command = new PromoteRoleCommand
            {
                UserId = request.UserId,
                Role = request.Role
            };


            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<UserResponse>(data));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("role-revoke")]
        public async Task<IActionResult> RevokeAsync([FromBody] RoleRevokeRequest request)
        {
            var command = new RevokeRoleCommand
            {
                UserId = request.UserId,
                Role = request.Role
            };


            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<UserResponse>(data));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("user-list")]
        public async Task<IActionResult> ListUsersAsync()
        {
            var query = new ListUsersQuery();

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<IReadOnlyCollection<UserResponse>>(data));
        }

        [HttpGet]
        [Authorize]
        [Route("user-find/{userId:guid}")]
        public async Task<IActionResult> FindUserAsync([FromRoute] Guid userId)
        {
            var query = new FindUserQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<UserResponse>(data));
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("user-delete/{userId:guid}")]
        public async Task<IActionResult> TerminateUserAsync([FromRoute] Guid userId)
        {
            var query = new TerminateUserCommand()
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            if(data == false)
            {
                NotFound($"no user with {userId}");
            }
            return NoContent();
        }
    }
}
