using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Domain.Exceptions
{
    public sealed class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException()
            : base("Invalid token")
        {
        }
    }
}
