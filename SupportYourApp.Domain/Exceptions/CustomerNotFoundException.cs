using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Domain.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(long icNumber)
            : base($"The customer with the IcNumber {icNumber} was not found.")
        {
        }
        public CustomerNotFoundException(string phoneNumber) : base($"The customer with the phone number {phoneNumber} was not found.")
        {

        }

        public CustomerNotFoundException(StringBuilder email) : base($"The customer with the email {email} was not found.")
        {

        }
    }
}
