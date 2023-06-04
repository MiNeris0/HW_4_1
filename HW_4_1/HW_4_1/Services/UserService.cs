using System.Diagnostics.Metrics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using HW_4_1.Config;
using HW_4_1.Dtos;
using HW_4_1.Dtos.Requests;
using HW_4_1.Dtos.Responses;
using HW_4_1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HW_4_1.Services;

public class UserService : IUserService
{
    private readonly IInternalHttpClientService _httpClientService;
    private readonly ILogger<UserService> _logger;
    private readonly ApiOption _options;
    private readonly string _userApi = "api/users/";
    private readonly string _resourceApi = "api/unknown/";
    private readonly string _registerApi = "api/register";
    private readonly string _loginApi = "api/login";
    private readonly string _usersApiDelayed = "api/users?delay=3";

    public UserService(
        IInternalHttpClientService httpClientService,
        IOptions<ApiOption> options,
        ILogger<UserService> logger)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<ListResponse<UserDto>> GetUsersByPage(int page)
    {
        string tempUserApi = _userApi.TrimEnd('/') + "?";
        var result = await _httpClientService.SendAsync<ListResponse<UserDto>, object>($"{_options.Host}{tempUserApi}page={page}", HttpMethod.Get);

        if (result != null)
        {
            _logger.LogInformation($"Page: {result.Page},\n\tPer page: {result.PerPage},\n\tTotal: {result.Total},\n\tTotal pages: {result.TotalPages}\n");

            foreach (var user in result.Data)
            {
                _logger.LogInformation($"id = {user.Id}\n\tEmail: {user.Email}\n\tFirst name: {user.FirstName}\n\tLast name: {user.LastName}\n\tAvatar: {user.Avatar}");
            }

            _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
        }

        return result;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var result = await _httpClientService.SendAsync<BaseResponse<UserDto>, object>($"{_options.Host}{_userApi}{id}", HttpMethod.Get);

        if (result?.Data != null)
        {
            _logger.LogInformation($"User with id = {result.Data.Id} was found:\n\tEmail: {result.Data.Email}\n\tFirst name: {result.Data.FirstName}\n\tLast name: {result.Data.LastName}\n\tAvatar: {result.Data.Avatar}");
            _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
        }
        else
        {
            _logger.LogInformation($"User with id = {id} was not found.");
        }

        return result?.Data;
    }

    public async Task<ListResponse<ResourceDto>> GetResourcesList()
    {
        var result = await _httpClientService.SendAsync<ListResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}", HttpMethod.Get);

        if (result != null)
        {
            _logger.LogInformation($"Page: {result.Page},\n\tPer page: {result.PerPage}\n\t,Total: {result.Total}\n\tTotal pages: {result.TotalPages}\n");

            foreach( var resource in result.Data)
            {
                _logger.LogInformation($"id = {resource.Id}\n\tName: {resource.Name}\n\tYear: {resource.Year}\n\tColor: {resource.Color}\n\tPantone value: {resource.PantoneValue}");
            }

            _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
        }

        return result;
    }

    public async Task<ResourceDto> GetResourceById(int id)
    {
        var result = await _httpClientService.SendAsync<BaseResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}{id}", HttpMethod.Get);

        if (result != null)
        {
            _logger.LogInformation($"Resource with id = {result.Data.Id} was found.\n\tName: {result.Data.Name}\n\tYear: {result.Data.Year}\n\tColor: {result.Data.Color}\n\tPantone value: {result.Data.PantoneValue}");
            _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
        }
        else
        {
            _logger.LogInformation($"Resource with id = {id} was not found.");
        }

        return result?.Data;
    }

    public async Task<UserResponse> CreateUser(string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserResponse, UserRequest>(
            $"{_options.Host}{_userApi}",
            HttpMethod.Post,
            new UserRequest()
            {
                Job = job,
                Name = name
            });

        if (result != null)
        {
            _logger.LogInformation($"User with id = {result?.Id} was created\n\tName: {result?.Name}, position: {result?.Job}\n\tCreated at: {result?.CreatedAt}");
        }

        return result;
    }

    public async Task<UserUpdateResponse> UpdateUser(int id, string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserUpdateResponse, UserRequest>($"{_options.Host}{_userApi}{id}", HttpMethod.Put, new UserRequest() { Job = job, Name = name });

        if (result != null)
        {
            _logger.LogInformation($"User with id = {result?.Id} was updated\n\tName: {result?.Name}, position: {result?.Job}\n\tUpdated at: {result?.UpdatedAt}");
        }

        return result;
    }

    public async Task<UserUpdateResponse> PatchUpdateUser(int id, string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserUpdateResponse, UserRequest>($"{_options.Host}{_userApi}{id}", HttpMethod.Patch, new UserRequest() { Job = job, Name = name });

        if (result != null)
        {
            _logger.LogInformation($"User with id = {result?.Id} was patched\n\tName: {result?.Name}, position: {result?.Job}\n\tPatched at: {result?.UpdatedAt}");
        }

        return result;
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

    public async Task<ListResponse<UserDto>> GetUsersDelayed()
    {
        var result = await _httpClientService.SendAsync<ListResponse<UserDto>, object>($"{_options.Host}{_usersApiDelayed}", HttpMethod.Get);

        if (result != null)
        {
            _logger.LogInformation($"Page: {result.Page},\n\tPer page: {result.PerPage},\n\tTotal: {result.Total},\n\tTotal pages: {result.TotalPages}\n");

            foreach (var user in result.Data)
            {
                _logger.LogInformation($"id = {user.Id}\n\tEmail: {user.Email}\n\tFirst name: {user.FirstName}\n\tLast name: {user.LastName}\n\tAvatar: {user.Avatar}");
            }

            _logger.LogInformation($"Support:\n\tUrl: {result.Support.Url}\n\tText: {result.Support.Text}");
        }

        return result;
    }

    public async Task<bool> DeleteUserById(int id)
    {
        var result = await _httpClientService.SendAsync<object, object>($"{_options.Host}{_userApi}{id}", HttpMethod.Delete);

        if (result != null)
        {
            _logger.LogInformation($"User with ID {id} was deleted.");
            return true;
        }
        else
        {
            _logger.LogInformation($"Unable to delete user with ID {id}.");
            return false;
        }
    }
}
