using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [Range(100000000000, 999999999999, ErrorMessage = "Must be a 12-digit number.")]
        public long IcNumber { get; set; }
        [Required]
        [MaxLength(12)]
        public required string MobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
