using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateToken(int seconds);
        string CreateRefreshToken();
    }
}
