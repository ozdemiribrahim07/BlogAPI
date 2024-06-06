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
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            
           IdentityResult result =  await _userManager.CreateAsync(new()
             {
                 Id = Guid.NewGuid().ToString(),
                 Email = request.Email,
                 UserName = request.KullaniciAdi,
                 NameSurname = request.AdSoyad,
             },request.Parola);

           
            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message += "Kullanıcı oluşturuldu";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}-{error.Description}<br>";
                }
            }

            return response;
           
                


                




        }
    }
}
