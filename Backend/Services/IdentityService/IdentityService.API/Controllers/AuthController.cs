using AutoMapper;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
using IdentityService.Application.Services;
using IdentityService.Contracts.v1.Requests;
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
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _identityService;

        public AuthController(IMediator mediator, IAuthService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }


        [HttpGet]
        [Route("auth/admin")]
        public async Task<IActionResult> Admin() 
            => Ok(await _identityService.CreateJsonWebToken("root@mail.com"));

        [HttpGet]
        [Route("auth/user")]
        public async Task<IActionResult> NormalUser()
            => Ok(await _identityService.CreateJsonWebToken("user@mail.com"));


        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest authenticateUserRequest)
        {

            var command = new AuthenticateUserCommand
            {
                Email = authenticateUserRequest.Email,
                Password = authenticateUserRequest.Password,
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }



    }
}
