using SupportYourApp.Contracts;
using SupportYourApp.Domain.Entities;

namespace SupportYourApp.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<OtpResponseDto> Register(RegisterDto registerDto);
        Task<OtpResponseDto> Login(long icNumber);

        Task VerifyPhoneOtp(string phoneNumber, string otp);
        Task VerifyEmailOtp(string email, string otp);

        Task Verify2FAOtp(string phoneNumber, string phoneOtp, string email, string emailOtp);
        Task CompleteProfile(ProfileCompleteDto profileCompleteDto);
        Task<CustomerDto> GetCustomerProfile(long icNumber);

        Task SignOut();


        Task<OtpResponseDto> ResendOtp(string email, string phoneNumber);

    }
}
