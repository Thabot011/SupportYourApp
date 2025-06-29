using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Domain.Exceptions
{
    public sealed class CustomerAlreadyExistsException : BadRequestException
    {
        public CustomerAlreadyExistsException(long IcNumber)
            : base($"The account with the IcNumber {IcNumber} already exists try to login")
        {
        }

        public CustomerAlreadyExistsException(string email)
            : base($"The account with the email {email} already exists")
        {
        }
    }
}
