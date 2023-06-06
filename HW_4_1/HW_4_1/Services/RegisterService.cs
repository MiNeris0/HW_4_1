using HW_4_1.Config;
using HW_4_1.Dtos.Requests;
using HW_4_1.Dtos.Responses;
using HW_4_1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly ILogger<UserService> _logger;
        private readonly ApiOption _options;
        private readonly string _registerApi = "api/register";
        private readonly string _loginApi = "api/login";

        public RegisterService(
        IInternalHttpClientService httpClientService,
        IOptions<ApiOption> options,
        ILogger<UserService> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<RegisterResponse> RegisterUser(string email, string password)
        {
            var result = await _httpClientService.SendAsync<RegisterResponse, RegisterRequest>($"{_options.Host}{_registerApi}", HttpMethod.Post, new RegisterRequest() { Email = email, Password = password });

            if (result != null)
            {
                _logger.LogInformation($"Successful registration!\n\tEmail: {result.Id}\n\tToken: {result.Token}");
            }

            return result;
        }

        public async Task<RegisterResponse> RegisterUser(string email)
        {
            var result = await _httpClientService.SendAsync<RegisterResponse, RegisterRequest>($"{_options.Host}{_registerApi}", HttpMethod.Post, new RegisterRequest() { Email = email, Password = default });

            if (result is null)
            {
                _logger.LogInformation("Unsuccessful registration!\n\tError: missing password");
            }

            return result;
        }

        public async Task<LoginResponse> LoginUser(string email, string password)
        {
            var result = await _httpClientService.SendAsync<LoginResponse, RegisterRequest>($"{_options.Host}{_loginApi}", HttpMethod.Post, new RegisterRequest { Email = email, Password = password });

            if (result != null)
            {
                _logger.LogInformation($"You have been logged in\n\tToken: {result.Token}");
            }

            return result;
        }

        public async Task<LoginResponse> LoginUser(string email)
        {
            var result = await _httpClientService.SendAsync<LoginResponse, RegisterRequest>($"{_options.Host}{_loginApi}", HttpMethod.Post, new RegisterRequest { Email = email, Password = default });

            if (result is null)
            {
                _logger.LogInformation("Login failed.\n\tError: missing password");
            }

            return result;
        }
    }
}
