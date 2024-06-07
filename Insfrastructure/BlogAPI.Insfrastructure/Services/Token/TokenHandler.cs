using BlogAPI.Application.Abstraction.Token;
using BlogAPI.Application.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Insfrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string  CreateRefreshToken()
        {
            byte[] bytes = new byte[32];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        public Application.Dtos.Token CreateToken(int seconds)
        {
            Application.Dtos.Token token = new();

            SymmetricSecurityKey securityKey = new (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            
            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken jwtSecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer : _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken =  tokenHandler.WriteToken(jwtSecurityToken);

            //Refresh Token

            string refreshToken = CreateRefreshToken();
            token.RefreshToken = refreshToken;

            return token;




        }



    }
}
