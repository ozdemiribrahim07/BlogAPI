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
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser appUser = await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);
            }

            if (appUser == null)
            {
                throw new Exception("Kullanıcı veya şifre hatalı !");
            }
          

           SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);

            if (result.Succeeded) {

               Token token = _tokenHandler.CreateToken(5);

                return new()
                {
                    Token = token
                };
            }
            else
            {
                return new()
                {
                    Message = "Kullanıcı veya şifre hatalı !"
                };
            }


        }
    }
}
