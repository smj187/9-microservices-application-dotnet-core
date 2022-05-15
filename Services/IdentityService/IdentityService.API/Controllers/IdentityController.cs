using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
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
    public class IdentityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IdentityController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest request)
        {
            var mapped = _mapper.Map<Token>(request);

            var command = new AuthenticateUserCommand
            {
                Token = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<AuthenticateUserResponse>(data);
            return Ok(result);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshAuthenticationRequest request)
        {

            var command = new RefreshAuthenticationCommand
            {
                Token = request.RefreshToken
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<AuthenticateUserResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("refresh-token/{userId:guid}")]
        public async Task<IActionResult> GetRefreshTokensAsync([FromRoute] Guid userId)
        {
            var query = new ListUserTokensQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("revoke-token")]
        public async Task<IActionResult> RevokeAuthenticationAsync([FromBody] RevokeAuthenticationRequest request)
        {
            var command = new RevokeAuthenticationCommand
            {
                RefreshToken = request.RefreshToken,
                UserId = request.UserId
            };

            var data = await _mediator.Send(command);
            return Ok(data == true ? "success" : "an error occured");
        }



    }
}
