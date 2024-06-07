using BlogAPI.Application.Abstraction.Services;
using BlogAPI.Application.Dtos.UserDtos;
using BlogAPI.Application.Features.Users.Commands.CreateUser;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = createUser.Email,
                UserName = createUser.KullaniciAdi,
                NameSurname = createUser.AdSoyad,
            }, createUser.Parola);


            CreateUserResponse response = new() { Succeeded = result.Succeeded };

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

        public async  Task UpdateRefreshTokenAsync(string refreshToken, string id, DateTime accessTokenDate, int refreshTokenDate)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenDate);

            await _userManager.UpdateAsync(user);
            

        }
    }
}
