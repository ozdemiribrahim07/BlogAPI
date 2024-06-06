using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Dtos.UserDtos
{
    public class CreateUser
    {
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string ParolaTekrar { get; set; }
    }
}
