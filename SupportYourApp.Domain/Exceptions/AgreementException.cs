using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Domain.Exceptions
{
    public sealed class AgreementException : BadRequestException
    {
        public AgreementException()
            : base($"You should agree first to the terms and conditions")
        {
        }
    }
}
