using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportYourApp.Contracts;
using SupportYourApp.Domain.Entities;
using SupportYourApp.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportYourApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var otp = await _customerService.Register(dto);
            return Ok(new APIResponse<OtpResponseDto>
            {
                Response = otp,
                Message = "Codes are sent to registred email and phone please verify"
            });
        }

        [HttpPost("resendOtp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpDto dto)
        {
            var otp = await _customerService.ResendOtp(dto.Email, dto.MobileNumber);
            return Ok(new APIResponse<OtpResponseDto>
            {
                Response = otp,
                Message = "Codes are sent to registred email and phone please verify"
            });
        }

        [HttpPost("login/{icNumber}")]
        public async Task<IActionResult> Login([FromRoute] long icNumber)
        {
            var otp = await _customerService.Login(icNumber);
            if (otp == null)
            {
                return Ok(new APIResponse<OtpResponseDto>
                {
                    Message = "Customer logged in successfully"
                });
            }
            return Ok(new APIResponse<OtpResponseDto>
            {
                Response = otp,
                Message = "2FA codes are sent to registred email and phone please verify"
            });
        }

        [HttpPost("verifyPhone")]
        public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneDto verifyPhoneDto)
        {
            await _customerService.VerifyPhoneOtp(verifyPhoneDto.Phone, verifyPhoneDto.Otp);
            return Ok(new APIResponse<string>
            {
                Message = "Phone number is verified successfully"
            });
        }

        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto verifyEmailDto)
        {
            await _customerService.VerifyEmailOtp(verifyEmailDto.email, verifyEmailDto.Otp);
            return Ok(new APIResponse<string>
            {
                Message = "Email is verified successfully"
            });
        }

        [HttpPost("verify2FA")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FADto verify2FADto)
        {
            await _customerService.Verify2FAOtp(verify2FADto.Phone, verify2FADto.PhoneOtp, verify2FADto.email, verify2FADto.EmailOtp);
            return Ok(new APIResponse<string>
            {
                Message = "2FA is verified successfully"
            });
        }




        [HttpPost("profileComplete")]
        public async Task<IActionResult> ProfileComplete([FromBody] ProfileCompleteDto profileCompleteDto)
        {
            await _customerService.CompleteProfile(profileCompleteDto);
            return Ok(new APIResponse<string>
            {
                Message = "Profile is saved successfully"
            });
        }

        [HttpGet("customerProfile/{icNumber}")]
        public async Task<IActionResult> GetCustomerProfile([FromRoute] int icNumber)
        {
            var Customer = await _customerService.GetCustomerProfile(icNumber);
            return Ok(new APIResponse<CustomerDto>
            {
                Response = Customer
            });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> logOut()
        {
            await _customerService.SignOut();
            return Ok(new APIResponse<string>
            {
                Message = "User is logged out successfully",
            });
        }
    }
}
