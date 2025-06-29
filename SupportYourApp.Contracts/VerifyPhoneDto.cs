using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class VerifyPhoneDto
    {
        [Required]
        [MaxLength(12)]
        public required string Phone { get; set; }
        [Required]
        public required string Otp { get; set; }


    }
}
