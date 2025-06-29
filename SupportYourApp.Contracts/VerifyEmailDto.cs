using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class VerifyEmailDto
    {
        [Required]
        [EmailAddress]
        public required string email { get; set; }
        [Required]
        public required string Otp { get; set; }
    }
}
