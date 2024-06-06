using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Exceptions
{
    public class CreateUserFailedException : Exception
    {
        public CreateUserFailedException()
        {
        }

        public CreateUserFailedException(string? message) : base(message)
        {
        }

        public CreateUserFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
