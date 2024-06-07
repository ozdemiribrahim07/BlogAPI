using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task<Dtos.Token> LoginAsync(string emailOrUsername, string password, int accessTokenLifeTime);

        Task<Dtos.Token> RefreshTokenLoginAsync(string refreshToken);


    }
}
