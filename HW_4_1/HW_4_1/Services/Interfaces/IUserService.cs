using System.Threading.Tasks;
using HW_4_1.Dtos;
using HW_4_1.Dtos.Responses;

namespace HW_4_1.Services.Interfaces;

public interface IUserService
{
    Task<ListResponse<UserDto>> GetUsersByPage(int page);
    Task<UserDto> GetUserById(int id);
    Task<UserResponse> CreateUser(string name, string job);
    Task<UserUpdateResponse> UpdateUser(int id, string name, string job);
    Task<UserUpdateResponse> PatchUpdateUser(int id, string name, string job);
    Task<ListResponse<UserDto>> GetUsersDelayed();
    Task<bool> DeleteUserById(int id);
}