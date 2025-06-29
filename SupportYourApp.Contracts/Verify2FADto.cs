using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class Verify2FADto
    {
        [Required]
        [EmailAddress]
        public required string email { get; set; }
        [Required]
        public required string EmailOtp { get; set; }

        [Required]
        [MaxLength(12)]
        public required string Phone { get; set; }
        [Required]
        public required string PhoneOtp { get; set; }
    }
}
