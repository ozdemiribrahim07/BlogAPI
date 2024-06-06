using BlogAPI.Application.Abstraction.Services;
using BlogAPI.Application.Dtos.UserDtos;
using BlogAPI.Application.Exceptions;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

          CreateUserResponse createUserResponse =  await _userService.CreateAsync(new()
            {
                AdSoyad = request.AdSoyad,
                KullaniciAdi = request.KullaniciAdi,
                Email = request.Email,
                Parola = request.Parola,
                ParolaTekrar = request.ParolaTekrar
            });


            return new()
            {
                Message = createUserResponse.Message,
                Succeeded = createUserResponse.Succeeded
            };
        }
    }
}
