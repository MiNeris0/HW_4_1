using System.Threading.Tasks;
using HW_4_1.Dtos;
using HW_4_1.Dtos.Responses;

namespace HW_4_1.Services.Interfaces;

public interface IUserService
{
    Task<ListResponse<UserDto>> GetUsersByPage(int page);
    Task<UserDto> GetUserById(int id);
    Task<ListResponse<ResourceDto>> GetResourcesList();
    Task<ResourceDto> GetResourceById(int id);
    Task<UserResponse> CreateUser(string name, string job);
    Task<UserUpdateResponse> UpdateUser(int id, string name, string job);
    Task<UserUpdateResponse> PatchUpdateUser(int id, string name, string job);
    Task<RegisterResponse> RegisterUser(string email, string password);
    Task<RegisterResponse> RegisterUser(string email);
    Task<LoginResponse> LoginUser(string email, string password);
    Task<LoginResponse> LoginUser(string email);
    Task<ListResponse<UserDto>> GetUsersDelayed();
    Task<bool> DeleteUserById(int id);
}