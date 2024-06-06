using BlogAPI.Application.Abstraction.Services;
using BlogAPI.Application.Abstraction.Token;
using BlogAPI.Application.Dtos;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
            
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
          var token = await _authService.LoginAsync(request.EmailOrUsername, request.Password,15);
            return new LoginUserCommandResponse()
            {
                Token = token
            };
        }
    }
}
