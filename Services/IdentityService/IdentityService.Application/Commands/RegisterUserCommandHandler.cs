using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Token>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<Token> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _userService.RegisterAsync(request.User, request.Password);
            if (token == null)
            {
                throw new Exception("TODO: handle something went wrong");
            }

            return token;
        }
    }
}
