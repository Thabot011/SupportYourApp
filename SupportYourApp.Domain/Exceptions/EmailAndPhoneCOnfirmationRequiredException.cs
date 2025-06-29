using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Domain.Exceptions
{
    public sealed class EmailAndPhoneCOnfirmationRequiredException : BadRequestException
    {
        public EmailAndPhoneCOnfirmationRequiredException()
            : base($"The email and phone confirmation is required")
        {
        }
    }
}
