using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class CustomerDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public long IcNumber { get; set; }
        public required string MobileNumber { get; set; }
        public required string Email { get; set; }
        public bool IsAgreedToTermsAndConditions { get; set; }
    }
}
