using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class OtpResponseDto
    {
        public required string PhoneOtp { get; set; }
        public required string EmailOtp { get; set; }
    }
}
