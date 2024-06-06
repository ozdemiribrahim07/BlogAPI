using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string ParolaTekrar { get; set; }

    }
}
