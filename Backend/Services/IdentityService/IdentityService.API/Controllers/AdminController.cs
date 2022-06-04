using AutoMapper;
using IdentityService.Application.Commands.Admin;
using IdentityService.Application.Queries.Users;
using IdentityService.Contracts.v1;
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

        [HttpPatch]
        [Route("{userid:guid}/roles/add")]
        public async Task<IActionResult> AddRolesToUserAsync([FromRoute] Guid userId, [FromBody] AddRoleToUserRequest request)
        {
            var command = new AddRoleToUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }        
        
        [HttpPatch]
        [Route("{userid:guid}/roles/remove")]
        public async Task<IActionResult> RemoveRolesFromUser([FromRoute] Guid userId, [FromBody] RemoveRoleFromUserRequest request)
        {
            var command = new RemoveRoleFromUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
