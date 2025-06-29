using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SupportYourApp.Contracts;
using SupportYourApp.Domain.Constants;
using SupportYourApp.Domain.Entities;
using SupportYourApp.Domain.Exceptions;
using SupportYourApp.Services.Abstractions;
using System.Text;
using static System.Net.WebRequestMethods;

namespace SupportYourApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;


        public CustomerService(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<OtpResponseDto> ResendOtp(string email, string phoneNumber)
        {
            var customer = await _userManager.FindByNameAsync(email);

            if (customer == null)
            {
                throw new CustomerNotFoundException(new StringBuilder(email));
            }


            customer = await FindCustomerByPhoneNumber(phoneNumber);
            if (customer == null)
            {
                throw new CustomerNotFoundException(phoneNumber);
            }


            return await GenerateOtp(customer);
        }

        public async Task<OtpResponseDto> Login(long icNumber)
        {
            var customer = await FindCustomerByIcNumber(icNumber);
            if (customer == null)
            {
                throw new CustomerNotFoundException(icNumber);
            }

            if (!customer.IsAgreedToTermsAndConditions)
            {
                throw new AgreementException();
            }

            OtpResponseDto otpResponse = null;
            if (!await _signInManager.IsTwoFactorClientRememberedAsync(customer))
            {
                otpResponse = await GenerateOtp(customer);
            }
            else
            {
                await _signInManager.SignInAsync(customer, true);
            }
            return otpResponse;
        }

        public async Task VerifyPhoneOtp(string phoneNumber, string otp)
        {
            var customer = await VerifyPhone(phoneNumber, otp);
            customer.PhoneNumberConfirmed = true;
            await UpdateCustomer(customer);
        }

        public async Task VerifyEmailOtp(string email, string otp)
        {
            var customer = await VerifyEmail(email, otp);
            customer.EmailConfirmed = true;
            await UpdateCustomer(customer);
        }

        public async Task<OtpResponseDto> Register(RegisterDto registerDto)
        {
            var customer = await FindCustomerByIcNumber(registerDto.IcNumber);
            if (customer != null)
            {
                throw new CustomerAlreadyExistsException(registerDto.IcNumber);
            }

            customer = new Customer
            {
                MobileNumber = registerDto.MobileNumber,
                Name = registerDto.Name,
                IcNumber = registerDto.IcNumber,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.MobileNumber,
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(customer);
            if (!result.Succeeded)
            {
                throw new Exception(Constants.GENERIC_ERROR);
            }
            return await GenerateOtp(customer);
        }

        public async Task CompleteProfile(ProfileCompleteDto profileCompleteDto)
        {
            var customer = await FindCustomerByIcNumber(profileCompleteDto.IcNumber);
            if (customer == null)
            {
                throw new CustomerNotFoundException(profileCompleteDto.IcNumber);
            }

            if (!await _userManager.IsEmailConfirmedAsync(customer) || !await _userManager.IsPhoneNumberConfirmedAsync(customer))
            {
                throw new EmailAndPhoneCOnfirmationRequiredException();
            }
            customer.IsAgreedToTermsAndConditions = profileCompleteDto.IsAgreedToTermsAndConditions;
            await UpdateCustomer(customer);

            await _userManager.AddPasswordAsync(customer, profileCompleteDto.Password);

            await _signInManager.SignInAsync(customer, true);
        }

        public async Task<CustomerDto> GetCustomerProfile(long icNumber)
        {
            var customer = await FindCustomerByIcNumber(icNumber);
            if (customer == null)
            {
                throw new CustomerNotFoundException(icNumber);
            }

            return new CustomerDto
            {
                Email = customer.Email,
                MobileNumber = customer.MobileNumber,
                IcNumber = customer.IcNumber,
                Id = customer.Id,
                IsAgreedToTermsAndConditions = customer.IsAgreedToTermsAndConditions,
                Name = customer.Name,
            };
        }

        public async Task Verify2FAOtp(string phoneNumber, string phoneOtp, string email, string emailOtp)
        {

            await VerifyEmail(email, emailOtp);
            var customer = await VerifyPhone(phoneNumber, phoneOtp);

            await _signInManager.RememberTwoFactorClientAsync(customer);
        }



        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<Customer> FindCustomerByIcNumber(long icNumber)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(c => c.IcNumber == icNumber);
            return customer;
        }

        private async Task<Customer> FindCustomerByPhoneNumber(string phoneNumber)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
            return customer;
        }

        private async Task<OtpResponseDto> GenerateOtp(Customer customer)
        {
            var phoneOtp = await _userManager.GenerateUserTokenAsync(customer, TokenOptions.DefaultPhoneProvider, Constants.CUSTOME_PHONE_PROVIDER_PURPOSE);
            var emailOtp = await _userManager.GenerateUserTokenAsync(customer, TokenOptions.DefaultEmailProvider, Constants.CUSTOME_EMAIL_PROVIDER_PURPOSE);

            return new OtpResponseDto
            {
                EmailOtp = emailOtp,
                PhoneOtp = phoneOtp
            };
        }

        private async Task UpdateCustomer(Customer customer)
        {
            var result = await _userManager.UpdateAsync(customer);
            if (!result.Succeeded)
            {
                throw new Exception(Constants.GENERIC_ERROR);
            }
        }

        private async Task<Customer> VerifyEmail(string email, string otp)
        {
            var customer = await _userManager.FindByNameAsync(email);
            if (customer == null)
            {
                throw new CustomerNotFoundException(new StringBuilder(email));
            }

            var isValid = await _userManager.VerifyUserTokenAsync(customer, TokenOptions.DefaultEmailProvider, Constants.CUSTOME_EMAIL_PROVIDER_PURPOSE, otp);
            if (!isValid)
            {
                throw new InvalidTokenException();
            }
            return customer;
        }

        private async Task<Customer> VerifyPhone(string phoneNumber, string otp)
        {
            var customer = await FindCustomerByPhoneNumber(phoneNumber);
            if (customer == null)
            {
                throw new CustomerNotFoundException(phoneNumber);
            }

            var isValid = await _userManager.VerifyUserTokenAsync(customer, TokenOptions.DefaultPhoneProvider, Constants.CUSTOME_PHONE_PROVIDER_PURPOSE, otp);
            if (!isValid)
            {
                throw new InvalidTokenException();
            }
            return customer;
        }
    }
}
