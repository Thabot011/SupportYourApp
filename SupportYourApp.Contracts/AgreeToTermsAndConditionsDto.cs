using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Contracts
{
    public class ProfileCompleteDto
    {

        [Required]
        [Range(100000000000, 999999999999, ErrorMessage = "Must be a 12-digit number.")]
        public long IcNumber { get; set; }
        [Required]
        public bool IsAgreedToTermsAndConditions { get; set; }

        [Required]
        [MaxLength(6)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string VerifyPassword { get; set; }
    }
}
