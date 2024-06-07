using BlogAPI.Application.Abstraction.Services;
using BlogAPI.Application.Abstraction.Token;
using BlogAPI.Application.Dtos;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }



        public async Task<Token> LoginAsync(string emailOrUsername, string password, int accessTokenLifeTime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(emailOrUsername);

            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(emailOrUsername);
            }

            if (appUser == null)
            {
                throw new Exception("Kullanıcı veya şifre hatalı !");
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateToken(accessTokenLifeTime);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, appUser.Id, token.Expiration, 30);
                return token;
            }
            else
            {
                throw new Exception("Kullanıcı veya şifre hatalı !");
            }

        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateToken(15);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user.Id, token.Expiration, 20);
                return token;
            }
            else
            {
                throw new Exception("Refresh token hatalı !");
            }
        }
        }
    }
