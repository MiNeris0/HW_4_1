using HW_4_1.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResponse> RegisterUser(string email, string password);
        Task<RegisterResponse> RegisterUser(string email);
        Task<LoginResponse> LoginUser(string email, string password);
        Task<LoginResponse> LoginUser(string email);
    }
}
