using AutoMapper;
using IdentityService.Application.Commands.Admins;
using IdentityService.Application.Queries.Admins;
using IdentityService.Contracts.v1.Contracts;
using MediatR;
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
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AdminController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("find/{userid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindUserAsync([FromRoute] Guid userId)
        {
            var query = new FindUserQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<AdminUserResponse>(data));
        }

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<AdminUserResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListUsersAsync()
        {
            var query = new ListUsersQuery();

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<IReadOnlyCollection<AdminUserResponse>>(data));
        }

        [HttpPatch]
        [Route("{userid:guid}/roles/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRolesToUserAsync([FromRoute] Guid userId, [FromBody] AddRoleToUserRequest request)
        {
            var command = new AddRoleToUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AdminUserResponse>(data));
        }

        [HttpPatch]
        [Route("{userid:guid}/roles/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveRolesFromUser([FromRoute] Guid userId, [FromBody] RemoveRoleFromUserRequest request)
        {
            var command = new RemoveRoleFromUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AdminUserResponse>(data));
        }


        [HttpPatch]
        [Route("{userid:guid}/lock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LockUserAccountAsync([FromRoute] Guid userId)
        {
            var command = new LockUserAccountCommand
            {
                UserId = userId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AdminUserResponse>(data));
        }

        [HttpPatch]
        [Route("{userid:guid}/unlock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnlockUserAccountAsync([FromRoute] Guid userId)
        {
            var command = new UnlockUserAccountCommand
            {
                UserId = userId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AdminUserResponse>(data));
        }

    }
}
