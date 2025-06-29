using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace SupportYourApp.Domain.Entities
{
    public class Customer : IdentityUser
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
        [Required]
        public bool IsAgreedToTermsAndConditions { get; set; }
    }
}
